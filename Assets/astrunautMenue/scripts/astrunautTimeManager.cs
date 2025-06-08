using UnityEngine;
using TMPro;

public class astrunautTimeManager : MonoBehaviour
{
    public TextMeshProUGUI timerText; 

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
}
