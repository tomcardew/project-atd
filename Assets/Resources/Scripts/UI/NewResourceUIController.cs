using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewResourceUIController : MonoBehaviour
{
    // Public properties
    public ResourceType resource;

    // Private properties
    private Image image;
    private Resource _resource;
    private CanvasGroup canvasGroup;
    private bool isMoving = false;

    private void Start()
    {
        image = GetComponentInChildren<Image>();
        _resource = ResourceList.GetResourceByType(resource);
        image.sprite = Resources.Load<Sprite>($"Sprites/{_resource.Icon}");

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0;
    }

    public void Show()
    {
        if (!isMoving)
        {
            StartCoroutine(ShowAndMoveUp());
        }
    }

    private IEnumerator ShowAndMoveUp()
    {
        isMoving = true;
        canvasGroup.alpha = 1;
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = originalPosition + Vector3.up * 0.5f;

        float duration = 1.0f; // Mover más lento
        float elapsedTime = 0;

        if (_resource.Sound != null)
            AudioSource.PlayClipAtPoint(
                Resources.Load<AudioClip>($"Sounds/{_resource.Sound}"),
                transform.position
            );

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(
                originalPosition,
                targetPosition,
                elapsedTime / duration
            );
            canvasGroup.alpha = 1 - (elapsedTime / duration); // Disminuir alpha lentamente
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        canvasGroup.alpha = 0;

        // Regresar a la posición original
        transform.position = originalPosition;
        isMoving = false;
    }
}
