using UnityEngine;
using System.Collections;

public class RepairAnStructure : TargetInteractor
{
    protected override InteractionTarget GetTarget()
    {
        return InteractionTarget.Structures;
    }

    protected override void InteractWithTarget(GameObject selected)
    {
        Damageable dmg = selected.GetComponentInChildren<Damageable>();
        if (dmg != null)
        {
            dmg.Repair();
        }

        Destroy(gameObject);
    }
}
