using System.Collections.Generic;
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
        GameObject obj = other.gameObject.transform.parent.gameObject;
        Damageable dmg = obj.GetComponentInChildren<Damageable>();
        if (dmg != null && dmg.armorLevel == ArmorLevel.Light && obj.CompareTag(Tags.Enemy)) // only attack light armor
        {
            dmg.ReceiveDamage(CurrentDamage);

            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<ArrowAttacker>().enabled = false;
            gameObject.GetComponentInParent<ArrowMovable>().enabled = false;
            Destroy(gameObject.transform.parent.gameObject, 5f);
        }
    }
}
