using UnityEngine;

public class UIManager : MonoBehaviour
{
    private string tooltip;

    public void SetTooltip(string text)
    {
        tooltip = text;
    }

    public string GetTooltip()
    {
        return tooltip;
    }

    public void ClearTooltip()
    {
        tooltip = "";
    }
}
