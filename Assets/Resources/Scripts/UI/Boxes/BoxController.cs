using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoxController : MonoBehaviour
{
    public Button closeButton;

    private GameObject container;

    private void Awake()
    {
        container = transform.GetChild(1).gameObject;
        // Set initial position and transparency
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(
            rectTransform.anchoredPosition.x,
            rectTransform.anchoredPosition.y - 0.2f
        );
        CanvasGroup canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    private void Start()
    {
        Manager.Sound.Play(
            Prefabs.GetSound(Prefabs.SoundType.BoxEnter),
            AudioSourceType.UI,
            Vector3.zero
        );
        closeButton.onClick.AddListener(() => Destroy(gameObject));
        // Animate position and fade in
        StartCoroutine(AnimateIn());
    }

    private IEnumerator AnimateIn()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        float duration = 0.1f;
        float elapsedTime = 0;

        Vector2 startPos = rectTransform.anchoredPosition;
        Vector2 endPos = new Vector2(startPos.x, startPos.y + 0.2f);

        while (elapsedTime < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / duration);
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = endPos;
        canvasGroup.alpha = 1;
    }

    public void AddElement(GameObject obj)
    {
        obj.transform.SetParent(container.transform);
    }

    private void OnDestroy()
    {
        Manager.Sound.Play(
            Prefabs.GetSound(Prefabs.SoundType.BoxExit),
            AudioSourceType.UI,
            Vector3.zero
        );
    }
}
