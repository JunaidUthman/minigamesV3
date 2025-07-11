using UnityEngine;
using TMPro;


public class AST_timeHandling : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private ASTScoreDelivring ScoreDeliveringRef;

    private ScoreCacul sc;

    void Start()
    {
        ScoreDeliveringRef = GameObject.Find("scoreDelivring").GetComponent<ASTScoreDelivring>();

        sc = GameObject.FindGameObjectWithTag("ScoreLogic").GetComponent<ScoreCacul>();
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

                int lastScore = sc.playerScore;


                ScoreDeliveringRef.deliverScore(lastScore);
            }
        }
    }
}
