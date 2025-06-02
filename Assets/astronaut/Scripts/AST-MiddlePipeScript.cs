using UnityEngine;

public class MiddlePipeScript : MonoBehaviour
{
    public ScoreCacul sc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sc = GameObject.FindGameObjectWithTag("ScoreLogic").GetComponent<ScoreCacul>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3) { 
        sc.addScore();
        }
    }
}
