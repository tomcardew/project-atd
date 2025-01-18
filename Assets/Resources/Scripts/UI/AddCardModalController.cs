using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddCardModalController : MonoBehaviour
{
    public Card[] cards; // List of selectable cards
    public Selectable[] selectables; // List of selectable components
    public GameObject scrollerPanel; // Panel containing the scroll view
    public Button continueButton; // Button to continue to the next screen

    public bool[] selectedCards; // List of selectable cards transformed

    private void Start()
    {
        Manager.Time.isPaused = true;
        selectables = new Selectable[3];
        selectedCards = cards.Take(3).Select(c => false).ToArray(); // Initialize the list of selectable cards

        if (selectedCards.Length == 0)
        {
            OnContinueClick();
        }

        continueButton.onClick.AddListener(OnContinueClick); // Add a listener to the continue button
        AddCardsToPanel();
    }

    private void OnContinueClick()
    {
        int index = Array.FindIndex(selectedCards, c => c);
        Card card = index > -1 ? cards[index] : null;
        Manager.Cards.SetAddCard(card);
        Manager.Time.isPaused = false;
        Destroy(gameObject);
    }

    private void AddCardsToPanel()
    {
        for (int i = 0; i < cards.Count(); i++)
        {
            var card = cards[i];
            var cardPrefab = Instantiate(
                Prefabs.GetPrefab(Prefabs.OtherType.CardUI),
                Vector2.zero,
                Quaternion.identity,
                scrollerPanel.transform
            );
            // change scale of the card
            cardPrefab.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            // disable draggable component
            cardPrefab.GetComponent<Draggable>().enabled = false;
            // set card data
            var cardUI = cardPrefab.GetComponentInChildren<CardUIController>();
            cardUI.card = card;

            Selectable selectable = cardPrefab.AddComponent<Selectable>();
            selectable.index = i;
            selectable.DidChangeSelected += HandleCardSelected;

            selectables[i] = selectable;
        }
    }

    private void HandleCardSelected(int index, bool selected)
    {
        for (int i = 0; i < selectedCards.Length; i++)
        {
            if (i != index)
            {
                selectables[i].isSelected = false;
                selectedCards[i] = false;
            }
            else
            {
                selectables[i].isSelected = selected;
                selectedCards[i] = selected;
            }
        }
    }
}
