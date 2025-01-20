using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    // Public properties
    public float initialDamage; // Initial damage amount
    public float damageMultiplier = 1.0f; // Multiplier to adjust the damage
    public float initialDamageDelay; // Initial delay before attacking
    public float initialDamageDelayMultiplier = 1.0f; // Multiplier to adjust the damage delay

    [System.NonSerialized]
    public Damageable targetDamageable; // Reference to the target Damageable object

    // Private properties
    private Damageable ownDamageable;
    private bool canDealDamage = false; // Flag to determine if the attacker can deal damage
    private Coroutine dealDamageRoutine; // Coroutine for dealing damage

    // Computed property to get the current damage
    private float CurrentDamage
    {
        get { return initialDamage * damageMultiplier; }
    }

    // Computed property to get the current damage delay
    private float CurrentDamageDelay
    {
        get { return initialDamageDelay * initialDamageDelayMultiplier; }
    }

    private void Start()
    {
        ownDamageable = transform.parent.GetComponentInChildren<Damageable>();
        // Start the damage routine coroutine
        dealDamageRoutine = StartCoroutine(DamageRoutine());
    }

    private void OnDisable()
    {
        // Stop the damage routine coroutine if it is running
        if (dealDamageRoutine != null)
        {
            StopCoroutine(dealDamageRoutine);
            dealDamageRoutine = null;
        }
    }

    /// <summary>
    /// Coroutine to handle the damage dealing process.
    /// </summary>
    /// <returns>IEnumerator for coroutine.</returns>
    private IEnumerator DamageRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(CurrentDamageDelay);
            if (canDealDamage && targetDamageable != null)
            {
                targetDamageable.ReceiveDamage(CurrentDamage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If a Damageable object reaches the Attack area, it can deal damage
        UpdateCanDealDamage(other, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        UpdateCanDealDamage(other, false);
    }

    private void UpdateCanDealDamage(Collider2D collider, bool value)
    {
        Damageable dmg = collider.transform.parent.GetComponentInChildren<Damageable>();
        if (dmg != null && targetDamageable == dmg && ownDamageable.armorLevel >= dmg.armorLevel)
        {
            canDealDamage = value;
        }
    }
}
