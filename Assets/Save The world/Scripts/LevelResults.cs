using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels; // les GameObjects de chaque level
    private int currentLevelIndex = 0;

    public GameObject resultPanel; // panel à afficher après submit
    public TMP_Text resultText; // texte à mettre à jour
    public Button nextButton; // bouton Next
    public GameObject gameOverPanel;
    public Button exit; // bouton Next
    public TMP_Text ScoreTotal; // texte à mettre à jour

    private void Start()
    {
        // Démarrer au niveau 0
        ShowLevel(0);
        resultPanel.SetActive(false);
        gameOverPanel.SetActive(false); 


        nextButton.onClick.AddListener(NextLevel);
        exit.onClick.AddListener(QuitGame); // Quitter l'application
    }
    public void QuitGame()
    {
        Debug.Log("Quitter le jeu...");
        SceneManager.LoadScene(1);
    }
    public void ShowScore(int score)
    {
        ScoreTotal.text = $"Score Total : {score}";
    }
    public void ShowLevel(int index)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i == index);
        }
    }

    public void ShowResult(int correctCount, int totalAnswers)
    {
        // Désactiver le niveau courant
        if (currentLevelIndex >= 0 && currentLevelIndex < levels.Length)
        {
            levels[currentLevelIndex].SetActive(false); // 👈 Désactivation explicite
        }

        // Afficher le panneau de résultats
        resultText.text = $"Réponses correctes : {correctCount} / {totalAnswers}";
        resultPanel.SetActive(true);

        // Afficher le bouton "Next" seulement s'il reste des niveaux
        nextButton.gameObject.SetActive(currentLevelIndex < levels.Length - 1);
    }

    public void NextLevel()
    {
        resultPanel.SetActive(false);
        currentLevelIndex++;

        if (currentLevelIndex < levels.Length-1)
        {
            ShowLevel(currentLevelIndex);
        }
        else
        {
            Debug.Log("Fin du jeu !");
            gameOverPanel.SetActive(true); // 👈 Affiche le panneau Game Over
        }
    }
}
