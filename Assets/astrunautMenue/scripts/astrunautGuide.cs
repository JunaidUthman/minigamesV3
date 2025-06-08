using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class astrunautGuide : MonoBehaviour
{
    public GameObject guidePanel;
    public TextMeshProUGUI guideText;
    public Button nextButton;
    public Image guideImage;
    public GameObject ExitButton;
    public GameObject menueButtons;
    public GameObject timeButtons;

    private int currentIndex = 0;

    private string[] messages = new string[]
{
    "Boom! Your fire-ship just exploded!",
    "Now you're drifting solo through space!",
    "Use the space stones to move forward.",
    "But be careful — only pick the correct ones.",
    "Let the space quest begin. Good luck, pilot!"
};



    public Sprite[] guideImages;

    void Start()
    {
        menueButtons.SetActive(false);
        ExitButton.SetActive(true);
        guidePanel.SetActive(true);
        timeButtons.SetActive(false);
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
            timeButtons.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    public void OnExitClicked()
    {
        guidePanel.SetActive(false);
        ExitButton.SetActive(false);
        menueButtons.SetActive(true);
        timeButtons.SetActive(true);
        Time.timeScale = 1f;
    }
}
