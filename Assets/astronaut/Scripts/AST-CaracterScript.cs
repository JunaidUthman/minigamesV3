using UnityEngine;
using UnityEngine.InputSystem; // Nouveau syst�me d�input
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Caracter : MonoBehaviour
{
    public Rigidbody2D body;
    public float flipStrenght;
    public ScoreCacul sc;
    public ChancesManager ch;

    public bool birdIsAlive = true;
    public GameObject fly;

    private astronautControls controls;

    private ASTScoreDelivring ScoreDeliveringRef;
    //public StonesFactory stonesFactory;
    public StonesFactory stonesFactoryScript;
    public GameObject stonesFactoryObject;

    void Awake()
    {

        controls = new astronautControls();

        // Lier l'action "Jump" � une m�thode
        controls.Gameplay.Jump.performed += ctx => OnJump();
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Start()
    {


        if (stonesFactoryObject != null)
        {
            stonesFactoryScript = stonesFactoryObject.GetComponent<StonesFactory>();
            if (stonesFactoryScript == null)
            {
                Debug.LogError("StonesFactory script not found on the assigned GameObject!");
            }
        }
        else
        {
            Debug.LogError("StonesFactory GameObject not assigned!");
        }


        ScoreDeliveringRef = GameObject.Find("scoreDelivring").GetComponent<ASTScoreDelivring>();



        sc = GameObject.FindGameObjectWithTag("ScoreLogic").GetComponent<ScoreCacul>();
        ch = GameObject.Find("Life Manager").GetComponent<ChancesManager>();

        fly = transform.Find("fly")?.gameObject;
        if (fly == null)
        {
            Debug.LogError("L'objet 'fly' n'a pas �t� trouv� dans 'bird'.");
        }
    }

    void Update()
    {
        float topLimit = Camera.main.orthographicSize;
        float bottomLimit = -Camera.main.orthographicSize;

        float screenAspect = Camera.main.aspect;
        float leftLimit = -17;


        if (transform.position.y > topLimit ||
            transform.position.y < bottomLimit ||
            transform.position.x < leftLimit)
        {
            sc.GameOver();
            birdIsAlive = false;
            StopGeneratingStones(); 
        }

        if (sc.playerScore > 30)
        {
            birdIsAlive = false;
            StopGeneratingStones();
            ScoreDeliveringRef.deliverScore(sc.playerScore);
            SceneManager.LoadScene(1);
        }
    }



    public void StopGeneratingStones()
    {
        if (stonesFactoryScript != null)
        {

            stonesFactoryScript.SetGameActive(false);
            Debug.Log("Stone generation stopped - Bird is dead");
        }
        else
        {
            Debug.LogError("StonesFactory script reference is null!");
        }
    }


    public void OnJump()
    {
        if (!birdIsAlive) return;

        body.linearVelocity = new Vector2(body.linearVelocity.x, flipStrenght);
        StartCoroutine(ActivateFlyTemporarily());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        AnswerOption option = other.GetComponent<AnswerOption>();
        if (option != null)
        {
            if (option.isCorrect)
            {
                sc.addScore();
            }
            else
            {
                ch.LoseLife();
                if (ch.life == 0)
                {
                    sc.GameOver();
                    StopGeneratingStones();
                    birdIsAlive = false;
                }
            }
        }
    }

    IEnumerator ActivateFlyTemporarily()
    {
        fly.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        fly.SetActive(false);
    }
}
