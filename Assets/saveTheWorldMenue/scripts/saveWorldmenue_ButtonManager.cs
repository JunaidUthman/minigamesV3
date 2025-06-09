using UnityEngine;
using UnityEngine.SceneManagement;

public class saveWorldmenue_ButtonManager : MonoBehaviour
{
    public void OnPlayeClicked()
    {
        SceneManager.LoadScene(9);
    }

    public void OnExitClicked()
    {
        SceneManager.LoadScene(1);
    }
}
