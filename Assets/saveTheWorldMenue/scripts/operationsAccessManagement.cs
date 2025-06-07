using UnityEngine;
using UnityEngine.SceneManagement;

public class operationsAccessManagement : MonoBehaviour
{
    // Called when Operation 1 button is clicked
    public void OnClick_Operation1()
    {
        //SceneManager.LoadScene(4); // Always allowed
        Debug.Log("op 1 is clicked , ");
    }

    public void OnClick_Operation2()
    {
        if (operationManager.Instance != null && operationManager.Instance.operation2)
        {
            //SceneManager.LoadScene(5); // Change to your actual scene index or name
            Debug.Log("op 2 is clicked , ");
        }
        else
        {
            Debug.Log("Operation 2 is locked.");
        }
    }

    public void OnClick_Operation3()
    {
        if (operationManager.Instance != null && operationManager.Instance.operation3)
        {
            //SceneManager.LoadScene(6); // Change to your actual scene index or name
            Debug.Log("op 3 is clicked , ");
        }
        else
        {
            Debug.Log("Operation 3 is locked.");
        }
    }

    public void OnClick_Operation4()
    {
        if (operationManager.Instance != null && operationManager.Instance.operation4)
        {
            //SceneManager.LoadScene(7); // Change to your actual scene index or name
            Debug.Log("op 4 is clicked , ");
        }
        else
        {
            Debug.Log("Operation 4 is locked.");
        }
    }

    public void OnExitClicked()
    {
        SceneManager.LoadScene(5);
    }
}
