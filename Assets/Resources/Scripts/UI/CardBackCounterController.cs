using UnityEngine;

public class CardBackCounterController : MonoBehaviour
{
    private CardBackUIController[] controllers;

    private void Start()
    {
        controllers = GetComponentsInChildren<CardBackUIController>();
    }

    private void Update()
    {
        controllers[0].UpdateData(Manager.Cards.deck.Count, "Deck");
        controllers[1].UpdateData(Manager.Cards.discarded.Count, "Discarded");
    }
}
