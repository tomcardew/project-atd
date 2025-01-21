using UnityEngine;

public abstract class TargetInteractor : MonoBehaviour
{
    private void Start()
    {
        Manager.Interactions.SelectingTargetFinished += HandleSelectedTarget;
        Manager.Interactions.TriggerSelectingTarget(GetTarget());
    }

    private void OnDestroy()
    {
        Manager.Interactions.SelectingTargetFinished -= HandleSelectedTarget;
    }

    private void HandleSelectedTarget(GameObject selected)
    {
        InteractWithTarget(selected);
    }

    protected abstract InteractionTarget GetTarget();
    protected abstract void InteractWithTarget(GameObject selected);
}
