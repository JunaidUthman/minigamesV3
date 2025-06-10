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

            // Display the time in mm:ss format
            int minutes = Mathf.FloorToInt(t / 60f);
            int seconds = Mathf.FloorToInt(t % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (t == 1)
            {
                // this is not working for now
                Debug.Log("hana dkhelt");
                int lastScore = sc.playerScore;

                Debug.Log("here is the lastScore "+ lastScore);
                ScoreDeliveringRef.deliverScore(lastScore);
            }
        }
    }
}
