using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoldierMovable : Movable
{
    // Private properties
    private Attacker attacker;

    public override void Init()
    {
        targetIdentifiers = new() { "Enemy", "LargeEnemy" };
        useTargetIdentifiers = true;
        attacker = GetComponentInChildren<Attacker>();
    }

    public override GameObject NextTarget(GameObject _target)
    {
        if (rotateTowards != null)
            rotateTowards.target = target;
        if (_target != null)
        {
            attacker.targetDamageable = _target.GetComponentInChildren<Damageable>();
        }
        else
        {
            return Utils.FindTheNearestGameObjectWithTag(transform, Tags.Structure, float.MaxValue);
        }
        return _target;
    }
}
