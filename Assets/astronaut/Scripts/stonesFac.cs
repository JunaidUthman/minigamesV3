using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class StonesFactory : MonoBehaviour
{



    private bool gameActive = true;


    [Header("Stone Settings")]
    public GameObject stonePrefab; // Un prefab qui contient les deux pierres (haut et bas)
    public float spawnInterval = 5f;
    public float spawnX = 10f;
    public float despawnX = -17;

    [Header("Optimization Mode")]
    [Tooltip("Pool Mode: Réutilise les stones. Single Mode: Une seule stone recyclée")]
    public OptimizationMode optimizationMode = OptimizationMode.Pool;

    [Header("Pool Settings")]
    [Tooltip("Nombre maximum de stones dans le pool")]
    public int maxPoolSize = 3;

    [Header("Single Stone Settings")]
    [Tooltip("Vitesse de déplacement de la stone en mode Single")]
    public float stoneSpeed = 2f;

    private OpGenerator opGenerator;
    private GameObject lastStone;

    // Variables pour le Pool Mode
    private Queue<GameObject> stonePool = new Queue<GameObject>();
    private List<GameObject> activeStones = new List<GameObject>();

    // Variables pour le Single Mode
    private GameObject singleStone;
    private bool isSingleStoneMoving = false;

    public enum OptimizationMode
    {
        Pool,      // Utilise un pool d'objets
        Single,    // Une seule stone qui se recycle
        Destroy    // Détruit les stones (mode original amélioré)
    }

    void Start()
    {
        // Obtenir la référence au générateur d'opérations
        GameObject opLogicObject = GameObject.FindGameObjectWithTag("OpLogic");
        if (opLogicObject != null)
        {
            opGenerator = opLogicObject.GetComponent<OpGenerator>();
        }
        else
        {
            Debug.LogError("OpLogic GameObject not found!");
        }

        // Initialiser selon le mode choisi
        switch (optimizationMode)
        {
            case OptimizationMode.Pool:
                InitializePool();
                SpawnStoneFromPool();
                break;
            case OptimizationMode.Single:
                InitializeSingleStone();
                break;
            case OptimizationMode.Destroy:
                SpawnStone();
                break;
        }
    }

    void Update()
    {
        if(!gameActive) return; // Ne pas mettre à jour si le jeu n'est pas actif
        switch (optimizationMode)
        {
            case OptimizationMode.Pool:
                UpdatePoolMode();
                break;
            case OptimizationMode.Single:
                UpdateSingleMode();
                break;
            case OptimizationMode.Destroy:
                UpdateDestroyMode();
                break;
        }
    }




    public void SetGameActive(bool isActive)
    {
        gameActive = isActive;
    }




    #region Pool Mode
    void InitializePool()
    {
        // Pré-créer les stones dans le pool
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject stone = Instantiate(stonePrefab);
            stone.SetActive(false);
            stonePool.Enqueue(stone);
        }
    }

    void UpdatePoolMode()
    {
        // Vérifier les stones actives qui doivent être recyclées
        for (int i = activeStones.Count - 1; i >= 0; i--)
        {
            if (activeStones[i] != null && activeStones[i].transform.position.x < despawnX)
            {
                RecycleStone(activeStones[i]);
                activeStones.RemoveAt(i);
            }
        }

        // Spawn une nouvelle stone si nécessaire
        if (lastStone != null && lastStone.transform.position.x < despawnX)
        {
            SpawnStoneFromPool();
        }
    }

    void SpawnStoneFromPool()
    {
        GameObject stone = GetStoneFromPool();
        if (stone != null)
        {
            SetupStone(stone);
            activeStones.Add(stone);
            lastStone = stone;
        }
    }

    GameObject GetStoneFromPool()
    {
        if (stonePool.Count > 0)
        {
            GameObject stone = stonePool.Dequeue();
            stone.SetActive(true);
            return stone;
        }
        else
        {
            // Si le pool est vide, créer une nouvelle stone
            Debug.LogWarning("Pool is empty, creating new stone");
            return Instantiate(stonePrefab);
        }
    }

    void RecycleStone(GameObject stone)
    {
        stone.SetActive(false);
        stonePool.Enqueue(stone);
    }
    #endregion

    #region Single Mode
    void InitializeSingleStone()
    {
        singleStone = Instantiate(stonePrefab);
        SetupStone(singleStone);
        isSingleStoneMoving = true;
    }

    void UpdateSingleMode()
    {
        if (singleStone != null && isSingleStoneMoving)
        {
            // Déplacer la stone
            singleStone.transform.Translate(Vector3.left * stoneSpeed * Time.deltaTime);

            // Si la stone est sortie de l'écran, la repositionner
            if (singleStone.transform.position.x < despawnX)
            {
                RepositionSingleStone();
            }
        }
    }

    void RepositionSingleStone()
    {
        Vector3 newPos = new Vector3(spawnX, 0, 0);
        singleStone.transform.position = newPos;

        // Générer une nouvelle opération
        if (opGenerator != null)
        {
            TextMeshPro topStone = singleStone.transform.Find("Top Stone/Text")?.GetComponent<TextMeshPro>();
            TextMeshPro bottomStone = singleStone.transform.Find("Buttom Stone/Text")?.GetComponent<TextMeshPro>();

            if (topStone != null && bottomStone != null)
            {
                opGenerator.GeneratorOperation(topStone, bottomStone);
            }
        }
    }
    #endregion

    #region Destroy Mode (Original amélioré)
    void UpdateDestroyMode()
    {
        if (lastStone != null && lastStone.transform.position.x < despawnX)
        {
            StartCoroutine(DestroyAndSpawnStone());
        }
    }

    IEnumerator DestroyAndSpawnStone()
    {
        // Détruire l'ancienne stone
        if (lastStone != null)
        {
            Destroy(lastStone);
            lastStone = null;
        }

        // Petit délai pour éviter les problèmes de synchronisation
        yield return new WaitForEndOfFrame();

        // Spawner une nouvelle stone
        SpawnStone();
    }
    #endregion

    #region Common Methods
    void SpawnStone()
    {
        Vector3 spawnPos = new Vector3(spawnX, 0, 0);
        GameObject newStone = Instantiate(stonePrefab, spawnPos, Quaternion.identity);
        SetupStone(newStone);
        lastStone = newStone;
    }

    void SetupStone(GameObject stone)
    {
        // Positionner la stone (Y = 0 pour garder la même hauteur)
        Vector3 spawnPos = new Vector3(spawnX, 0, 0);
        stone.transform.position = spawnPos;

        // Configurer les textes
        TextMeshPro topStone = stone.transform.Find("Top Stone/Text")?.GetComponent<TextMeshPro>();
        TextMeshPro bottomStone = stone.transform.Find("Buttom Stone/Text")?.GetComponent<TextMeshPro>();

        if (topStone != null && bottomStone != null && opGenerator != null)
        {
            opGenerator.GeneratorOperation(topStone, bottomStone);
        }
        else
        {
            Debug.LogError("Missing TextMeshPro components or OpGenerator reference");
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Changer le mode d'optimisation pendant le jeu
    /// </summary>
    public void ChangeOptimizationMode(OptimizationMode newMode)
    {
        // Nettoyer l'ancien mode
        CleanupCurrentMode();

        // Changer le mode
        optimizationMode = newMode;

        // Initialiser le nouveau mode
        switch (optimizationMode)
        {
            case OptimizationMode.Pool:
                InitializePool();
                SpawnStoneFromPool();
                break;
            case OptimizationMode.Single:
                InitializeSingleStone();
                break;
            case OptimizationMode.Destroy:
                SpawnStone();
                break;
        }
    }

    /// <summary>
    /// Nettoyer le mode actuel
    /// </summary>
    void CleanupCurrentMode()
    {
        // Nettoyer les stones actives
        foreach (GameObject stone in activeStones)
        {
            if (stone != null)
                Destroy(stone);
        }
        activeStones.Clear();

        // Nettoyer le pool
        while (stonePool.Count > 0)
        {
            GameObject stone = stonePool.Dequeue();
            if (stone != null)
                Destroy(stone);
        }

        // Nettoyer la single stone
        if (singleStone != null)
        {
            Destroy(singleStone);
            singleStone = null;
        }

        // Nettoyer lastStone
        if (lastStone != null && optimizationMode == OptimizationMode.Destroy)
        {
            Destroy(lastStone);
            lastStone = null;
        }
    }

    /// <summary>
    /// Pauser/Reprendre le mouvement de la single stone
    /// </summary>
    public void PauseSingleStone(bool pause)
    {
        isSingleStoneMoving = !pause;
    }
    #endregion

    void OnDestroy()
    {
        CleanupCurrentMode();
    }
}