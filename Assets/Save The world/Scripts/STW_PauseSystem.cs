using UnityEngine;
using UnityEngine.SceneManagement;
public class STW_PauseSystem : MonoBehaviour
{
    public GameObject PauseMenue;

    private SW_ScoreDelivring SW_ScoreDelivringRef;

    void Start()
    {
        SW_ScoreDelivringRef = GameObject.Find("score_Delivring").GetComponent<SW_ScoreDelivring>();

        PauseMenue.SetActive(false);
        Time.timeScale = 1f; // Ensure game is unpaused when starting
    }

    public void OnPauseClicked()
    {
        Time.timeScale = 0f; // Pause the game
        PauseMenue.SetActive(true); // Show the pause menu
    }

    public void OnPlayClicked()
    {
        PauseMenue.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f; // Resume the game
    }

    public void OnExitClicked()
    {
        Time.timeScale = 1f; // Make sure game is unpaused before leaving
        SceneManager.LoadScene(1); // Load desired scene

        SW_ScoreDelivringRef.deliverScore(ScoreManager.Instance.GetScore());
    }
}
