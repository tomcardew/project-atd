using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Draggable
    : MonoBehaviour,
        IPointerDownHandler,
        IPointerEnterHandler,
        IPointerExitHandler,
        IBeginDragHandler,
        IEndDragHandler,
        IDragHandler
{
    // Public properties
    public abstract void Init();
    public abstract void DidPointerDown(PointerEventData eventData);
    public abstract void DidBeginDrag(PointerEventData eventData);
    public abstract void Drag(PointerEventData eventData);
    public abstract void DidEndDrag(PointerEventData eventData);

    // Private properties
    [SerializeField]
    private Canvas canvas;
    private RectTransform rectTransform;
    private Vector3 offset;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }
        Init();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 worldPoint;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out worldPoint
        );
        offset = rectTransform.position - worldPoint;
        DidPointerDown(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DidBeginDrag(eventData);
        Manager.Cursor.SetGrabCursor();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPoint;
        if (
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out worldPoint
            )
        )
        {
            rectTransform.position = worldPoint + offset;
        }
        Drag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DidEndDrag(eventData);
        Manager.Cursor.SetDefaultCursor();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Manager.Cursor.SetHandCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Manager.Cursor.SetDefaultCursor();
    }
}
