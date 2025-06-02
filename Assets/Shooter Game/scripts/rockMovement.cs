using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class rockMovement : MonoBehaviour
{
    //==========Rocks==========
   public GameObject WrongRock;
   public GameObject RigthRock;
   public float timeBeforeInstantiantion;

   public List<string> RightdivisionCompositions;
   public List<string> WrongdivisionCompositions;


    int target;
    int maxRange = 10;
    int minCompositions = 7;

    public int[] targets;

    public TMP_Text targetText;


    void Start()
    {
        targets = generateTargets(maxRange);
        target = targets[0];

        timeBeforeInstantiantion = 1f;

        RightdivisionCompositions = DivisionCompositionGenerator.GenerateRightDivisionCompositionsAsText(target, maxRange, minCompositions);
        WrongdivisionCompositions = DivisionCompositionGenerator.GenerateWrongDivisionCompositionsAsText(target, maxRange -1, minCompositions);

        targetText.text = "target :"+target;

    }

    void Update()
    {
        timeBeforeInstantiantion -= Time.deltaTime;
        if (timeBeforeInstantiantion <= 0)
        {

            generateRock();
            timeBeforeInstantiantion = 1f;
        }
    }

    void generateRock()
    {
        float randomX = Random.Range(0.2f, 0.8f);

        int number = UnityEngine.Random.Range(1, 3);
        if (number == 1)
        {
            

            Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(randomX, 1f, Mathf.Abs(Camera.main.transform.position.z)));
            spawnPoint.y += 2;
            GameObject newRock = Instantiate(RigthRock, spawnPoint, Quaternion.identity);
        }
        else if (number == 2)
        {
            

            Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(randomX, 1f, Mathf.Abs(Camera.main.transform.position.z)));
            spawnPoint.y += 2;
            GameObject newRock = Instantiate(WrongRock, spawnPoint, Quaternion.identity);
        }
        

    }

    int[] generateTargets(int maxRange)
    {
        int[] targets = new int[maxRange];
        for (int i=0; i<maxRange; i++)
        {
            targets[i] = i + 1;
        }
        return targets;
    }

    //void handelDistruction(GameObject newRock)
    //{
    //    Vector3 pos = newRock.transform.position;
    //    pos.y -= fallingSpeed;
    //    transform.position = pos;

    //    float minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0f, Mathf.Abs(Camera.main.transform.position.z))).y;

    //    if (pos.y <= minY)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
