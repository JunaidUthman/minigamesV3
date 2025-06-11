using UnityEngine;
using UnityEngine.SceneManagement;

public class gamePlay_ButtonHandler : MonoBehaviour
{

    private ScoreDelivering ScoreDeliveringRef;
    private Score_Handling ScoreHandlerRef;

    public GameObject PauseMenueRef;
    public GameObject fire;
    public GameObject left_Rigth;

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
        
        ScoreDeliveringRef.deliverScore(ScoreHandlerRef.score);
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(1);
    }

    public void pause()
    {
        fire.SetActive(false);
        left_Rigth.SetActive(false);
        Debug.Log("pauuuuuuuse");
        Time.timeScale = 0f;
        PauseMenueRef.SetActive(true);
    }

    public void resume()
    {
        fire.SetActive(true);
        left_Rigth.SetActive(true);
        Debug.Log("resuuume");
        Time.timeScale = 1f;
        PauseMenueRef.SetActive(false);
    }
}
