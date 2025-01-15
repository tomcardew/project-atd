using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    public List<Card> destroyed; // It will contain the destroyed cards
    public List<Card> deck; // It will contain the current set of cards
    public List<Card> hand; // It will contain the current hand of cards
    private GameObject draggingCard; // It will contain the current dragging card
    private GameObject cardsHolder;

    private void Awake()
    {
        // Initialize the deck with all available cards
        destroyed = new List<Card>();
        hand = new List<Card>();
        deck = new List<Card>();
        deck = Utils.ShuffleList(Cards.StartDeck.ToList());
        cardsHolder = new GameObject("CardsHolder");

        for (int i = 0; i < 5; i++)
        {
            Draw();
        }

        RenderCards();
    }

    private void OnEnable()
    {
        GameManager.OnWaveStart += HandleWaveStart;
        GameManager.OnWaveEnd += HandleWaveEnd;
    }

    private void OnDisable()
    {
        GameManager.OnWaveStart -= HandleWaveStart;
        GameManager.OnWaveEnd -= HandleWaveEnd;
    }

    private void HandleWaveStart()
    {
        Debug.Log("Wave started!");
        // Implement additional logic for when a wave starts
        // Refill your hand with cards
        for (int i = hand.Count; i < 5; i++)
        {
            Draw();
        }
    }

    private void HandleWaveEnd()
    {
        Debug.Log("Wave ended!");
        for (int i = hand.Count; i < 5; i++)
        {
            Draw();
        }
    }

    private void RenderCards()
    {
        Utils.DestroyAllChildren(cardsHolder);
        for (int i = 0; i < hand.Count; i++)
        {
            SpawnCard(hand[i], i);
        }
    }

    public void ShuffleDeck()
    {
        deck = Utils.ShuffleList(deck);
    }

    public void Draw()
    {
        if (deck.Count == 0)
        {
            if (destroyed.Count == 0)
            {
                throw new System.Exception("No cards available");
            }
            deck = Utils.ShuffleList(destroyed);
            destroyed.Clear();
        }
        hand.Add(deck[0]);
        deck.RemoveAt(0);
        RenderCards();
    }

    public void RegisterDraggingCard(GameObject card)
    {
        draggingCard = card;
    }

    public void EndDrag()
    {
        if (draggingCard == null)
        {
            throw new System.Exception("No dragging card found");
        }
        CardUIController cardUI = draggingCard.GetComponentInChildren<CardUIController>();
        if (cardUI != null && Manager.Resources.CanPay(cardUI.card))
        {
            Card card = cardUI.card;
            Droppable dp = new GameObject("Droppable").AddComponent<Droppable>();
            dp.card = card;
            dp.OnDropFinished += EndDrop;
            Destroy(draggingCard);
        }
        else
        {
            Destroy(draggingCard);
            RenderCards();
        }
        draggingCard = null;
    }

    private void EndDrop(Card card, bool success)
    {
        if (success)
        {
            destroyed.Add(card);
            hand.RemoveAt(hand.FindIndex(c => c.name == card.name));
        }
        RenderCards();
        Manager.Cursor.SetDefaultCursor();
    }

    private void SpawnCard(Card card, int index)
    {
        // Instantiate the card UI prefab at the origin
        GameObject _card = Instantiate(
            Prefabs.CardUI,
            Vector3.zero,
            Quaternion.identity,
            cardsHolder.transform
        );

        // Calculate the initial position based on the camera's viewport
        Vector2 position = Camera.main.ViewportToWorldPoint(
            new Vector3(0, 0, Camera.main.nearClipPlane)
        );

        // Get the RectTransform and CanvasScaler components
        RectTransform rect = _card.GetComponent<RectTransform>();
        float scaler = _card.GetComponent<CanvasScaler>().referencePixelsPerUnit;
        float rate = rect.sizeDelta.y / rect.sizeDelta.x;

        // Calculate the offset for the card's position
        Vector2 offset = new Vector2(0.1f + (1.5f * index), 0.5f);
        position += new Vector2(rect.localScale.x * scaler, rect.localScale.y * scaler * rate);
        position += offset;

        // Set the card's position
        _card.transform.position = position;

        // Assign the card data to the UI controller
        CardUIController cardUI = _card.GetComponentInChildren<CardUIController>();
        cardUI.card = card;
    }
}
