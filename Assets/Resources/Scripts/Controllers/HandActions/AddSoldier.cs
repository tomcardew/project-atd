using UnityEngine;

public class AddSoldier : MonoBehaviour
{
    private void Start()
    {
        int index = Manager.Cards.FindIndexOfCardWithNameOnDeck(Cards.SoldierTent.name);
        if (index > -1)
        {
            Manager.Cards.MoveDeckIndexToHand(index);
        }
        Destroy(gameObject);
    }
}
