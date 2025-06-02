using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null)
        {
            TMP_Text draggedText = dropped.GetComponent<TMP_Text>();

            // Trouver le champ texte à remplir dans la zone de drop
            TMP_Text targetText = GetComponentInChildren<TMP_Text>();
            if (draggedText != null && targetText != null)
            {
                targetText.text = draggedText.text;
            }
        }
    }
}
