using UnityEngine;

public class AddHouse : MonoBehaviour
{
    private void Start()
    {
        int index = Manager.Cards.FindIndexOfCardWithNameOnDeck(Cards.House.name);
        if (index > -1)
        {
            Manager.Cards.MoveDeckIndexToHand(index);
        }
        Destroy(gameObject);
    }
}
