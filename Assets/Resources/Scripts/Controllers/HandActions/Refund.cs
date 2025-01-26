using System.Linq;
using UnityEngine;

public class Refund : TargetInteractor
{
    protected override InteractionTarget GetTarget()
    {
        return InteractionTarget.Card;
    }

    protected override void InteractWithTarget(GameObject selected)
    {
        CardUIController controller = selected.GetComponent<CardUIController>();
        if (controller != null)
        {
            Manager.Cards.Discard(controller.index);
            var value = controller.card.resources
                .Where(r => r.resource == ResourceType.Money)
                .Sum(r => r.value);
            Debug.Log("Refund: " + value);
            Manager.Resources.AddToResource(ResourceType.Money, value);
        }

        Destroy(gameObject);
    }
}
