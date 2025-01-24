using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    public List<Card> discarded; // It will contain the discarded cards
    public List<Card> deck; // It will contain the current set of cards
    public List<Card> hand; // It will contain the current hand of cards

    public Transform deckHolder; // where is located the deck holder
    public Transform discardedHolder; // where is located the discarded holder

    private CardHolderController cardsHolder; //cards container reference
    private List<GameObject> handCards; // internal list of card objects
    private GameObject draggingCard; // It will contain the current dragging card

    private void Start()
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

        // Call the FirstDrawModalController to select the first draw cards
        List<Card> cards = deck.Take(5).ToList();
        Manager.UI.ShowFirstDrawModal(cards.ToArray());
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

    public void SetFirstDrawCards(Card[] selected, Card[] unselected)
    {
        hand.Clear();
        hand.AddRange(selected);
        discarded.Clear();
        discarded.AddRange(unselected);
        deck.RemoveRange(0, 5);

        for (int i = 0; i < selected.Length; i++)
        {
            var item = selected[i];
            CreateAndAddToHand(item, i);
        }
        for (int i = selected.Length; i < 5; i++)
        {
            Draw();
        }
        Manager.Game.StartGame();
    }

    public void SetAddCard(Card card)
    {
        if (card != null)
            deck.Insert(0, card);

        Manager.Game.waitingForDraw = false;
        for (int i = hand.Count; i < 5; i++)
        {
            Draw();
        }
    }

    private void HandleWaveStart()
    {
        // Implement additional logic for when a wave starts
        // Refill your hand with cards
        for (int i = hand.Count; i < 5; i++)
        {
            Draw();
        }
    }

    private void HandleWaveEnd()
    {
        Card[] availableCards = Cards.GetAvailableCardAtRound(Manager.Game.currentRound).ToArray();
        Card[] cardsToShow = Utils.ShuffleList(availableCards.ToList()).Take(3).ToArray();
        Manager.UI.ShowAddCardModal(cardsToShow);
    }

    public void ShuffleDeck()
    {
        deck = Utils.ShuffleList(deck);
    }

    public void Draw()
    {
        var card = GetNextDrawCard();
        CreateAndAddToHand(card, hand.Count);

        hand.Add(card);
        deck.RemoveAt(0);
    }

    public int FindIndexOfCardWithNameOnDeck(string name)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            var c = deck[i];
            if (c.name == name)
            {
                return i;
            }
        }
        return -1;
    }

    public int GetRandomCardWithNames(string[] names)
    {
        var matchingCards = deck.Select((card, index) => new { card, index })
            .Where(x => names.Contains(x.card.name))
            .ToList();

        if (matchingCards.Count == 0)
        {
            return -1; // No matching card found
        }

        var randomIndex = Random.Range(0, matchingCards.Count);
        return matchingCards[randomIndex].index;
    }

    public void MoveDeckIndexToHand(int index)
    {
        if (index < 0 || index >= deck.Count)
        {
            return;
        }
        var card = deck[index];
        hand.Add(card);
        deck.RemoveAt(index);
        deck = Utils.ShuffleList(deck);
        CreateAndAddToHand(card, hand.Count - 1);
    }

    public void Discard(int index)
    {
        Card c = hand[index];
        discarded.Add(c);
        hand.RemoveAt(index);
        cardsHolder.MoveToDiscarded(index);
    }

    private Card GetNextDrawCard()
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
        return deck[0];
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

    private void CreateAndAddToHand(Card card, int index)
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
