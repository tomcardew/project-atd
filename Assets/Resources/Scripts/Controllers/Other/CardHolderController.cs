using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHolderController : MonoBehaviour
{
    public Transform deck;
    public Transform discarded;

    private List<GameObject> hand;

    private void Awake()
    {
        hand = new();
    }

    public void Clean()
    {
        foreach (var card in hand)
        {
            Destroy(card.gameObject);
        }
        hand.Clear();
    }

    public void AddToHand(GameObject card)
    {
        var cardUIController = card.GetComponentInChildren<CardUIController>();
        if (cardUIController == null)
        {
            throw new System.ArgumentException("The card must have a CardUIController component");
        }
        card.transform.position = deck.position;
        var nextPosition = GetPositionByIndex(
            hand.Count,
            card.GetComponent<RectTransform>(),
            card.GetComponent<CanvasScaler>()
        );
        hand.Add(card);
        cardUIController.MoveToPosition(nextPosition, 0.5f, 0.1f * (hand.Count - 1));
        UpdateIndexes();
    }

    public void MoveBackToHand(int index)
    {
        var card = hand[index];
        var nextPosition = GetPositionByIndex(
            index,
            card.GetComponent<RectTransform>(),
            card.GetComponent<CanvasScaler>()
        );
        card.GetComponentInChildren<CardUIController>().MoveToPosition(nextPosition, 0.5f, 0);
        UpdateIndexes();
    }

    public void MoveToDiscarded(int index)
    {
        var card = hand[index];
        hand.RemoveAt(index);
        card.GetComponentInChildren<CardUIController>().MoveToPosition(discarded.position, 0.5f, 0);
        Destroy(card, 0.5f);
        for (int i = 0; i < hand.Count; i++)
        {
            var _c = hand[i];
            _c.GetComponentInChildren<CardUIController>()
                .MoveToPosition(
                    GetPositionByIndex(
                        i,
                        _c.GetComponent<RectTransform>(),
                        _c.GetComponent<CanvasScaler>()
                    ),
                    0.5f,
                    0
                );
        }
        UpdateIndexes();
    }

    public void GoToWaitingPosition(int index)
    {
        Vector2 position = Camera.main.ViewportToWorldPoint(
            new Vector3(0.5f, 0, Camera.main.nearClipPlane)
        );
        position += new Vector2(0, -deck.position.y);
        hand[index].GetComponentInChildren<CardUIController>().MoveToPosition(position, 0.5f, 0);
    }

    private Vector3 GetPositionByIndex(int index, RectTransform rect, CanvasScaler scaler)
    {
        // Calculate the initial position based on the camera's viewport
        Vector2 position = Camera.main.ViewportToWorldPoint(
            new Vector3(0, 0, Camera.main.nearClipPlane)
        );

        // Get the RectTransform and CanvasScaler components
        float _scaler = scaler.referencePixelsPerUnit;
        float _rate = rect.sizeDelta.y / rect.sizeDelta.x;

        // Calculate the offset for the card's position
        Vector2 offset = new Vector2(0.1f + (1.5f * index), 0.5f);
        position += new Vector2(rect.localScale.x * _scaler, rect.localScale.y * _scaler * _rate);
        position += offset;

        return position;
    }

    private void UpdateIndexes()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].GetComponentInChildren<CardUIController>().index = i;
        }
    }
}
