using UnityEngine;
using TMPro;

public class STW_timeHandling : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private SW_ScoreDelivring SW_ScoreDelivringRef;


    void Start()
    {
        SW_ScoreDelivringRef = GameObject.Find("score_Delivring").GetComponent<SW_ScoreDelivring>();

    }

    void Update()
    {
        if (TimeManager.Instance != null)
        {
            float t = TimeManager.Instance.timeRemaining;


            int minutes = Mathf.FloorToInt(t / 60f);
            int seconds = Mathf.FloorToInt(t % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (t <= 0)
            {
                SW_ScoreDelivringRef.deliverScore(ScoreManager.Instance.GetScore());
            }
        }
    }
}
