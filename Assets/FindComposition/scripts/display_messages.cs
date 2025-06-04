using UnityEngine;
using TMPro;

public class display_messages : MonoBehaviour
{
    public TextMeshProUGUI helloText;
    public TextMeshProUGUI askText;
    public TextMeshProUGUI guideText;

    private float timer = 0f;

    void Start()
    {
        helloText.gameObject.SetActive(true);
        askText.gameObject.SetActive(false);
        guideText.gameObject.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer <= 4f)
        {
            helloText.gameObject.SetActive(true);
            askText.gameObject.SetActive(false);
            guideText.gameObject.SetActive(false);
        }
        else if (timer <= 8f)
        {
            helloText.gameObject.SetActive(true);
            askText.gameObject.SetActive(true);
            guideText.gameObject.SetActive(false);
        }
        else if (timer <= 13f)
        {
            helloText.gameObject.SetActive(false);
            askText.gameObject.SetActive(false);
            guideText.gameObject.SetActive(true);
        }
        else
        {
            helloText.gameObject.SetActive(false);
            askText.gameObject.SetActive(false);
            guideText.gameObject.SetActive(false);
        }
    }
}
