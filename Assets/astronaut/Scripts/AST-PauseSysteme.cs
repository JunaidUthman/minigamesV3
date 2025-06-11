using UnityEngine;
using UnityEngine.SceneManagement;

public class AST_PauseSysteme : MonoBehaviour
{

    private ASTScoreDelivring ScoreDeliveringRef;

    private ScoreCacul sc;

    public GameObject PauseMenue;

    void Start()
    {
        ScoreDeliveringRef = GameObject.Find("scoreDelivring").GetComponent<ASTScoreDelivring>();

        sc = GameObject.FindGameObjectWithTag("ScoreLogic").GetComponent<ScoreCacul>();

        PauseMenue.SetActive(false);
        Time.timeScale = 1f; 
    }

    public void OnPauseClicked()
    {
        Time.timeScale = 0f; 
        PauseMenue.SetActive(true); 
    }

    public void OnPlayClicked()
    {
        PauseMenue.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnExitClicked()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(1); 


        int lastScore = sc.playerScore;

        ScoreDeliveringRef.deliverScore(lastScore);
    }
}
