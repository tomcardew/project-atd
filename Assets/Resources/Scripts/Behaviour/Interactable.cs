using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable
    : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerClickHandler
{
    // Public properties
    public SpriteRenderer spriteRenderer;
    public bool isTarget;
    public InteractionTarget target;

    // Private properties
    private Color originalColor;
    private bool isSelectingTarget = false;

    void Start()
    {
        if (spriteRenderer == null)
        {
            throw new System.Exception("SpriteRenderer is required");
        }
        originalColor = spriteRenderer.color;
        Manager.Interactions.SelectingTargetStarted += OnSelectingTargetStarted;
        Manager.Interactions.SelectingTargetFinished += OnSelectingTargetFinished;
    }

    private void OnDestroy()
    {
        Manager.Interactions.SelectingTargetStarted -= OnSelectingTargetStarted;
        Manager.Interactions.SelectingTargetFinished -= OnSelectingTargetFinished;
    }

    private void ResetSpriteRenderer()
    {
        spriteRenderer.color = originalColor;
    }

    private void OnSelectingTargetStarted(InteractionTarget target)
    {
        isSelectingTarget = target == this.target;
    }

    private void OnSelectingTargetFinished(GameObject gameObject)
    {
        isSelectingTarget = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Manager.Cursor.SetDefaultCursor();
        Manager.Interactions.TriggerTargetFinished(gameObject);
        ResetSpriteRenderer();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isTarget && isSelectingTarget)
        {
            spriteRenderer.color = Color.yellow;
            Manager.Cursor.SetSelectCursor();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isTarget && isSelectingTarget)
        {
            ResetSpriteRenderer();
            Manager.Cursor.SetDefaultCursor();
        }
    }
}
