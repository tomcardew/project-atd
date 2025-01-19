using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirstDrawModalController : MonoBehaviour
{
    public Card[] cards; // List of selectable cards
    public GameObject scrollerPanel; // Panel containing the scroll view
    public TextMeshProUGUI countLabel; // Label displaying the number of selected cards
    public Button continueButton; // Button to continue to the next screen

    public bool[] selectedCards; // List of selectable cards transformed

    private void Start()
    {
        selectedCards = cards.Take(5).Select(c => false).ToArray(); // Initialize the list of selectable cards
        continueButton.onClick.AddListener(OnContinueClick); // Add a listener to the continue button
        AddCardsToPanel();
    }

    private void OnContinueClick()
    {
        List<Card> _selectedCards = cards.Where((c, i) => selectedCards[i]).ToList();
        List<Card> _unselectedCards = cards.Where((c, i) => !selectedCards[i]).ToList();
        Manager.Cards.SetFirstDrawCards(_selectedCards.ToArray(), _unselectedCards.ToArray());
        Manager.Time.isPaused = false;
        Destroy(gameObject); // Destroy the modal
    }

    private void Update()
    {
        if (selectedCards != null)
        {
            countLabel.text = $"{selectedCards.Count(c => c)} selected"; // Update the count label
        }
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
        }
    }

    private void HandleCardSelected(int index, bool selected)
    {
        selectedCards[index] = selected;
    }
}
