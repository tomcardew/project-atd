using UnityEngine;

public class AntitankSoldierMovable : Movable
{
    public override void Init()
    {
        useTargetIdentifiers = true;
        targetIdentifiers = new() { "Tank" }; // only heavy enemies
        rotateTowards = GetComponentInChildren<RotateTowards>();
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
