using UnityEngine;

public class DiscardAndDraw : TargetInteractor
{
    protected override InteractionTarget GetTarget()
    {
        Manager.UI.SetTooltip("Select the target card you want to discard");
        return InteractionTarget.Card;
    }

    protected override void InteractWithTarget(GameObject selected)
    {
        CardUIController controller = selected.GetComponent<CardUIController>();
        if (controller != null)
        {
            Manager.Cards.Discard(controller.index);
            Manager.Cards.Draw();
        }

        Manager.UI.ClearTooltip();
        Destroy(gameObject);
    }
}
