using TMPro;
using UnityEngine;

public class CardResourceItemController : MonoBehaviour
{
    // Public properties
    public Sprite icon;
    public int value;

    // Private properties
    private SpriteRenderer iconRenderer;
    private TextMeshProUGUI valueText;

    private void Start()
    {
        iconRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        valueText = transform.GetComponentInChildren<TextMeshProUGUI>();
    }
}
