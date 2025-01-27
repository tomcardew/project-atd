using System.Collections;
using UnityEngine;

public abstract class BulletShooter : MonoBehaviour
{
    // Public properties
    // Where bullets are instantiated and how ofter are they fired
    [Header("Fire settings")]
    public Vector3 firePointOffset;
    public float fireRate;
    public float fireRateMultiplier = 1.0f;

    public abstract GameObject GetBulletPrefab();
    public abstract bool CanShoot(GameObject obj, Damageable dmg);

    // Private properties
    private Movable movable;
    private GameObject targetEnemy;
    private Coroutine attackCoroutine;
    private bool hasBeenEnabled = false;

    private float CurrentFireRate
    {
        get { return fireRate * (fireRateMultiplier + UnityEngine.Random.Range(-0.1f, 0.1f)); }
    }

    private void Start()
    {
        movable = GetComponentInParent<Movable>();
        attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    private void Update()
    {
        movable.move = !hasBeenEnabled;
    }

    private void OnDisable()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(CurrentFireRate);
            if (targetEnemy != null)
            {
                Shoot();
            }
            else
            {
                hasBeenEnabled = false;
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(
            GetBulletPrefab(),
            transform.position + firePointOffset,
            Quaternion.identity
        );
        Movable bulletMovable = bullet.GetComponent<Movable>();
        bulletMovable.target = targetEnemy.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject.transform.parent.gameObject;
        Damageable dmg = obj.GetComponentInChildren<Damageable>();
        if (dmg != null && CanShoot(obj, dmg))
        {
            hasBeenEnabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        GameObject obj = other.gameObject.transform.parent.gameObject;
        Damageable dmg = obj.GetComponentInChildren<Damageable>();
        if (hasBeenEnabled && dmg != null && CanShoot(obj, dmg))
        {
            float distanceToEnemy = Vector3.Distance(transform.position, obj.transform.position);
            if (
                targetEnemy == null
                || distanceToEnemy
                    < Vector3.Distance(transform.position, targetEnemy.transform.position)
            )
            {
                targetEnemy = obj;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + firePointOffset, 1.0f);
    }
}
