using UnityEngine;
using UnityEngine.SceneManagement;

public class entringGames : MonoBehaviour
{
    public int myGame;
    public int simoGame;
    public int drag_drop;


    private bool galaxyButton = false;
    private bool planet1Button = false;
    private bool planet2Button = false;

    public GameObject galaxyButtonCanvas;
    public GameObject planet1ButtonCanvas;
    public GameObject planet2ButtonCanvas;

    void Start()
    {
        myGame = 0;
        simoGame = 2;
        drag_drop = 3;
        galaxyButtonCanvas.SetActive(false);
        planet1ButtonCanvas.SetActive(false);
        planet2ButtonCanvas.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // You can use tag to identify the planet
        if (other.gameObject.name == "planet1")
        {
            planet1ButtonCanvas.SetActive(true);
            Debug.Log("planet1");
            
            //SceneManager.LoadScene(1); // Replace 1 with the scene index of the mini-game
        }
        else if (other.gameObject.name == "planet2")
        {
            planet2ButtonCanvas.SetActive(true);
            Debug.Log("planet2");
            //SceneManager.LoadScene(2);
        }
        else if (other.gameObject.name == "galaxy")
        {
            galaxyButtonCanvas.SetActive(true);
            Debug.Log("the value of the galaxy button is" + galaxyButton);

            if (galaxyButton) SceneManager.LoadScene(1);
        }

    }

    void Update()
    {
        if (galaxyButton)
        {
            SceneManager.LoadScene(1);
        }
        else if (planet1Button)
        {
            SceneManager.LoadScene(1);
        }
        else if (planet2Button)
        {
            SceneManager.LoadScene(1);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "planet1")
        {
            planet1ButtonCanvas.SetActive(false);

        }
        else if (other.gameObject.name == "planet2")
        {
            planet2ButtonCanvas.SetActive(false);

        }
        else if (other.gameObject.name == "galaxy")
        {
            galaxyButtonCanvas.SetActive(false);

        }
    }


    public void OngalaxyButtonClick()
    {
        galaxyButton = true;
        Debug.Log("My Game button clicked!");
    }

    // Call this when the "Simo Game" button is clicked
    public void Onplanet1ButtonClick()
    {
        planet1Button = true;
        Debug.Log("Simo Game button clicked!");
    }

    // Call this when the "Drag & Drop Game" button is clicked
    public void Onplanet2ButtonClick()
    {
        planet2Button = true;
        Debug.Log("Drag & Drop Game button clicked!");
    }
}
