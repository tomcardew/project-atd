using UnityEngine;
using UnityEngine.EventSystems;

public class CardDraggable : Draggable
{
    public override void Init() { }

    public override void DidBeginDrag(PointerEventData eventData)
    {
        Manager.Cards.RegisterDraggingCard(gameObject);
    }

    public override void DidEndDrag(PointerEventData eventData)
    {
        Manager.UI.ClearTooltip();
        Manager.Cards.EndDrag();
    }

    public override void DidPointerDown(PointerEventData eventData) { }

    public override void Drag(PointerEventData eventData) { }
}
