using UnityEngine;

public class SoldierMovable : Movable
{
    // Private properties
    private Attacker attacker;

    public override void Init()
    {
        targetArmorLevelTag = Tags.Enemy;
        targetArmorLevel = ArmorLevel.Light;
        useTargetArmorLevel = true;
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
            return Utils.FindTheNearestGameObjectWithTag(transform, Tags.Structure, 10f);
        }
        return _target;
    }
}
