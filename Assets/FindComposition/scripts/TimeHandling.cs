using UnityEngine;
using TMPro;

public class TimeHandling : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private Score_Handling ScoreHandlerRef;
    private ScoreDelivering ScoreDeliveringRef;

    void Start()
    {
        ScoreHandlerRef = GameObject.Find("player_ship").GetComponent<Score_Handling>();
        ScoreDeliveringRef = GameObject.Find("player_ship").GetComponent<ScoreDelivering>();
    }

    void Update()
    {
        if (TimeManager.Instance != null)
        {
            float t = TimeManager.Instance.timeRemaining;

            // Display the time in mm:ss format
            int minutes = Mathf.FloorToInt(t / 60f);
            int seconds = Mathf.FloorToInt(t % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (t <=0 )
            {
                int lastScore = ScoreHandlerRef.score;
                ScoreDeliveringRef.deliverScore(lastScore);
            }
        }
    }
}
