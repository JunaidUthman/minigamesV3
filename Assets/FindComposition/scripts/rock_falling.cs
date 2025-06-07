using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;


public class rock_falling : MonoBehaviour
{
    //------------- Game attributes ------------------
    private int bulletLayer = 9;
    float fallingSpeed = 5f;

    private rockMovement rockmvt; 

    //============ db attributes ==================
    private List<string> minNumCompositions;
    private List<string> maxNumberRange;


    private List<string> rightAnswers;
    private List<string> wrongAnswers;
    public TextMeshPro displayedAnswer;
    string answer;

    void Start()
    {

        rockmvt = GameObject.Find("rock_generation").GetComponent<rockMovement>();

        int index = UnityEngine.Random.Range(0, rockmvt.RightdivisionCompositions.Count);

       
        rightAnswers = rockmvt.RightdivisionCompositions;
        wrongAnswers = rockmvt.WrongdivisionCompositions;


        if (gameObject.name == "right_rock Variant(Clone)")
        {
            answer = rightAnswers[index];
            displayedAnswer.text = answer;
        }
        else if (gameObject.name == "wrong_rock Variant(Clone)")
        {
            answer = wrongAnswers[index];
            displayedAnswer.text = answer;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == bulletLayer && gameObject.name == "wrong_rock Variant(Clone)")
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y -= fallingSpeed * Time.deltaTime;
        transform.position = pos;

        float minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0f, Mathf.Abs(Camera.main.transform.position.z) ) ).y;

        if (pos.y <= minY)
        {
            Destroy(gameObject);
        }
    }

    public static List<List<int>> GenerateDivisionCompositions(int target, int maxNumberRange, int requiredCount)
    {
        List<List<int>> results = new List<List<int>>();
        HashSet<string> seen = new HashSet<string>(); // To avoid duplicates

        void Backtrack(List<int> path, int currentValue)
        {
            if (path.Count > 1 && currentValue == target)
            {
                string key = string.Join(",", path);
                if (!seen.Contains(key))
                {
                    seen.Add(key);
                    results.Add(new List<int>(path));
                }
                return;
            }

            if (results.Count >= requiredCount || path.Count >= 6) return;

            for (int i = 1; i <= maxNumberRange; i++)
            {
                if (i == 0 || currentValue % i != 0) continue;

                path.Add(i);
                Backtrack(path, currentValue / i);
                path.RemoveAt(path.Count - 1);
            }
        }

        for (int start = 1; start <= maxNumberRange; start++)
        {
            Backtrack(new List<int> { start }, start);
        }


        // Return only the requested amount (or fewer if not enough found)
        return results.GetRange(0, Math.Min(requiredCount, results.Count));
    }
    
}
