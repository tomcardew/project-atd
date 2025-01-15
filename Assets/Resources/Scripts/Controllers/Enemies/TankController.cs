using System.Collections;
using System.Linq;
using UnityEngine;

public class TankControlller : MonoBehaviour
{
    // Public properties
    public Vector3 firePointOffset; // Where bullets are instantiated
    public float fireRate;
    public float fireRateMultiplier = 1.0f;

    // Private properties
    private TankMovable tankMovable;
    private GameObject targetEnemy;
    private Coroutine attackCoroutine;
    private bool hasBeenEnabled = false;

    private float CurrentFireRate
    {
        get { return fireRate * fireRateMultiplier; }
    }

    private void Start()
    {
        tankMovable = GetComponentInParent<TankMovable>();
        attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    private void Update()
    {
        tankMovable.move = !hasBeenEnabled;
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
        var tags = new string[] { Tags.Castle, Tags.Structure };
        if (tags.Contains(other.transform.parent.tag))
        {
            hasBeenEnabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var tags = new string[] { Tags.Castle, Tags.Structure };
        GameObject obj = other.transform.parent.gameObject;
        if (hasBeenEnabled && tags.Contains(other.transform.parent.tag))
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
            Prefabs.TankBullet,
            transform.position + firePointOffset,
            Quaternion.identity
        );
        TankBulletMovable bulletMovable = bullet.GetComponent<TankBulletMovable>();
        bulletMovable.target = targetEnemy.transform.position;
    }
}
