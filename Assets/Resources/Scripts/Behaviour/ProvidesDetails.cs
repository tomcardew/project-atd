using UnityEngine;
using UnityEngine.EventSystems;

public class ProvidesDetails
    : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerClickHandler
{
    // Public properties
    public ObjectType type;
    public new string name;

    // Private properties
    private GameObject detailsBox;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (detailsBox != null)
        {
            Destroy(detailsBox);
        }
        detailsBox = BoxBuilder.CreateDetailsBox(name, gameObject, type);
        detailsBox.transform.position = transform.position + Vector3.right + Vector3.down;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Manager.Cursor.SetSelectCursor();
        Manager.UI.SetTooltip("Click for details");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Manager.Cursor.SetDefaultCursor();
        Manager.UI.ClearTooltip();
    }
}
