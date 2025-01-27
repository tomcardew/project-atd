using UnityEngine;

public class ArcherMovable : Movable
{
    public override void Init()
    {
        // Attack only light enemies
        targetArmorLevelTag = Tags.Enemy;
        targetArmorLevel = ArmorLevel.Light;
        useTargetArmorLevel = true;
    }

    public override GameObject NextTarget(GameObject _target)
    {
        if (rotateTowards != null)
            rotateTowards.target = target;
        if (_target != null)
        {
            rotateTowards.target = _target.transform.position;
        }
        else
        {
            return Utils.FindTheNearestGameObjectWithTag(transform, Tags.Structure, float.MaxValue);
        }
        return _target;
    }
}
