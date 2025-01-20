using UnityEngine;

public class DrawACard : MonoBehaviour
{
    void Start()
    {
        Manager.Cards.Draw();
        Destroy(gameObject);
    }
}
