using UnityEngine;

public class gameOver : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject exitButton;
    public GameObject gameOverButton;
    void Start()
    {
        
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

        restartButton.SetActive(true);
        exitButton.SetActive(true);

    }
}
