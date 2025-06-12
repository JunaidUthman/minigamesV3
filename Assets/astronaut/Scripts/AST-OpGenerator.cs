using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OpGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshPro OperationText;
     int fakeResult;

    // db data 

    private int minNumberRange;
    private int maxNumberRange;
    //public TextMeshPro topOptionText;
    //public TextMeshPro bottomOptionText;

    void Start()
    {
        minNumberRange = GameConfigManager.Instance.chooseAnswer.minNumberRange;
        Debug.Log("GameConfigManager.Instance.chooseAnswer.minNumberRange =" + GameConfigManager.Instance.chooseAnswer.minNumberRange);
        maxNumberRange = GameConfigManager.Instance.chooseAnswer.maxNumberRange;
        Debug.Log("GameConfigManager.Instance.chooseAnswer.maxNumberRange =" + GameConfigManager.Instance.chooseAnswer.maxNumberRange);
    }




    public void GeneratorOperation(TextMeshPro topOptionText, TextMeshPro bottomOptionText) { 
       int a = Random.Range(1, 10);
       int b = Random.Range(1, a+1);
       int correctResult = a / b;
       OperationText.text = a + " / " + b;
       
        do
        {
            fakeResult = correctResult + Random.Range(-3, 4);
        } while (fakeResult == correctResult || fakeResult < 0);

        // Randomiser l'ordre d'affichage des réponses
        if (Random.value < 0.5f)
        {
            topOptionText.text = correctResult.ToString();
            bottomOptionText.text = fakeResult.ToString();
            // Assurez-vous que le script AnswerOption est attaché à ces objets
            topOptionText.GetComponent<AnswerOption>().isCorrect = true;
            bottomOptionText.GetComponent<AnswerOption>().isCorrect = false;

        }
        else
        {
            topOptionText.text = fakeResult.ToString();
            bottomOptionText.text = correctResult.ToString();
            
            topOptionText.GetComponent<AnswerOption>().isCorrect = false;
            bottomOptionText.GetComponent<AnswerOption>().isCorrect = true;

        }




    }


}
