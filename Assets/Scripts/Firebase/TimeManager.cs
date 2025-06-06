using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    public float timeRemaining;
    public bool timerIsRunning = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (timerIsRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerIsRunning = false;
                EndTest();
            }
        }
    }

    public void StartTimer(float durationInSeconds)
    {
        timeRemaining = durationInSeconds;
        timerIsRunning = true;
    }

    private void EndTest()
    {
        Debug.Log("Time is up. Test ends.");
        Time.timeScale = 0f;
        // TODO :: call a function to calculate the the score of the tree games , and store it as a math level in the dataBase
    }
}
