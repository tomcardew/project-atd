using UnityEngine;

public class AntitankBulletMovable : Movable
{
    public override void Init()
    {
        rotateTowards = GetComponentInChildren<RotateTowards>();
        rotateTowards.target = target;
        Destroy(gameObject, 5f);
    }

    public override GameObject NextTarget(GameObject target)
    {
        return target;
    }
}
