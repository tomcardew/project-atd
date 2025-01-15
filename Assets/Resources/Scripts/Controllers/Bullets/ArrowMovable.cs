using UnityEngine;

public class ArrowMovable : Movable
{
    private RotateTowards rotateTowards;

    public override void Init()
    {
        rotateTowards = GetComponentInChildren<RotateTowards>();
        rotateTowards.target = target;
        rotateTowards.offset = 90f;
        Destroy(gameObject, 5f);
    }

    public override GameObject NextTarget(GameObject _target)
    {
        rotateTowards.target = _target.transform.position;
        if (Vector2.Distance(transform.position, _target.transform.position) < 0.2f)
        {
            GetComponentInChildren<Collider2D>().enabled = false;
            GetComponentInChildren<ArrowAttacker>().enabled = false;
            Destroy(gameObject, 5f);
            move = false;
            enabled = false;
            return null;
        }
        return _target;
    }
}
