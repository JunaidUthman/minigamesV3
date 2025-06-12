using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoP3 : MonoBehaviour
{


    [Header("UI Elements")]
    public TMP_Text dividendeText;
    public TMP_Text diviseurText;
    public TMP_Text quotientText;
    public TMP_Text resteText;
    public TMP_Text soustr1Text;
    public TMP_Text soustrResultText;
    public TMP_Text soustr2Text;
    public TMP_Text[] optionTexts; // Les options � glisser
    private SW_ScoreDelivring SW_ScoreDelivringRef;
    public LevelManager levelManager; // � assigner dans l�inspecteur




    private List<DropZone> activeDropZones = new List<DropZone>();
    private Dictionary<DropZone, string> correctAnswers = new Dictionary<DropZone, string>();

    public bool isRight = false;

    // db variables
    private int minNumberRange;
    private int maxNumberRange;


    void Start()
    {
        SW_ScoreDelivringRef = GameObject.Find("score_Delivring").GetComponent<SW_ScoreDelivring>();

        //minNumberRange = GameConfigManager.Instance.verticalOperations.minNumberRange;
        //Debug.Log("GameConfigManager.Instance.verticalOperations.minNumberRange in Operation4 = " + minNumberRange);
        //maxNumberRange = GameConfigManager.Instance.verticalOperations.maxNumberRange;
        //Debug.Log("GameConfigManager.Instance.verticalOperations.maxNumberRange in Operation4 = " + maxNumberRange);
        if (levelManager == null)
            levelManager = FindObjectOfType<LevelManager>();
        GenerateProblem();
    }


    void GenerateProblem()
    {
        // Nettoyer les dropzones pr�c�dentes
        //ClearAllDropZones();

        // �TAPE 1: G�n�rer les composants de base de mani�re coh�rente
        int diviseur = Random.Range(3, 12); // Diviseur entre 3 et 11

        // G�n�rer deux chiffres pour le quotient (pour avoir 2 soustractions)
        int chiffre1 = Random.Range(2, 9); // Premier chiffre du quotient
        int chiffre2 = Random.Range(1, 9); // Deuxi�me chiffre du quotient
        int quotient = chiffre1 * 10 + chiffre2; // Quotient � 2 chiffres

        // G�n�rer le reste final (doit �tre < diviseur)
        int resteFinal = Random.Range(0, diviseur);

        // �TAPE 2: Calculer le dividende de base
        int dividendeBase = quotient * diviseur + resteFinal;

        // �TAPE 3: D�terminer les �l�ments de la division verticale
        // Premier chiffre du quotient * diviseur = premi�re soustraction
        int soustr1 = chiffre1 * 10 * diviseur; // Ce qu'on soustrait en premier

        // Calculer ce qui reste apr�s la premi�re soustraction
        int resteApres1 = dividendeBase - soustr1;

        // S'assurer que le reste apr�s la premi�re soustraction est valide
        if (resteApres1 < 0)
        {
            // R�ajuster si n�cessaire
            chiffre1 = Random.Range(1, (dividendeBase / (diviseur * 10)) + 1);
            quotient = chiffre1 * 10 + chiffre2;
            soustr1 = chiffre1 * 10 * diviseur;
            resteApres1 = dividendeBase - soustr1;
            dividendeBase = quotient * diviseur + resteFinal; // Recalculer
            resteApres1 = dividendeBase - soustr1;
        }

        // Deuxi�me soustraction = chiffre2 * diviseur
        int soustr2 = chiffre2 * diviseur;

        // V�rification finale et ajustement si n�cessaire
        if (resteApres1 < soustr2)
        {
            // R�g�n�rer avec des valeurs plus petites
            chiffre1 = Random.Range(2, 6);
            chiffre2 = Random.Range(1, 6);
            quotient = chiffre1 * 10 + chiffre2;
            resteFinal = Random.Range(0, diviseur);
            dividendeBase = quotient * diviseur + resteFinal;
            soustr1 = chiffre1 * 10 * diviseur;
            soustr2 = chiffre2 * diviseur;
            resteApres1 = dividendeBase - soustr1;
        }

        // �TAPE 4: Calculs finaux
        int dividende = dividendeBase;
        int resteApresDeuxieme = resteApres1 - soustr2; // Doit �galer resteFinal

        // V�rification math�matique compl�te
        Debug.Log($"V�rification: {dividende} � {diviseur} = {quotient} reste {resteApresDeuxieme}");
        Debug.Log($"Premi�re soustraction: {dividende} - {soustr1} = {resteApres1}");
        Debug.Log($"Deuxi�me soustraction: {resteApres1} - {soustr2} = {resteApresDeuxieme}");

        // �TAPE 5: Assignation aux champs de texte
        diviseurText.text = diviseur.ToString();
        dividendeText.text = dividende.ToString();
        quotientText.text = quotient.ToString();
        resteText.text = resteApresDeuxieme.ToString();

        // �l�ments de premi�re soustraction
        soustr1Text.text = soustr1.ToString();
        soustrResultText.text = resteApres1.ToString(); // R�sultat apr�s premi�re soustraction

        // �l�ment de deuxi�me soustraction
        soustr2Text.text = soustr2.ToString();

        // �TAPE 6: S�lectionner 4 champs � cacher
        List<TMP_Text> availableFields = new List<TMP_Text>
    {
        quotientText,
        resteText,
        soustr1Text,
        soustrResultText,
        soustr2Text
    };

        // M�langer la liste
        for (int i = 0; i < availableFields.Count; i++)
        {
            TMP_Text temp = availableFields[i];
            int randomIndex = Random.Range(i, availableFields.Count);
            availableFields[i] = availableFields[randomIndex];
            availableFields[randomIndex] = temp;
        }

        // Prendre les 4 premiers pour les cacher
        for (int i = 0; i < 4; i++)
        {
            string correctValue = availableFields[i].text;
            SetupDropZone(availableFields[i], correctValue);
        }

        // G�n�rer les options avec les bonnes r�ponses
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

        // Enregistrer la dropzone et sa bonne r�ponse
        activeDropZones.Add(dropZone);
        correctAnswers[dropZone] = correctAnswer;
    }

    void GenerateOptions()
    {
        // Prendre les 4 bonnes r�ponses
        List<string> trueAnswers = new List<string>(correctAnswers.Values);
        List<string> allOptions = new List<string>(trueAnswers);

        //afficher les bonnes r�ponses dans les options
        for (int i = 0; i < trueAnswers.Count && i < optionTexts.Length; i++)
        {

            Debug.Log(trueAnswers[i]);

        }


        // Ajouter 2 mauvaises r�ponses diff�rentes
        while (allOptions.Count < 6)
        {
            int baseValue = int.Parse(trueAnswers[Random.Range(0, trueAnswers.Count)]);
            int wrong = baseValue + Random.Range(-10, 11);

            // S'assurer qu'elle est positive et non d�j� incluse
            if (wrong >= 0 && !allOptions.Contains(wrong.ToString()))
            {
                allOptions.Add(wrong.ToString());
            }
        }

        // M�langer les options
        for (int i = 0; i < allOptions.Count; i++)
        {
            int rand = Random.Range(i, allOptions.Count);
            (allOptions[i], allOptions[rand]) = (allOptions[rand], allOptions[i]);
        }

        // Assigner aux textes d�option (suppos� avoir 6 TMP_Text)
        for (int i = 0; i < optionTexts.Length && i < allOptions.Count; i++)
        {
            optionTexts[i].text = allOptions[i];

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
            Debug.Log("Toutes les r�ponses sont correctes !");
            // junaid : i added this line so i can switch between canvas
            levelManager.ShowResult(correctCount, activeDropZones.Count);
            levelManager.ShowScore(ScoreManager.Instance.GetScore());
            SW_ScoreDelivringRef.deliverScore(ScoreManager.Instance.GetScore());


        }
        else
        {
            Debug.Log("Certaines r�ponses sont incorrectes, essayez encore !");
            levelManager.ShowResult(correctCount, activeDropZones.Count);
            levelManager.ShowScore(ScoreManager.Instance.GetScore());

        }
        ScoreManager.Instance.AddScore(correctCount);

        Debug.Log($"Nombre de r�ponses correctes : {correctCount} sur {activeDropZones.Count}");
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

    //    // Remettre toutes les options � leur place
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