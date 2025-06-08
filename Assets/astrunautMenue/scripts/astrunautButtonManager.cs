using UnityEngine;
using UnityEngine.SceneManagement;
public class astrunautButtonManager : MonoBehaviour
{
    public void OnPlayeClicked()
    {
        SceneManager.LoadScene(7);
    }

    public void OnExitClicked()
    {
        SceneManager.LoadScene(1);
    }
}
