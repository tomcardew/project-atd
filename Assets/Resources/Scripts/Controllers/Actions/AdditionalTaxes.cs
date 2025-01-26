using UnityEngine;

public class AdditionalTaxes : MonoBehaviour
{
    private void Start()
    {
        CastleController castleController = GameObject.FindAnyObjectByType<CastleController>();
        Manager.Resources.AddToResource(ResourceType.Money, castleController.GetTaxValue());

        Manager.Cards.DestroyCard(Cards.AdditionalTaxes.name);

        Destroy(gameObject);
    }
}
