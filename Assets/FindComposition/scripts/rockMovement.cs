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
    int maxRange = GameConfigManager.Instance.findComposition.maxNumberRange;
    int minCompositions = GameConfigManager.Instance.findComposition.numComposition;

    public int[] targets;

    public TMP_Text targetText;


    void Start()
    {
        if (GameConfigManager.Instance != null)
        {
            Debug.Log("linstance de GameConfigManager est  differante de null");
        }


        //Debug.Log("the maxCompo given from the db is " + maxRange);
        //Debug.Log("the minCompositions given from the db is " + minCompositions);
        targets = generateTargets(maxRange);
        target = targets[0];

        timeBeforeInstantiantion = 1f;

        Debug.Log("generating the answers of the rigth rock");
        RightdivisionCompositions = DivisionCompositionGenerator.GenerateRightDivisionCompositionsAsText(target, maxRange, minCompositions);

        Debug.Log("generating the answers of the wro,g rock");
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
        Debug.Log("generating the rocks have just bigen");
        float randomX = Random.Range(0.2f, 0.8f);

        int number = UnityEngine.Random.Range(1, 3);
        if (number == 1)
        {
            Debug.Log("rigth wrok generated");

            Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(randomX, 1f, Mathf.Abs(Camera.main.transform.position.z)));
            spawnPoint.y += 2;
            GameObject newRock = Instantiate(RigthRock, spawnPoint, Quaternion.identity);
        }
        else if (number == 2)
        {

            Debug.Log("wrong wrok generated");
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
