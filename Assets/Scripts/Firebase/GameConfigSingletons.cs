using UnityEngine;



public class GameConfigManager : MonoBehaviour
{
    public static GameConfigManager Instance { get; private set; }

    public GameConfig findComposition;
    public GameConfig chooseAnswer;
    public GameConfig verticalOperations;

    public int findCompositionScore;
    public int chooseAnswerScore = 10;
    public int verticalOperationsScore= 5;

    private void Awake()
    {
        if (Instance != null) {
            Debug.Log("iwaaaa chi hed ra mkhdem had linstance");
            Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

[System.Serializable]
public class GameConfig
{
    public int maxNumberRange;
    public int numOperations;
    public int numComposition;
    public int order;
    public int requiredCorrectAnswersMinimumPercent;
    public int minNumberRange;
}

