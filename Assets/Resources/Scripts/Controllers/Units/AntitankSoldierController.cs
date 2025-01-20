using System.Collections;
using System.Linq;
using UnityEngine;

public class AntitankSoldierControlller : MonoBehaviour
{
    // Public properties
    public Vector3 firePointOffset; // Where bullets are instantiated
    public float fireRate;
    public float fireRateMultiplier = 1.0f;

    // Private properties
    private AntitankSoldierMovable movable;
    private GameObject targetEnemy;
    private Coroutine attackCoroutine;
    private bool hasBeenEnabled = false;

    private float CurrentFireRate
    {
        get { return fireRate * fireRateMultiplier; }
    }

    private void Start()
    {
        movable = GetComponentInParent<AntitankSoldierMovable>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject.transform.parent.gameObject;
        Damageable dmg = obj.GetComponentInChildren<Damageable>();
        if (dmg != null && dmg.armorLevel == ArmorLevel.Heavy) // only attack heavy armor
        {
            hasBeenEnabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        GameObject obj = other.gameObject.transform.parent.gameObject;
        Damageable dmg = obj.GetComponentInChildren<Damageable>();
        if (
            hasBeenEnabled
            && dmg != null
            && dmg.armorLevel == ArmorLevel.Heavy
            && obj.CompareTag(Tags.Enemy)
        )
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
            Prefabs.GetPrefab(Prefabs.BulletType.AntitankBullet),
            transform.position + firePointOffset,
            Quaternion.identity
        );
        AntitankBulletMovable bulletMovable = bullet.GetComponent<AntitankBulletMovable>();
        bulletMovable.target = targetEnemy.transform.position;
    }
}
