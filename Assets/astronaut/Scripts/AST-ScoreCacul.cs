using UnityEngine;
using UnityEngine.UI;//pour utiliser UI object
using UnityEngine.SceneManagement;
using TMPro;
public class ScoreCacul : MonoBehaviour
{

    public int playerScore;
    public TextMeshPro scoreText;
    public GameObject GameOverScreen;

    [ContextMenu("increase")]
    public void addScore()
    {
        playerScore += 1;
        scoreText.text = playerScore.ToString();
    }

    public void RestarGame()
    {
        Debug.Log("Restarting Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        GameOverScreen.SetActive(true);

    }
}
