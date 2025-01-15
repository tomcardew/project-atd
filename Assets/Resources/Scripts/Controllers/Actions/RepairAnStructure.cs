using UnityEngine;
using System.Collections;

public class RepairAnStructure : MonoBehaviour
{
    private void Start()
    {
        Manager.Interactions.SelectingTargetFinished += HandleSelectedTarget;
        Manager.Interactions.TriggerSelectingTarget(InteractionTarget.Structures);
    }

    private void OnDestroy()
    {
        Manager.Interactions.SelectingTargetFinished -= HandleSelectedTarget;
    }

    private void HandleSelectedTarget(GameObject selected)
    {
        Damageable dmg = selected.GetComponentInChildren<Damageable>();
        if (dmg != null)
        {
            dmg.Repair();
        }
        Destroy(gameObject);
    }
}
