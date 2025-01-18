using System.Collections;
using UnityEngine;

public class BombAttacker : MonoBehaviour
{
    // Public variables
    public float initialDamage;
    public float damageMultiplier = 1.0f;
    public float timeBeforeExplosion;
    public float timeBeforeExplosionMultiplier = 1.0f;
    public SpriteRenderer spriteRenderer;

    private float CurrentDamage
    {
        get { return initialDamage * damageMultiplier; }
    }

    private float CurrentTimeBeforeExplosion
    {
        get { return timeBeforeExplosion * timeBeforeExplosionMultiplier; }
    }

    private bool explode = false;
    private Coroutine explosionRoutine;

    private void Start()
    {
        spriteRenderer.enabled = false;
        explosionRoutine = StartCoroutine(ExplosionRoutine());
    }

    private void OnDisable()
    {
        if (explosionRoutine != null)
        {
            StopCoroutine(explosionRoutine);
            explosionRoutine = null;
        }
    }

    private IEnumerator ExplosionRoutine()
    {
        yield return new WaitForSeconds(CurrentTimeBeforeExplosion);
        explode = true;
        spriteRenderer.enabled = true;
        yield return new WaitForFixedUpdate();
        Destroy(gameObject.transform.parent.gameObject);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (explode && other.gameObject.transform.parent.CompareTag(Tags.Enemy))
        {
            Damageable dmg = other.GetComponentInParent<Damageable>();
            dmg.ReceiveDamage(CurrentDamage);
        }
    }
}
