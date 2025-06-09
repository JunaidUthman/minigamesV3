// OpGen2 pour Level 2 - Gestionnaire principal avec trois champs vides
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class OpGen2 : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text dividendeText;
    public TMP_Text diviseurText;
    public TMP_Text quotientText;
    public TMP_Text resteText;
    public TMP_Text soustractionText;
    public TMP_Text[] optionTexts; // Les options à glisser



    private List<DropZone> activeDropZones = new List<DropZone>();
    private Dictionary<DropZone, string> correctAnswers = new Dictionary<DropZone, string>();

    void Start()
    {
        GenerateProblem();
    }

    void GenerateProblem()
    {
        // Nettoyer les dropzones précédentes
        //ClearAllDropZones();

        // Générer une division simple
        int diviseur = Random.Range(2, 10);
        int quotient = Random.Range(2, 15);
        int reste = Random.Range(0, diviseur - 1);
        int dividende = (quotient * diviseur) + reste;
        int soustraction = quotient * diviseur;

        // affecter les valeurs aux champs 
        dividendeText.text = dividende.ToString();
        diviseurText.text = diviseur.ToString();
        quotientText.text = quotient.ToString();
        resteText.text = reste.ToString();
        soustractionText.text = soustraction.ToString();

        // 3 champs à cacher 
        List<TMP_Text> availableFields = new List<TMP_Text>
        {
            quotientText,
            resteText,
            soustractionText
        };

        // Mélanger la liste
        //for (int i = 0; i < availableFields.Count; i++)
        //{
        //    TMP_Text temp = availableFields[i];
        //    int randomIndex = Random.Range(i, availableFields.Count);
        //    availableFields[i] = availableFields[randomIndex];
        //    availableFields[randomIndex] = temp;
        //}

        // Prendre les 2 premiers pour les cacher
        for (int i = 0; i < 2; i++)
        {
            string correctValue = availableFields[i].text;
            SetupDropZone(availableFields[i], correctValue);
        }

        // Générer les options avec les bonnes réponses
        GenerateOptions();
    }

    void SetupDropZone(TMP_Text textComponent, string correctAnswer)
    {
        GameObject textGO = textComponent.gameObject;
        Transform parent = textGO.transform.parent;

        // Ajouter la dropzone
        DropZone dropZone = parent.gameObject.AddComponent<DropZone>();
        dropZone.Initialize(textComponent);

        // Changer le texte en "?"
        textComponent.text = "?";

        // Effet visuel pour indiquer une zone de drop
        textComponent.color = Color.red;

        // Enregistrer la dropzone et sa bonne réponse
        activeDropZones.Add(dropZone);
        correctAnswers[dropZone] = correctAnswer;
    }

    void GenerateOptions()
    {
        List<string> allCorrectAnswers = new List<string>(correctAnswers.Values);
        List<string> options = new List<string>(allCorrectAnswers);

        // Ajouter 2 fausses réponses
        while (options.Count < 5)
        {
            string wrongAnswer;
            do
            {
                int baseValue = int.Parse(allCorrectAnswers[Random.Range(0, allCorrectAnswers.Count)]);
                wrongAnswer = (baseValue + Random.Range(-10, 11)).ToString();
            } while (options.Contains(wrongAnswer) || int.Parse(wrongAnswer) < 0);

            options.Add(wrongAnswer);
        }

        // Mélanger les options
        for (int i = 0; i < options.Count; i++)
        {
            string temp = options[i];
            int randomIndex = Random.Range(i, options.Count);
            options[i] = options[randomIndex];
            options[randomIndex] = temp;
        }

        // Assigner aux 5 TMP_Text dans optionTexts
        for (int i = 0; i < optionTexts.Length && i < options.Count; i++)
        {
            optionTexts[i].text = options[i];

            if (optionTexts[i].GetComponent<Draggable>() == null)
            {
                optionTexts[i].gameObject.AddComponent<Draggable>();
            }
        }
    }

    public void OnSubmitClicked()
    {
        CheckAnswers();
    }

    void CheckAnswers()
    {
        int correctCount = 0;
        bool allFilled = true;

        foreach (DropZone dropZone in activeDropZones)
        {
            if (dropZone.targetText.text == "?")
            {
                allFilled = false;
                break;
            }

            if (dropZone.targetText.text == correctAnswers[dropZone])
            {
                correctCount++;
                dropZone.targetText.color = Color.green;
            }
            else
            {
                dropZone.targetText.color = Color.red;
            }
        }

        if (!allFilled)
        {
            Debug.Log("Veuillez remplir tous les champs avant de soumettre !");
            return;
        }

        if (correctCount == activeDropZones.Count)
        {
            Debug.Log("Toutes les réponses sont correctes !");
        }
        else
        {
            Debug.Log("Certaines réponses sont incorrectes, essayez encore !");
        }
        ScoreManager.Instance.AddScore(correctCount);
        Debug.Log("score : "+ScoreManager.Instance.GetScore());

        Debug.Log($"Nombre de réponses correctes : {correctCount} sur {activeDropZones.Count}");
    }

    


    //void ClearAllDropZones()
    //{
    //    foreach (DropZone dropZone in activeDropZones)
    //    {
    //        if (dropZone != null)
    //        {
    //            dropZone.ClearDropZone();
    //            Destroy(dropZone);
    //        }
    //    }

    //    activeDropZones.Clear();
    //    correctAnswers.Clear();

    //    // Remettre toutes les options à leur place
    //    foreach (TMP_Text option in optionTexts)
    //    {
    //        Draggable draggable = option.GetComponent<Draggable>();
    //        if (draggable != null)
    //        {
    //            draggable.ReturnToOrigin();
    //        }
    //    }
    //}

    
}