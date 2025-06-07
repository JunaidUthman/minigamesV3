using UnityEngine;

public class player_damage : MonoBehaviour
{
    // ---------------------- Game attributes ----------------------------------

    //========Sounds===============
    //public AudioSource lilExplosion;
    //=========Answers=============
    int rigthAnswerLayerNumber = 7;
    int wrongAnswerLayerNumber = 6;
    //int playerLayerNumber = 8;

    //=========damage graphics=============
    public SpriteRenderer circleRenderer; 
    private Color badCollision = Color.red;
    private Color goodCollision = Color.green;
    private Color initialColor = Color.white;
    [Range(0f, 1f)]
    public float targetOpacity = 0.5f;    // New opacity (0 is transparent, 1 is opaque)
    private float coloringTime = 0f;

    //======Health points =================
    private int healhPoints = 2;
    public GameObject heartPrefab; // Drag your heart prefab here
    public Transform heartsParent;

    //=======Game Over Class===============
    public gameOver gameOverRef;

    // ------------------------ DataBase attributes ----------------------------------



    void Start()
    {
        initialColor.a = 0f;
        circleRenderer.color = initialColor;
        badCollision.a = targetOpacity;
        goodCollision.a = targetOpacity;

        // Add new hearts
        for (int i = 0; i < healhPoints; i++)
        {
            Instantiate(heartPrefab, heartsParent);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == rigthAnswerLayerNumber)
        {
            coloringTime = 10f;
            circleRenderer.color = goodCollision;

        }
        else if (other.gameObject.layer == wrongAnswerLayerNumber)
        {
            coloringTime = 10f;
            circleRenderer.color = badCollision;
            //lilExplosion.Play();

            healhPoints--;
            UpdateHearts(healhPoints);
        }
        

    }
    void Update()
    {
        coloringTime -= 1;
        if (coloringTime <= 0)
        {
            circleRenderer.color = initialColor;
        }

        if (healhPoints <= 0)
        {
            gameOverRef = GameObject.Find("player_ship").GetComponent<gameOver>();
            gameOverRef.kill();
            
        }

        for (int i = 0; i < healhPoints; i++)
        {
            if (i < healhPoints)
            {

            }
        }
    }

    public void UpdateHearts(int healhPoints)
    {
        // Clear old hearts
        foreach (Transform child in heartsParent)
        {
            Destroy(child.gameObject);
        }

        // Add new hearts
        for (int i = 0; i < healhPoints; i++)
        {
            Instantiate(heartPrefab, heartsParent);
        }
    }

}
