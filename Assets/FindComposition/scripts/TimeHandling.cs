using UnityEngine;
using TMPro;

public class TimeHandling : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Drag the TextMeshProUGUI from your Canvas here

    void Update()
    {
        if (TimeManager.Instance != null)
        {
            float t = TimeManager.Instance.timeRemaining;

            // Display the time in mm:ss format
            int minutes = Mathf.FloorToInt(t / 60f);
            int seconds = Mathf.FloorToInt(t % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
