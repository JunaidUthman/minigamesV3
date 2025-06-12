using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("im about to go to scean 2 !!!!!!!!!!!!!!!");
        SceneManager.LoadSceneAsync(2);
    }


    public void ExitMenue()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
