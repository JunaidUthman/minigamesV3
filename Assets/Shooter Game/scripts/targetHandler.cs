using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class targetHandler : MonoBehaviour
{
    private Score_Handling ScoreHandlerRef;
    private rockMovement RockMovementRef;
    private rock_falling RockFallingRef;

    int maxRange = 10;
    int minCompositions = 7;

    int index = 0;
    int newTarget;

    private List<string> rightAnswers;
    private List<string> wrongAnswers;

    public TMP_Text targetText;

    private bool level2 = false;
    private bool level3 = false;
    private bool level4 = false;

    void Start()
    {
        ScoreHandlerRef = GameObject.Find("player_ship").GetComponent<Score_Handling>();
        RockMovementRef = GameObject.Find("rock_generation").GetComponent<rockMovement>();
        //RockFallingRef = GameObject.Find("rock_falling").GetComponent<rock_falling>();

    }

    void Update()
    {
        if (ScoreHandlerRef.score >= 5 && ScoreHandlerRef.score < 10 && !level2)
        {
            level2 = true;
            Debug.Log("level 2");
            index++;
            newTarget = RockMovementRef.targets[index];

            rightAnswers = DivisionCompositionGenerator.GenerateRightDivisionCompositionsAsText(newTarget, maxRange, minCompositions);
            Debug.Log(string.Join(", ", rightAnswers));
            wrongAnswers = DivisionCompositionGenerator.GenerateWrongDivisionCompositionsAsText(newTarget, maxRange, minCompositions);

            RockMovementRef.RightdivisionCompositions = rightAnswers;
            RockMovementRef.WrongdivisionCompositions = wrongAnswers;

            targetText.text = "target :" + newTarget;
        }

        else if (ScoreHandlerRef.score >= 10 && ScoreHandlerRef.score < 17 && !level3)
        {
            level3 = true;
            //Debug.Log("the target should be updated now");
            index++;
            newTarget = RockMovementRef.targets[index];

            rightAnswers = DivisionCompositionGenerator.GenerateRightDivisionCompositionsAsText(newTarget, maxRange, minCompositions);
            Debug.Log(string.Join(", ", rightAnswers));
            wrongAnswers = DivisionCompositionGenerator.GenerateWrongDivisionCompositionsAsText(newTarget, maxRange -1, minCompositions);

            RockMovementRef.RightdivisionCompositions = rightAnswers;
            RockMovementRef.WrongdivisionCompositions = wrongAnswers;

            targetText.text = "target :" + newTarget;
        }

        else if(ScoreHandlerRef.score >= 27 && !level4)
        {
            Debug.Log("hello , im in level 3");
            level4 = true;
            //Debug.Log("the target should be updated now");
            index++;
            newTarget = RockMovementRef.targets[index];

            rightAnswers = DivisionCompositionGenerator.GenerateRightDivisionCompositionsAsText(newTarget, maxRange, minCompositions);

            Debug.Log(string.Join(", ", rightAnswers));
            wrongAnswers = DivisionCompositionGenerator.GenerateWrongDivisionCompositionsAsText(newTarget, maxRange, minCompositions);

            RockMovementRef.RightdivisionCompositions = rightAnswers;
            RockMovementRef.WrongdivisionCompositions = wrongAnswers;

            targetText.text = "target :" + newTarget;
        }
    }
}
