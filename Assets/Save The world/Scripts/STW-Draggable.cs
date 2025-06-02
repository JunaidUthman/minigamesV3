using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Transform originalParent;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        Debug.Log("Draggable Awake called: " + gameObject.name);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //originalParent = transform.parent;
        //transform.SetParent(canvas.transform, true);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPos
        );

        rectTransform.anchoredPosition = localPos;
        //ça est essentielle pour convertir la position du curseur vers un système de coordonnées compréhensible par l’UI de Unity, ce qui permet de déplacer précisément l'objet UI que l’on est en train de faire glisser.
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //transform.SetParent(originalParent, true);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }
}
