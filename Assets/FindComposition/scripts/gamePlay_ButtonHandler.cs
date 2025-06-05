using UnityEngine;
using UnityEngine.SceneManagement;

public class gamePlay_ButtonHandler : MonoBehaviour
{
    public GameObject PauseMenueRef;
    public void replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(2);
    }


    public void exitToMenue()
    {
        Time.timeScale = 1f;
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
