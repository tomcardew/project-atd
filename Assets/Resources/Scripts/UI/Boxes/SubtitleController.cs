using TMPro;
using UnityEngine;

public class SubtitleController : MonoBehaviour
{
    public string text;

    private TextMeshProUGUI label;

    private void Start()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();
        label.text = text.ToUpper();
    }

    public void SetData(string text)
    {
        this.text = text;
        label.text = text.ToUpper();
    }
}
