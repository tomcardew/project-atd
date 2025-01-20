using UnityEngine;

public class DiscardAllAndDraw5 : MonoBehaviour
{
    void Start()
    {
        for (int i = Manager.Cards.hand.Count - 1; i > -1; i--)
        {
            Manager.Cards.Discard(i);
        }
        for (int i = 0; i < 5; i++)
        {
            Manager.Cards.Draw();
        }
        Destroy(gameObject);
    }
}
