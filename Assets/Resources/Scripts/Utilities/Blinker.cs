using System.Collections;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    public CanvasRenderer canvasRenderer;
    public float blinkSpeed = 1.0f;

    private void Start()
    {
        if (canvasRenderer == null)
        {
            Debug.LogError("CanvasRenderer component not found.");
            return;
        }
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            // canvasRenderer.SetAlpha(0);
            canvasRenderer.gameObject.SetActive(false);
            yield return new WaitForSeconds(blinkSpeed);
            // canvasRenderer.SetAlpha(1);
            canvasRenderer.gameObject.SetActive(true);
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}
