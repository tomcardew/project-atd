using TMPro;
using UnityEngine;
using System.Collections;

public class StateBarController : MonoBehaviour
{
    public TextMeshProUGUI peopleValue;
    public TextMeshProUGUI moneyValue;
    public TextMeshProUGUI woodValue;

    public float topPosY = 270f;
    public float bottomPosY = 20f;
    public float transitionDuration = 1f;

    private RectTransform rectTransform;
    private bool isUp = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        UpdateValues();
        if (Manager.Cards.hand.Count > 0 && !isUp)
        {
            isUp = true;
            MoveRectTransform(true);
        }
        else if (Manager.Cards.hand.Count == 0 && isUp)
        {
            isUp = false;
            MoveRectTransform(false);
        }
    }

    private void UpdateValues()
    {
        if (Manager.Resources != null)
        {
            bool moneyLimitReached = Manager.Resources.HasReachedLimit(ResourceType.Money);
            moneyValue.text = Manager.Resources.GetResourceValue(ResourceType.Money).ToString("D3");
            moneyValue.color = moneyLimitReached ? Color.red : Color.white;

            bool woodLimitReached = Manager.Resources.HasReachedLimit(ResourceType.Wood);
            woodValue.text = Manager.Resources.GetResourceValue(ResourceType.Wood).ToString("D3");
            woodValue.color = woodLimitReached ? Color.red : Color.white;

            peopleValue.text = Manager.Population.GetPopulation().ToString("D3");
        }
    }

    public void MoveRectTransform(bool moveToTop)
    {
        StartCoroutine(MoveRectTransformCoroutine(moveToTop));
    }

    private IEnumerator MoveRectTransformCoroutine(bool moveToTop)
    {
        float elapsedTime = 0f;
        Vector2 startPos = rectTransform.anchoredPosition;
        Vector2 endPos = new Vector2(startPos.x, moveToTop ? topPosY : bottomPosY);

        while (elapsedTime < transitionDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(
                startPos,
                endPos,
                elapsedTime / transitionDuration
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = endPos;
    }
}
