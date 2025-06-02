using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GuideManager : MonoBehaviour
{
    public GameObject guidePanel;
    public TextMeshProUGUI guideText;
    public Button nextButton;
    public Image guideImage;
    public GameObject miniMap;

    private int currentIndex = 0;

    private string[] messages = new string[]
    {
        "Welcome, explorer!",
        "Do you want to play some games?",
        "Fly to one of the planets to begin!",
        "Good luck and have fun!"
    };

    public Sprite[] guideImages; 

    void Start()
    {
        miniMap.SetActive(false);
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
            miniMap.SetActive(true);
            Time.timeScale = 1f;
        }
    }
}
