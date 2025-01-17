using UnityEngine;

public class AssasinMovable : Movable
{
    // Private properties
    private Attacker attacker;

    public override void Init()
    {
        useTargetTags = true;
        targetTags = new() { Tags.Soldier, Tags.Castle };
        attacker = GetComponentInChildren<Attacker>();
    }

    public override GameObject NextTarget(GameObject _target)
    {
        if (_target != null)
        {
            rotateTowards.target = _target.transform.position;
            attacker.targetDamageable = _target.GetComponentInChildren<Damageable>();
        }
        return _target;
    }
}
