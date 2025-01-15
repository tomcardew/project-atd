using System.Collections;
using UnityEngine;

public class ArcherTowerController : MonoBehaviour
{
    public Vector3 firePointOffset; // Where bullets are instantiated
    public float fireRate;
    public float fireRateMultiplier = 1.0f;

    private GameObject targetEnemy;
    private Coroutine attackCoroutine;
    private bool hasBeenEnabled = false;

    private float CurrentFireRate
    {
        get { return fireRate * fireRateMultiplier; }
    }

    private void Start()
    {
        attackCoroutine = StartCoroutine(AttackCoroutine());
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
        if (other.transform.parent.CompareTag(Tags.Enemy))
        {
            hasBeenEnabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        GameObject obj = other.transform.parent.gameObject;
        if (hasBeenEnabled && obj.CompareTag(Tags.Enemy))
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
        }
    }

    private void Shoot()
    {
        GameObject arrow = Instantiate(
            Prefabs.Arrow,
            transform.position + firePointOffset,
            Quaternion.identity
        );
        ArrowMovable arrowMovable = arrow.GetComponent<ArrowMovable>();
        arrowMovable.target = targetEnemy.transform.position;
    }
}
