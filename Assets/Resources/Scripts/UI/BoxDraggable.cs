using UnityEngine;
using UnityEngine.EventSystems;

public class BoxDraggable : Draggable
{
    public override void Init()
    {
        updateCursor = false;
    }

    public override void DidBeginDrag(PointerEventData eventData) { }

    public override void DidEndDrag(PointerEventData eventData) { }

    public override void DidPointerDown(PointerEventData eventData) { }

    public override void Drag(PointerEventData eventData) { }
}
