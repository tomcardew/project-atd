using UnityEngine;

public class TankBulletMovable : Movable
{
    public override void Init()
    {
        Destroy(gameObject, 5f);
    }

    public override GameObject NextTarget(GameObject _target)
    {
        if (_target == null)
        {
            Destroy(gameObject);
        }
        if (
            _target != null
            && Vector2.Distance(transform.position, _target.transform.position) < 0.2f
        )
        {
            GetComponentInChildren<Collider2D>().enabled = false;
            GetComponentInChildren<TankBulletAttacker>().enabled = false;
            Destroy(gameObject, 1f);
            move = false;
            enabled = false;
            return null;
        }
        return _target;
    }
}
