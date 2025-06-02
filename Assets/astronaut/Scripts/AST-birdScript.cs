using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
public class birdScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public Rigidbody2D body;
    public float flipStrenght;
    public ScoreCacul sc;
    public ChancesManager ch;

    public bool birdIsAlive=true;
    public GameObject fly;
    void Start()
    {
        sc = GameObject.FindGameObjectWithTag("ScoreLogic").GetComponent<ScoreCacul>();
        ch = GameObject.Find("Life Manager").GetComponent<ChancesManager>();

        fly = transform.Find("fly")?.gameObject;
        if (fly == null)
        {
            Debug.LogError("L'objet 'fly' n'a pas été trouvé dans 'bird'.");
        }

    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive){
        //body.linearVelocity = Vector2.up * flipStrenght;
            body.velocity = new Vector2(body.velocity.x, flipStrenght);
            StartCoroutine(ActivateFlyTemporarily());
        }
        // ?? Vérifie si l'oiseau sort de l'écran
        float topLimit = Camera.main.orthographicSize;
        float bottomLimit = -Camera.main.orthographicSize;

        if (transform.position.y > topLimit || transform.position.y < bottomLimit)
        {
            sc.GameOver();
            birdIsAlive = false;
        }

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
