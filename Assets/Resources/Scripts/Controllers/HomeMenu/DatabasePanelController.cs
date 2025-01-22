using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DatabasePanelController : MonoBehaviour
{
    [Header("Configuration")]
    public Button backButton;
    public Transform listPanel;

    private void Start()
    {
        AddCardsToPanel();
        GetComponent<ScrollRect>().verticalScrollbar.value = 1;
        backButton.onClick.AddListener(Back);
    }

    private void Back()
    {
        gameObject.SetActive(false);
    }

    private void AddCardsToPanel()
    {
        var cards = Cards.AllCards.OrderByDescending(c => c.foregroundType).ToArray();
        for (int i = 0; i < cards.Length; i++)
        {
            var card = cards[i];
            var cardPrefab = Instantiate(
                Prefabs.GetPrefab(Prefabs.OtherType.CardUI),
                Vector2.zero,
                Quaternion.identity,
                listPanel
            );
            // change scale of the card
            cardPrefab.transform.localScale = Vector3.one;
            // disable draggable component
            cardPrefab.GetComponent<Draggable>().enabled = false;
            // set card data
            var cardUI = cardPrefab.GetComponentInChildren<CardUIController>();
            cardUI.card = card;
        }
    }
}
