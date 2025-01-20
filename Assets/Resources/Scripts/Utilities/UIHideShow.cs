using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHideShow : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private AudioClip toggleClip;

    public bool isShowing = false;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0;
    }

    public void Show()
    {
        StartCoroutine(InternalShow());
    }

    public void Hide()
    {
        StartCoroutine(InternalHide());
    }

    private IEnumerator InternalShow()
    {
        float duration = 1.0f; // Mover más lento
        float elapsedTime = 0;

        if (toggleClip != null)
            Manager.Sound.Play(toggleClip, AudioSourceType.Resources, transform.position);

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = elapsedTime / duration; // Disminuir alpha lentamente
            elapsedTime += Manager.Time.pausableDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1;
        isShowing = true;
    }

    private IEnumerator InternalHide()
    {
        float duration = 1.0f; // Mover más lento
        float elapsedTime = 0;

        if (toggleClip != null)
            Manager.Sound.Play(toggleClip, AudioSourceType.Resources, transform.position);

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = 1 - elapsedTime / duration; // Disminuir alpha lentamente
            elapsedTime += Manager.Time.pausableDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        isShowing = false;
    }
}
