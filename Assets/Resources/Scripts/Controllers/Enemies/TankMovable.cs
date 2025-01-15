using UnityEngine;

public class TankMovable : Movable
{
    public override void Init()
    {
        useTargetTags = true;
        targetTags = new() { Tags.Castle, Tags.Structure };
        rotateTowards = GetComponentInChildren<RotateTowards>();
    }

    public override GameObject NextTarget(GameObject _target)
    {
        if (_target != null)
        {
            rotateTowards.target = _target.transform.position;
        }
        return _target;
    }
}
