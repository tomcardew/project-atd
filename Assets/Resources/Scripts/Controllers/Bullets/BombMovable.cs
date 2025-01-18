using UnityEngine;

public class BombMovable : Movable
{
    public override void Init() { }

    public override GameObject NextTarget(GameObject target)
    {
        return target;
    }
}
