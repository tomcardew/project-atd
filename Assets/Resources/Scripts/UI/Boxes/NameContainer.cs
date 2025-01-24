using TMPro;
using UnityEngine;

public class NameContainer : MonoBehaviour
{
    public string text;

    private TextMeshProUGUI label;

    private void Start()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();
        label.text = text;
    }

    public void SetData(string text)
    {
        this.text = text;
        label.text = text;
    }
}
