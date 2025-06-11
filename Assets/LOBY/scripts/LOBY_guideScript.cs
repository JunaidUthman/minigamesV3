using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GuideManager : MonoBehaviour
{
    public static GuideManager Instance; // Singleton reference

    public GameObject guidePanel;
    public TextMeshProUGUI guideText;
    public Button nextButton;
    public Image guideImage;
    public GameObject miniMap;
    //public GameObject ExitButton;

    private int currentIndex = 0;

    private string[] messages = new string[]
    {
        "Welcome, explorer!",
        "Do you want to play some games?",
        "Fly to one of the planets to begin!",
        "Good luck and have fun!"
    };

    public Sprite[] guideImages;

    private bool isSeen = false;

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
            Destroy(gameObject); // Destroy any new duplicates
            return;
        }
    }

    void Start()
    {
        if (!isSeen)
        {
            isSeen = true;
            miniMap.SetActive(false);
            //ExitButton.SetActive(true);
            guidePanel.SetActive(true);
            guideText.text = messages[currentIndex];
            guideImage.sprite = guideImages[currentIndex];
            nextButton.onClick.AddListener(ShowNextMessage);
            Time.timeScale = 0f;
        }
        else
        {
            guidePanel.SetActive(false);
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
            guidePanel.SetActive(false);
            //ExitButton.SetActive(false);
            miniMap.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    public void OnExitClicked()
    {
        guidePanel.SetActive(false);
        miniMap.SetActive(true);
        //ExitButton.SetActive(false);
        Time.timeScale = 1f;
    }
}
