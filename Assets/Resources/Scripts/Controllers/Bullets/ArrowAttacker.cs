using Unity.VisualScripting;
using UnityEngine;

public class ArrowAttacker : MonoBehaviour
{
    // Public variables
    public float initialDamage;
    public float damageMultiplier = 1.0f;

    private float CurrentDamage
    {
        get { return initialDamage * damageMultiplier; }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.parent.CompareTag("Enemy"))
        {
            Damageable dmg = other.GetComponentInParent<Damageable>();
            dmg.ReceiveDamage(CurrentDamage);

            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<ArrowAttacker>().enabled = false;
            gameObject.GetComponentInParent<ArrowMovable>().enabled = false;
            Destroy(gameObject.transform.parent.gameObject, 5f);
        }
    }
}
