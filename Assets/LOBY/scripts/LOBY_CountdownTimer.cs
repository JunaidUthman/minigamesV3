using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    private float timeRemaining;
    public TextMeshProUGUI timerText;
    private bool timerIsRunning = true;

    void Start()
    {
        
        StartCoroutine(WaitForPlayerGlobalData());
    }

    private IEnumerator WaitForPlayerGlobalData()
    {
        
        while (PlayerGlobalData.Instance == null)
        {
            
            yield return null; 
        }

        // Once ready, initialize timer
        timeRemaining = PlayerGlobalData.Instance.gameDuration * 60f;
        Debug.Log("PlayerGlobalData is ready, timer initialized with time: " + timeRemaining);
    }

    void Update()
    {
        if (TimeManager.Instance != null)
        {
            float t = TimeManager.Instance.timeRemaining;
            int minutes = Mathf.FloorToInt(t / 60f);
            int seconds = Mathf.FloorToInt(t % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }


    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
