using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public float value;

    private Image sliderImage;

    private void Start()
    {
        sliderImage = GetSliderImage();
        sliderImage.fillAmount = 1.0f;
        ToggleUI(false);
    }

    private Image GetSliderImage()
    {
        return transform.GetChild(1).GetComponent<Image>();
    }

    public void SetValue(float newValue)
    {
        value = newValue;
        sliderImage = GetSliderImage();
        sliderImage.fillAmount = value;
        ToggleUI(value > 0 && value < 1f);
    }

    public void ChangeColor(Color newColor)
    {
        sliderImage = GetSliderImage();
        sliderImage.color = newColor;
    }

    private void ToggleUI(bool active)
    {
        // Disable all children
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}
