using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Interactable
    : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerClickHandler
{
    // Public properties
    [Tooltip("SpriteRenderer is use for non-cards items")]
    public SpriteRenderer spriteRenderer;

    [Tooltip("SpriteRenderer is use for cards items only")]
    public Image image;

    public bool isTarget;
    public InteractionTarget target;

    // Private properties
    private Color originalColor;
    private bool isSelectingTarget = false;

    void Start()
    {
        switch (target)
        {
            case InteractionTarget.Card:
                if (image == null)
                {
                    throw new System.Exception("Image is required");
                }
                originalColor = image.color;
                break;
            default:
                if (spriteRenderer == null)
                {
                    throw new System.Exception("SpriteRenderer is required");
                }
                originalColor = spriteRenderer.color;
                break;
        }

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
        switch (target)
        {
            case InteractionTarget.Card:
                image.color = originalColor;
                break;
            default:
                spriteRenderer.color = originalColor;
                break;
        }
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
            switch (target)
            {
                case InteractionTarget.Card:
                    image.color = Color.yellow;
                    break;
                default:
                    spriteRenderer.color = Color.yellow;
                    break;
            }
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
