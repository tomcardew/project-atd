using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUIController : MonoBehaviour
{
    // Public properties
    public Card card;
    public int index;
    public RawImage icon;

    public GameObject slotA;
    public GameObject slotB;
    public GameObject slotC;

    [Header("Card Resources")]
    public Sprite normalCard;
    public Sprite structureCard;
    public Sprite actionCard;
    public Sprite woodResource;

    // Private properties
    private TextMeshProUGUI title;
    private TextMeshProUGUI description;
    private Image foregroundImage;

    private void Start()
    {
        GetComponentInParent<Canvas>().worldCamera = Camera.main;
        foregroundImage = GetComponent<Image>();

        var labels = GetComponentsInChildren<TextMeshProUGUI>();
        title = labels[0];
        description = labels[1];

        Sprite foreground;
        switch (card.foregroundType)
        {
            case CardForegroundType.Structure:
                foreground = structureCard;
                break;
            case CardForegroundType.Action:
                foreground = actionCard;
                break;
            case CardForegroundType.WoodResource:
                foreground = woodResource;
                break;
            case CardForegroundType.Normal:
            default:
                foreground = normalCard;
                break;
        }
        foregroundImage.sprite = foreground;

        SetData();
    }

    void OnDestroy()
    {
        Manager.Sound.Play(
            Prefabs.GetSound(Prefabs.SoundType.CardDiscarded),
            AudioSourceType.UI,
            transform.position,
            5f
        );
    }

    public void MoveToPosition(
        Vector3 targetPosition,
        float duration,
        float delay,
        bool playSound = false
    )
    {
        StartCoroutine(MoveCoroutine(targetPosition, duration, delay, playSound));
    }

    private IEnumerator MoveCoroutine(
        Vector3 targetPosition,
        float duration,
        float delay,
        bool playSound
    )
    {
        yield return new WaitForSeconds(delay);
        Vector3 startPosition = transform.parent.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.parent.position = Vector3.Lerp(
                startPosition,
                targetPosition,
                elapsedTime / duration
            );
            elapsedTime += Manager.Time.pausableDeltaTime;
            yield return null;
        }
        if (playSound)
        {
            Manager.Sound.Play(
                Prefabs.GetSound(Prefabs.SoundType.CardFlip),
                AudioSourceType.UI,
                transform.position,
                3f
            );
        }

        transform.parent.position = targetPosition;
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
