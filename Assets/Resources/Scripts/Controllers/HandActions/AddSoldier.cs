using UnityEngine;

public class AddSoldier : MonoBehaviour
{
    private void Start()
    {
        int index = Manager.Cards.GetRandomCardWithNames(
            new string[] { Cards.AntitankSoldierTent.name, Cards.SoldierTent.name }
        );
        if (index > -1)
        {
            Manager.Cards.MoveDeckIndexToHand(index);
        }
        else
        {
            // Manager.UI.ShowNotification("No matching card found");
        }
        Destroy(gameObject);
    }
}
