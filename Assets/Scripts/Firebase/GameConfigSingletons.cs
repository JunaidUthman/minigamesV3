using UnityEngine;



public class GameConfigManager : MonoBehaviour
{
    public static GameConfigManager Instance { get; private set; }

    public GameConfig findComposition;
    public GameConfig chooseAnswer;
    public GameConfig verticalOperations;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

[System.Serializable]
public class GameConfig
{
    public int maxNumberRange;
    public int numOperations;
    public int order;
    public int requiredCorrectAnswersMinimumPercent;
}

