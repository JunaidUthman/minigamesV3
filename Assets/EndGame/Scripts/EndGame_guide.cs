using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGame_guide : MonoBehaviour
{
    public GameObject guidePanel;
    public TextMeshProUGUI guideText;
    public Button nextButton;
    public Image guideImage;

    private int currentIndex = 0;

    private string[] messages = new string[]
{
    "Well done, Champ! You crushed the game!",
    "Can’t wait to see you back for the next adventure!"
};


    public Sprite[] guideImages;

    void Start()
    {
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
            Time.timeScale = 1f;
        }
    }
}
