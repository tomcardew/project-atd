using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    public List<Card> discarded; // It will contain the discarded cards
    public List<Card> deck; // It will contain the current set of cards
    public List<Card> hand; // It will contain the current hand of cards
    private List<GameObject> handCards;

    private CardHolderController cardsHolder;

    public Transform deckHolder;
    public Transform discardedHolder;

    private GameObject draggingCard; // It will contain the current dragging card

    private void Awake()
    {
        // Initialize the deck with all available cards
        discarded = new List<Card>();
        hand = new List<Card>();
        handCards = new List<GameObject>();
        deck = new List<Card>();
        deck = Utils.ShuffleList(Cards.StartDeck.ToList());

        var _cardHolder = new GameObject("CardsHolder");
        cardsHolder = _cardHolder.AddComponent<CardHolderController>();

        SpawnCounters();

        for (int i = 0; i < 5; i++)
        {
            Draw();
        }
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
        for (int i = hand.Count; i < 5; i++)
        {
            Draw();
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
            if (discarded.Count == 0)
            {
                throw new System.Exception("No cards available");
            }
            deck = Utils.ShuffleList(discarded);
            discarded.Clear();
        }
        SpawnCard(deck[0], hand.Count);

        hand.Add(deck[0]);
        deck.RemoveAt(0);
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
        cardsHolder.GoToWaitingPosition(cardUI.index);
        if (cardUI != null && Manager.Resources.CanPay(cardUI.card))
        {
            Card card = cardUI.card;
            Droppable dp = new GameObject("Droppable").AddComponent<Droppable>();
            dp.card = card;
            dp.index = cardUI.index;
            dp.OnDropFinished += EndDrop;
        }
        else
        {
            cardsHolder.MoveBackToHand(cardUI.index);
        }
        draggingCard = null;
    }

    private void EndDrop(Card card, int index, bool success)
    {
        if (success)
        {
            discarded.Add(card);
            hand.RemoveAt(index);
            cardsHolder.MoveToDiscarded(index);
        }
        else
        {
            cardsHolder.MoveBackToHand(index);
        }
        Manager.Cursor.SetDefaultCursor();
    }

    private GameObject SpawnCounters()
    {
        GameObject _counters = Instantiate(
            Prefabs.GetPrefab(Prefabs.OtherType.CardBackCounters),
            Vector3.zero,
            Quaternion.identity,
            cardsHolder.transform
        );

        // Update the holder references
        deckHolder = _counters.transform.GetChild(0);
        cardsHolder.deck = deckHolder;

        discardedHolder = _counters.transform.GetChild(1);
        cardsHolder.discarded = discardedHolder;

        // Calculate the initial position based on the camera's viewport
        Vector2 position = Camera.main.ViewportToWorldPoint(
            new Vector3(1, 0, Camera.main.nearClipPlane)
        );

        position += new Vector2(-0.8f, 1.6f);

        _counters.transform.position = position;
        return _counters;
    }

    private void SpawnCard(Card card, int index)
    {
        // Instantiate the card UI prefab at the origin
        GameObject _card = Instantiate(
            Prefabs.GetPrefab(Prefabs.OtherType.CardUI),
            deckHolder.position,
            Quaternion.identity,
            cardsHolder.transform
        );
        handCards.Add(_card);

        // Assign the card data to the UI controller
        CardUIController cardUI = _card.GetComponentInChildren<CardUIController>();
        cardUI.card = card;
        cardUI.index = index;

        // Add to cardholder
        cardsHolder.AddToHand(_card);
    }
}
