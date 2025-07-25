using UnityEngine;
using UnityEngine.EventSystems;

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
        Debug.Log($"{name} - Awake called");
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (rectTransform == null)
            Debug.LogError($"{name} - RectTransform is MISSING!");

        if (canvasGroup == null)
            Debug.LogError($"{name} - CanvasGroup is MISSING! Drag will fail!");
    }

    void Start()
    {
        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;

        Debug.Log($"{name} - Original parent set to {originalParent.name}, original position: {originalPosition}");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"{name} - Begin Drag");

        // IMPORTANT : r�affecter le canvas parent actif
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError($"{name} - Canvas NOT FOUND during drag! Are you sure this object is under a Canvas?");
            return;
        }

        // lib�rer la zone si d�j� pos�e
        if (currentDropZone != null)
        {
            Debug.Log($"{name} - Clearing current drop zone: {currentDropZone.name}");
            currentDropZone.ClearDropZone();
        }

        // On d�place l�objet au sommet du Canvas
        transform.SetParent(canvas.transform, true);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"{name} - Dragging...");

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

        // Si on ne l'a pas pos� sur une dropzone, revenir � l'origine
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
    void OnEnable()
    {
        // Sauvegarder la position et le parent d'origine � chaque activation
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        if (originalParent == null) originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;
    }

}
