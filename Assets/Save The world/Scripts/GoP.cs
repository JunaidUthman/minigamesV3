using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoP : MonoBehaviour
{
    public TMP_Text diviseurText;
    public TMP_Text dividendeText;
    public TMP_Text resultText;
    public TMP_Text soustractionText;
    public TMP_Text resteText;

    public GameObject diviseurGO;
    public GameObject dividendeGO;
    public GameObject resultGO;
    public GameObject soustractionGO;
    public GameObject resteGO;

    public bool isRight=false;

    //private int dividend;
    //private int divisor;
    //private int quotient;
    //private int reste;
    //private int sousProduit;

    [Header("Options de réponse")]
    public TMP_Text[] optionTexts; // Assigne Option1, Option2, Option3 dans l'inspecteur
    private string correctAnswer;
    private HiddenPart hidden;
    public enum HiddenPart { Result, Soustraction, Reste }
    private DropZone currentDropZone;

    private SW_ScoreDelivring SW_ScoreDelivringRef;

    // db variables
    private int minNumberRange;
    private int maxNumberRange;

    void Start()
    {
        SW_ScoreDelivringRef = GameObject.Find("score_Delivring").GetComponent<SW_ScoreDelivring>();

        minNumberRange = GameConfigManager.Instance.verticalOperations.minNumberRange;
        Debug.Log("GameConfigManager.Instance.verticalOperations.minNumberRange in Operation1 = " + minNumberRange);
        maxNumberRange = GameConfigManager.Instance.verticalOperations.maxNumberRange;
        Debug.Log("GameConfigManager.Instance.verticalOperations.maxNumberRange in Operation1 = " + maxNumberRange);


        GenerateDivision();
        HideRandomPart();
        GenerateOptions(correctAnswer);
    }

    void GenerateDivision()
    {
        //pour version DB
        int diviseur = Random.Range(minNumberRange, maxNumberRange);
        int result = Random.Range(minNumberRange, maxNumberRange);
        int reste = Random.Range(minNumberRange, diviseur); // reste < diviseur
        int produit = diviseur * result;
        int dividende = produit + reste;
        int soustraction = produit;




        //affecter les objets 
        diviseurText = diviseurGO.GetComponentInChildren<TMP_Text>();
        dividendeText = dividendeGO.GetComponentInChildren<TMP_Text>();
        resultText = resultGO.GetComponentInChildren<TMP_Text>();
        soustractionText = soustractionGO.GetComponentInChildren<TMP_Text>();
        resteText = resteGO.GetComponentInChildren<TMP_Text>();



        // Affecter les textes
        diviseurText.text = diviseur.ToString();
        dividendeText.text = dividende.ToString();
        resultText.text = result.ToString();
        soustractionText.text = soustraction.ToString();
        resteText.text = reste.ToString();
    }

    void HideRandomPart()
    {
        hidden =(HiddenPart)Random.Range(0, 3);

        switch (hidden)
        {
            case HiddenPart.Result:
                correctAnswer = resultText.text;
                SetupDropZone(resultText);
                //resultText.text = "?";
                //AddDropZone(resultText);
                ShowImage(resultText);
                break;

            case HiddenPart.Soustraction:
                correctAnswer = soustractionText.text;
                SetupDropZone(soustractionText);
                //soustractionText.text = "?";
                //AddDropZone(soustractionText);
                ShowImage(soustractionText);
                break;
            case HiddenPart.Reste:
                correctAnswer = resteText.text;
                SetupDropZone(resteText);
                //resteText.text = "?";
                //AddDropZone(resteText);
                ShowImage(resteText);
                break;
        }
    }
    void SetupDropZone(TMP_Text textComponent)
    {
        GameObject textGO = textComponent.gameObject;
        Transform parent = textGO.transform.parent;

        // Supprimer l'ancienne dropzone si elle existe
        if (currentDropZone != null)
        {
            Destroy(currentDropZone);
        }

        // Ajouter la nouvelle dropzone
        currentDropZone = parent.gameObject.AddComponent<DropZone>();
        currentDropZone.Initialize(textComponent);

        // Changer le texte en "?"
        textComponent.text = "?";

        // Optionnel : changer l'apparence pour indiquer que c'est une zone de drop
        ShowDropZoneVisual(textComponent);

    }

    void ShowDropZoneVisual(TMP_Text textComponent)
    {
        // Changer la couleur ou ajouter un effet visuel
        textComponent.color = Color.red;
        // Vous pouvez ajouter d'autres effets visuels ici
    }


    void GenerateOptions(string correct)
    {
        int correctValue = int.Parse(correct);
        int correctIndex = Random.Range(0, optionTexts.Length);

        for (int i = 0; i < optionTexts.Length; i++)
        {
            if (i == correctIndex)
            {
                optionTexts[i].text = correct;
            }
            else
            {
                int wrongAnswer;
                do
                {
                    wrongAnswer = correctValue + Random.Range(-5, 6);
                } while (wrongAnswer == correctValue || wrongAnswer < 0);
                optionTexts[i].text = wrongAnswer.ToString();
            }

            // S'assurer que chaque option a un composant Draggable
            if (optionTexts[i].GetComponent<Draggable>() == null)
            {
                optionTexts[i].gameObject.AddComponent<Draggable>();
            }
        }
    }

    //public string GetCorrectAnswer() => correctAnswer;
    //public HiddenPart GetHiddenPart() => hidden;

    //void AddDropZone(TMP_Text textComponent)
    //{
    //    GameObject textGO = textComponent.gameObject;
    //    Transform parent = textGO.transform.parent;

    //    // On ajoute DropZone au parent (pas directement au TMP_Text)
    //    if (parent.GetComponent<DropZone>() == null)
    //    {
    //        DropZone zone = parent.gameObject.AddComponent<DropZone>();
    //    }
    //}
    void ShowImage(TMP_Text textField)
    {
        Image img = textField.transform.parent.GetComponentInChildren<Image>(true); // true pour les objets inactifs
        if (img != null)
            img.gameObject.SetActive(true);
    }

    public void OnSubmitClicked()
    {
        CheckAnswer();
    }
   public void CheckAnswer()
    {
        if (currentDropZone != null && currentDropZone.targetText != null)
        {
            string placedAnswer = currentDropZone.targetText.text;

            // Vérifier si une réponse a été placée
            if (placedAnswer == "?")
            {
                Debug.Log("Veuillez placer une réponse avant de soumettre !");
                return;
            }

            if (placedAnswer == correctAnswer)
            {
                
                Debug.Log("good job");
                ScoreManager.Instance.AddScore(10);
                Debug.Log("score : " + ScoreManager.Instance.GetScore());
                // junaid : i added this lines so i can switch between canvas
                //isRight = true;
                //FindObjectOfType<OperationManager>().TryNextOperation(isRight);

                SW_ScoreDelivringRef.deliverScore(ScoreManager.Instance.GetScore());


            }
            else
            {
                Debug.Log("wrong answer");
            }
        }
    }


}
