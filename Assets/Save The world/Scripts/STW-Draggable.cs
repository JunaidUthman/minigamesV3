using UnityEngine.EventSystems;
using UnityEngine;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    [HideInInspector] public Transform originalParent;
    [HideInInspector] public Vector2 originalPosition;
    [HideInInspector] public DropZone currentDropZone = null;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    void Start()
    {
        // Sauvegarder la position et le parent d'origine
        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        // Si on était dans une dropzone, la libérer
        if (currentDropZone != null)
        {
            currentDropZone.ClearDropZone();
        }

        transform.SetParent(canvas.transform, true);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPos
        );
        rectTransform.anchoredPosition = localPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        // Si on ne l'a pas posé sur une dropzone, revenir à l'origine
        if (currentDropZone == null)
        {
            ReturnToOrigin();
        }
    }
    public void ReturnToOrigin()
    {
        gameObject.SetActive(true);
        transform.SetParent(originalParent, false);
        rectTransform.anchoredPosition = originalPosition;
        currentDropZone = null;
    }
}
