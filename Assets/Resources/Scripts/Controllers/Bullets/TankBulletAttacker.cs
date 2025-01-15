using System.Linq;
using UnityEngine;

public class TankBulletAttacker : MonoBehaviour
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
        var tags = new string[] { Tags.Castle, Tags.Structure };
        if (tags.Contains(other.transform.parent.tag))
        {
            Damageable dmg = other.GetComponentInParent<Damageable>();
            dmg.ReceiveDamage(CurrentDamage);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
