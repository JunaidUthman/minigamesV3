using UnityEngine;

public class PlayerGlobalData : MonoBehaviour
{
    public static PlayerGlobalData Instance { get; private set; }

    public string id;
    public int coins;
    public int mathLevel;
    public int score;
    public string testId;
    public float gameDuration;

    public int FindCompositionScore =0;
    public int ChooseAnswerScore =0;
    public int SaveTheWorldScore =0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
