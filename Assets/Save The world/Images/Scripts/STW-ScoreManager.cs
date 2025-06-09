using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton
    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre les niveaux
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }

    public int GetScore()
    {
        return score;
    }
}