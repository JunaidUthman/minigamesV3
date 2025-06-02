using UnityEngine;
using UnityEngine.UIElements;

public class SpawnerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject pipe;
    public float spownrate = 2;//2 seconde entre chaque pipe 
    private float timer = 0;
    public float heightOffSet = 10;
    void Start()
    {
        spawnePipe();

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spownrate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnePipe();
            timer = 0;
        }

    }
    void spawnePipe()
    {
        float lowestpoint = transform.position.y + heightOffSet ;
        float heighestpoint= transform.position.y - heightOffSet;
        Instantiate(pipe, new Vector3(transform.position.x,Random.Range(lowestpoint,heighestpoint),0), transform.rotation);

    }
}
