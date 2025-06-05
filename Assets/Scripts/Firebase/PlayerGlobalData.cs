using UnityEngine;

public class PlayerGlobalData : MonoBehaviour
{
    public static PlayerGlobalData Instance { get; private set; }

    public string id;
    public int coins;
    public int mathLevel;
    public string testId;

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
