using System;
using UnityEngine;

public class EnemyMovable : Movable
{
    // Private properties
    private Attacker attacker;

    public override void Init()
    {
        useTargetTags = true;
        targetTags = new() { Tags.Castle };
        attacker = GetComponentInChildren<Attacker>();
    }

    public override GameObject NextTarget(GameObject _target)
    {
        if (_target != null)
        {
            attacker.targetDamageable = _target.GetComponentInChildren<Damageable>();
        }
        return _target;
    }
}
