using System.Collections;
using System.Linq;
using UnityEngine;

public class SniperTowerController : MonoBehaviour
{
    public Vector3 firePointOffset; // Where bullets are instantiated
    public float fireRate;
    public float fireRateMultiplier = 1.0f;
    public GameObject sniperScopePrefab;

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
        GameObject obj = other.transform.parent.gameObject;
        Damageable dmg = obj.GetComponentInChildren<Damageable>();
        if (obj.CompareTag(Tags.Enemy) && dmg.armorLevel <= ArmorLevel.Light)
        {
            hasBeenEnabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        GameObject obj = other.transform.parent.gameObject;
        Damageable dmg = obj.GetComponentInChildren<Damageable>();
        if (
            hasBeenEnabled
            && targetEnemy == null
            && obj.CompareTag(Tags.Enemy)
            && dmg.armorLevel <= ArmorLevel.Light
        )
        {
            targetEnemy = obj;
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
                StartCoroutine(Shoot());
            }
        }
    }

    private IEnumerator Shoot()
    {
        var scope = Instantiate(
            sniperScopePrefab,
            targetEnemy.transform.position,
            Quaternion.identity
        );
        scope.transform.SetParent(targetEnemy.transform);

        yield return new WaitForSeconds(1.9f);
        var line = DrawLineToTarget(targetEnemy.transform.position);

        yield return new WaitForSeconds(0.1f);

        Destroy(scope);
        Destroy(line);
        Destroy(targetEnemy);
        targetEnemy = null;
    }

    private GameObject DrawLineToTarget(Vector3 targetPosition)
    {
        GameObject line = new GameObject("Line");
        line.transform.position = transform.position + firePointOffset;

        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        lineRenderer.SetPosition(0, transform.position + firePointOffset);
        lineRenderer.SetPosition(1, targetPosition);

        return line;
    }
}
