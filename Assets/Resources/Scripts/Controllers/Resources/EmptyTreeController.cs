using System.Collections;
using UnityEngine;

public class EmptyTreeController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float fadeDuration = 5.0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        float startAlpha = spriteRenderer.color.a;
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            Color tmpColor = spriteRenderer.color;
            tmpColor.a = Mathf.Lerp(startAlpha, 0, progress);
            spriteRenderer.color = tmpColor;

            progress += rate * Time.deltaTime;

            yield return null;
        }

        Destroy(transform.parent.gameObject);
    }
}
