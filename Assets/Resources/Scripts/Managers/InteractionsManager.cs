using UnityEngine;

public enum InteractionTarget
{
    Structures,
    Resources,
    Card
}

public class InteractionsManager : MonoBehaviour
{
    // Target has started
    public delegate void OnSelectingTargetStarted(InteractionTarget target);
    public event OnSelectingTargetStarted SelectingTargetStarted;

    // Target has finished
    public delegate void OnSelectingTargetFinished(GameObject selected);
    public event OnSelectingTargetFinished SelectingTargetFinished;

    public void TriggerSelectingTarget(InteractionTarget target)
    {
        Manager.UI.SetTooltip($"Select a {target} to interact with");
        SelectingTargetStarted?.Invoke(target);
    }

    public void TriggerTargetFinished(GameObject selected)
    {
        SelectingTargetFinished?.Invoke(selected);
        Manager.UI.ClearTooltip();
    }
}
