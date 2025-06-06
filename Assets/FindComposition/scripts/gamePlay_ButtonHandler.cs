using UnityEngine;
using UnityEngine.SceneManagement;

public class gamePlay_ButtonHandler : MonoBehaviour
{

    private ScoreDelivering ScoreDeliveringRef;
    private Score_Handling ScoreHandlerRef;

    public GameObject PauseMenueRef;

    void Start()
    {
        ScoreDeliveringRef = GameObject.Find("player_ship").GetComponent<ScoreDelivering>();
        ScoreHandlerRef = GameObject.Find("player_ship").GetComponent<Score_Handling>();
    }
    public void replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(2);
    }


    public void exitToMenue()
    {
        Time.timeScale = 1f;
        ScoreDeliveringRef.deliverScore(ScoreHandlerRef.score);
        SceneManager.LoadSceneAsync(3);
    }

    public void pause()
    {
        Debug.Log("pauuuuuuuse");
        Time.timeScale = 0f;
        PauseMenueRef.SetActive(true);
    }

    public void resume()
    {
        Debug.Log("resuuume");
        Time.timeScale = 1f;
        PauseMenueRef.SetActive(false);
    }
}
