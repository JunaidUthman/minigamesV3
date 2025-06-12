using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FindCompoGuide : MonoBehaviour
{
    public static FindCompoGuide Instance; // Singleton

    public GameObject guidePanel;
    public TextMeshProUGUI guideText;
    public Button nextButton;
    public Image guideImage;
    public GameObject ExitButton;
    public GameObject menueButtons;

    private int currentIndex = 0;
    private bool isSeen = false;

    private string[] messages = new string[]
    {
        "Here you go!!",
        "Hit matching rocks to score points.",
        "You have 3 engines. A wrong hit costs one.",
        "Lose all engines = restart.",
        "Good luck!"
    };

    public Sprite[] guideImages;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }
    }

    void Start()
    {
        if (!isSeen)
        {
            isSeen = true;
            guidePanel.SetActive(true);
            guideText.text = messages[currentIndex];
            guideImage.sprite = guideImages[currentIndex];
            nextButton.onClick.AddListener(ShowNextMessage);
            ExitButton.SetActive(true);
            menueButtons.SetActive(false);
            Time.timeScale = 0f;
        }
        else
        {
            guidePanel.SetActive(false);
            ExitButton.SetActive(false);
            menueButtons.SetActive(true);
        }
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
            CloseGuide();
        }
    }

    public void OnExitClicked()
    {
        CloseGuide();
    }

    void CloseGuide()
    {
        guidePanel.SetActive(false);
        ExitButton.SetActive(false);
        menueButtons.SetActive(true);
        Time.timeScale = 1f;
    }
}
