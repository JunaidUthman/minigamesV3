using UnityEngine;

public class LOBY_TestEnding : MonoBehaviour
{
    public static LOBY_TestEnding Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void EndTest()
    {
        Debug.Log("Time is up! Ending the test from TestEnding.cs");
        Time.timeScale = 0f;

        //TODO :: log the game out or somthing
    }
}
