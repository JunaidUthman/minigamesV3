using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class astrunautGuide : MonoBehaviour
{
    public static astrunautGuide Instance; // Singleton

    public GameObject guidePanel;
    public TextMeshProUGUI guideText;
    public Button nextButton;
    public Image guideImage;
    //public GameObject ExitButton;
    public GameObject menueButtons;
    public GameObject timeButtons;

    private int currentIndex = 0;
    private bool isSeen = false;

    private string[] messages = new string[]
    {
        "Boom! Your fire-ship just exploded!",
        "Now you're drifting solo through space!",
        "Use the space stones to move forward.",
        "But be careful — only pick the correct ones.",
        "Let the space quest begin. Good luck, pilot!"
    };

    public Sprite[] guideImages;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Avoid duplicates
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
            //ExitButton.SetActive(true);
            menueButtons.SetActive(false);
            timeButtons.SetActive(false);
            Time.timeScale = 0f;
        }
        else
        {
            // Hide guide if already shown
            guidePanel.SetActive(false);
            //ExitButton.SetActive(false);
            menueButtons.SetActive(true);
            timeButtons.SetActive(true);
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
        //ExitButton.SetActive(false);
        menueButtons.SetActive(true);
        timeButtons.SetActive(true);
        Time.timeScale = 1f;
    }
}
