using System;
using UnityEngine;

public enum ArmorLevel
{
    None = 0, // can be damaged by all
    Light = 1, // can be damaged by light and heavy
    Heavy = 2 // can be damaged by heavy
}

/* Damageable makes an object have a life and be a target for Attackers objects */
public class Damageable : MonoBehaviour
{
    // Public properties
    public float initialLife; // Initial life of the object
    public float lifeMultiplier = 1f; // Multiplier to adjust the life
    public SliderController healtbar; // Reference to the health bar
    public ArmorLevel armorLevel; // Armor level of the object
    public bool repairable = false; // If the object can be repaired

    [NonSerialized]
    public bool isUnderRepair = false; // If the object is under repair

    // Private properties
    private GameObject parent; // Reference to the parent GameObject
    public float receivedDamage = 0f; // Total damage received

    // Computed property to get the current life
    private float CurrentLife
    {
        get { return (initialLife * lifeMultiplier) - receivedDamage; }
    }

    private void Start()
    {
        // Initialize the parent GameObject reference
        parent = transform.parent.gameObject;
    }

    /// <summary>
    /// The ReceiveDamage function reduces the object's life by a specified amount and destroys the
    /// object if its life reaches zero.
    /// </summary>
    /// <param name="damage">The `damage` parameter represents the amount of damage that the object will
    /// receive.</param>
    public void ReceiveDamage(float damage)
    {
        receivedDamage += damage; // Add the received damage to the total damage
        if (CurrentLife <= 0) // Check if the object's life has reached zero
        {
            Destroy(parent); // Destroy the parent GameObject
        }
        UpdateHealthbar(); // Update the health bar
    }

    public void Repair()
    {
        this.receivedDamage = 0;
        UpdateHealthbar();
    }

    private void UpdateHealthbar()
    {
        // Update the health bar value
        if (healtbar != null)
        {
            float value = CurrentLife / (initialLife * lifeMultiplier);
            healtbar.SetValue(value);
            if (value < 0.3f)
            {
                healtbar.ChangeColor(Color.red);
            }
            else if (value < 0.6f)
            {
                healtbar.ChangeColor(Color.yellow);
            }
            else
            {
                healtbar.ChangeColor(Color.green);
            }
        }
    }
}
