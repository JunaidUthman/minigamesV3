using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FindCompoGuide : MonoBehaviour
{
    public GameObject guidePanel;
    public TextMeshProUGUI guideText;
    public Button nextButton;
    public Image guideImage;
    public GameObject ExitButton;
    public GameObject menueButtons;

    private int currentIndex = 0;

    private string[] messages = new string[]
    {
    "Here you go!!",
    "Hit matching rocks to score points.",
    "You have 3 engines. A wrong hit costs one.",
    "Lose all engines = restart.",
    "Good luck!"
    };


    public Sprite[] guideImages;

    void Start()
    {
        menueButtons.SetActive(false);
        ExitButton.SetActive(true);
        guidePanel.SetActive(true);
        guideText.text = messages[currentIndex];
        guideImage.sprite = guideImages[currentIndex];
        nextButton.onClick.AddListener(ShowNextMessage);
        Time.timeScale = 0f;
    }

    void ShowNextMessage()
    {
        currentIndex++;
        if (currentIndex < messages.Length)
        {
            guideText.text = messages[currentIndex];
            guideImage.sprite = guideImages[currentIndex];
        }
        else
        {
            guidePanel.SetActive(false);
            ExitButton.SetActive(false);
            menueButtons.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    public void OnExitClicked()
    {
        guidePanel.SetActive(false);
        ExitButton.SetActive(false);
        menueButtons.SetActive(true);
        Time.timeScale = 1f;
    }
}
