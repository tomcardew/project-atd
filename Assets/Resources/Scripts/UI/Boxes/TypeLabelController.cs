using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeLabelController : MonoBehaviour
{
    public CardType cardType;

    private Image image;
    private TextMeshProUGUI label;

    private void Start()
    {
        image = GetComponent<Image>();
        label = GetComponentInChildren<TextMeshProUGUI>();

        SetData(cardType);
    }

    public void SetData(CardType cardType)
    {
        this.cardType = cardType;
        image.color = Utils.GetForegroundTypeColor(cardType);
        label.text = Utils.GetForegroundTypeName(cardType).ToUpper();
    }
}
