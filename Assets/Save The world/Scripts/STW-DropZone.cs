using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DropZone : MonoBehaviour, IDropHandler
{
    [HideInInspector] public Draggable heldItem;
    [HideInInspector] public TMP_Text targetText; // Le texte "?" � remplacer
    [HideInInspector] public string originalText; // Texte original avant "?"

    public void Initialize(TMP_Text textComponent)
    {
        targetText = textComponent;
        originalText = textComponent.text;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObj = eventData.pointerDrag;
        if (droppedObj != null)
        {
            Draggable draggable = droppedObj.GetComponent<Draggable>();
            if (draggable != null)
            {
                // Si un item est d�j� pr�sent, on le remet dans sa zone d'origine
                if (heldItem != null)
                {
                    ReturnItemToOrigin(heldItem);
                }

                // On accepte le nouvel item
                AcceptItem(draggable);
            }
        }
    }

    private void AcceptItem(Draggable draggable)
    {
        // Cacher l'objet draggable visuellement
        draggable.gameObject.SetActive(false);

        // Remplacer le "?" par le texte de l'option
        TMP_Text optionText = draggable.GetComponent<TMP_Text>();
        if (optionText != null && targetText != null)
        {
            targetText.text = optionText.text;
        }

        // Marquer cette dropzone comme occup�e
        heldItem = draggable;
        draggable.currentDropZone = this;


    }
    
    private void ReturnItemToOrigin(Draggable item)
    {
        CanvasGroup canvasGroup = item.GetComponent<CanvasGroup>();

        // Remettre le "?" dans le champ
        if (targetText != null)
        {
            targetText.text = "?";
        }


        // R�activer et repositionner l'item
        item.gameObject.SetActive(true);
        item.transform.SetParent(item.originalParent, false);
        item.transform.localPosition = item.originalPosition;
        item.currentDropZone = null;

        // R�initialiser l'alpha et les raycasts
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;           // Opacit� normale
            canvasGroup.blocksRaycasts = true; // R�active les interactions
        }


        heldItem = null;
    }

    public void ClearDropZone()
    {
        if (heldItem != null)
        {
            ReturnItemToOrigin(heldItem);
        }
    }
}