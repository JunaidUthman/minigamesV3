using UnityEngine;
using TMPro;

public class displayOperation : MonoBehaviour
{
    public TextMeshPro score;
    int counter;
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        score.text = counter.ToString();
    }
}
