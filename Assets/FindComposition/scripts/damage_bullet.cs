using UnityEngine;

public class damage_bullet : MonoBehaviour
{
    int wrongAnswerLayerNumber = 6;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == wrongAnswerLayerNumber && gameObject.name == "bullet(Clone)")
        {
            Destroy(gameObject);
        }

    }
}
