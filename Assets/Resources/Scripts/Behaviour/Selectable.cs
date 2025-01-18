using UnityEngine;
using UnityEngine.EventSystems;

public class Selectable
    : MonoBehaviour,
        IPointerDownHandler,
        IPointerEnterHandler,
        IPointerExitHandler
{
    public bool isSelected = false;
    public int index;
    public float scaleSpeed = 5f;

    public delegate void DidChangeSelectedHandler(int index, bool selected);
    public event DidChangeSelectedHandler DidChangeSelected;

    private Vector3 originalScale;
    private Vector3 targetScale;

    private void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    private void Update()
    {
        if (isSelected)
        {
            targetScale = originalScale * 0.9f;
        }
        else
        {
            targetScale = originalScale;
        }

        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Manager.Time.unpausableDeltaTime * scaleSpeed
        );
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isSelected = !isSelected;
        DidChangeSelected?.Invoke(index, isSelected);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Manager.Cursor.SetSelectCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Manager.Cursor.SetDefaultCursor();
    }
}
