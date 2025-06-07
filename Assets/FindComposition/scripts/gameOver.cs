using UnityEngine;

public class gameOver : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject exitButton;
    public GameObject gameOverButton;

    private ScoreDelivering ScoreDeliveringRef;
    private Score_Handling ScoreHandlerRef;
    void Start()
    {
        ScoreDeliveringRef = GameObject.Find("player_ship").GetComponent<ScoreDelivering>();
        ScoreHandlerRef = GameObject.Find("player_ship").GetComponent<Score_Handling>();
    }

    void Update()
    {
        
    }

    public void kill()
    {
        Destroy(gameObject);
        gameOverButton.SetActive(true);
        Time.timeScale = 0f; // Pauses physics and animations
        Debug.Log("Game Over!");

        ScoreDeliveringRef.deliverScore(ScoreHandlerRef.score);

        restartButton.SetActive(true);
        exitButton.SetActive(true);

    }
}
