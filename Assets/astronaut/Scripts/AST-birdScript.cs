using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.InputSystem;

public class birdScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public Rigidbody2D body;
    public float flipStrenght;
    public ScoreCacul sc;
    public ChancesManager ch;

    public bool birdIsAlive=true;
    public GameObject fly;


    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();

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

        if (transform.position.y > topLimit || transform.position.y < bottomLimit)
        {
            sc.GameOver();
            birdIsAlive = false;
        }
    }

    void OnJump()
    {
        if (!birdIsAlive) return;

        body.linearVelocity = new Vector2(body.linearVelocity.x, flipStrenght);
        StartCoroutine(ActivateFlyTemporarily());
    }




    //void Start()
    //{
    //    sc = GameObject.FindGameObjectWithTag("ScoreLogic").GetComponent<ScoreCacul>();
    //    ch = GameObject.Find("Life Manager").GetComponent<ChancesManager>();

    //    fly = transform.Find("fly")?.gameObject;
    //    if (fly == null)
    //    {
    //        Debug.LogError("L'objet 'fly' n'a pas �t� trouv� dans 'bird'.");
    //    }

    //}

    //// Update is called once per frame
    //[System.Obsolete]
    //void Update()
    //{


    //    if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive){
    //    //body.linearVelocity = Vector2.up * flipStrenght;
    //        body.velocity = new Vector2(body.velocity.x, flipStrenght);
    //        StartCoroutine(ActivateFlyTemporarily());
    //    }
    //    // ?? V�rifie si l'oiseau sort de l'�cran
    //    float topLimit = Camera.main.orthographicSize;
    //    float bottomLimit = -Camera.main.orthographicSize;

    //    if (transform.position.y > topLimit || transform.position.y < bottomLimit)
    //    {
    //        sc.GameOver();
    //        birdIsAlive = false;
    //    }

    //}




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
                    birdIsAlive = false;
                }

            }
        }
    }


    IEnumerator ActivateFlyTemporarily()
    {
        fly.SetActive(true);
        yield return new WaitForSeconds(0.1f); // attendre 0.1 seconde
        fly.SetActive(false);
    }

}
