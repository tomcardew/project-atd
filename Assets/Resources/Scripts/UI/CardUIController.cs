using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardUIController : MonoBehaviour
{
    // Public properties
    public Card card;
    public RawImage icon;

    public GameObject slotA;
    public GameObject slotB;
    public GameObject slotC;

    // Private properties
    private TextMeshProUGUI title;
    private TextMeshProUGUI description;

    private void Start()
    {
        GetComponentInParent<Canvas>().worldCamera = Camera.main;

        var labels = GetComponentsInChildren<TextMeshProUGUI>();
        title = labels[0];
        description = labels[1];

        SetData();
    }

    private void SetData()
    {
        title.text = card.name;
        description.text = card.description;
        icon.texture = Resources.Load<Texture2D>($"Sprites/{card.iconPath}");

        GameObject[] slots = { slotA, slotB, slotC };

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < card.resources.Length)
            {
                var resource = card.resources[i];
                slots[i].GetComponentInChildren<TextMeshProUGUI>().text = resource.value.ToString();
                slots[i].GetComponentInChildren<RawImage>().texture = Resources.Load<Texture2D>(
                    $"Sprites/{ResourceList.GetResourceByType(resource.resource).Icon}"
                );
            }
            else
            {
                slots[i].SetActive(false);
            }
        }
    }
}
