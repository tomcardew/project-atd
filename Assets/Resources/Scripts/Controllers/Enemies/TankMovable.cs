using UnityEngine;

public class TankMovable : Movable
{
    // Private properties
    private Attacker attacker;
    private RotateTowards rotateTowards;

    public override void Init()
    {
        useTargetTags = true;
        targetTags = new() { Tags.Castle, Tags.Structure };
        attacker = GetComponentInChildren<Attacker>();
        rotateTowards = GetComponentInChildren<RotateTowards>();
    }

    public override GameObject NextTarget(GameObject _target)
    {
        if (_target != null)
        {
            attacker.targetDamageable = _target.GetComponentInChildren<Damageable>();
            rotateTowards.target = _target.transform.position;
        }
        return _target;
    }
}
