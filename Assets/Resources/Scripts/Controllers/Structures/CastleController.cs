using System.Collections;
using UnityEngine;

public class CastleController : MonoBehaviour
{
    // Public properties
    public float tax; // Base tax rate
    public float taxMultiplier = 1.0f; // Multiplier for the tax rate
    public float taxDelay; // Delay between tax collections
    public float taxDelayMultiplier = 1.0f; // Multiplier for the tax delay

    [Header("Entrance settings")]
    public Vector3 entranceOffset; // Offset for the entrance position

    // Private properties
    private Coroutine taxRoutine; // Coroutine for tax collection
    private NewResourceUIController newResourceUI;

    // Property to get the current tax rate
    private float CurrentTax
    {
        get { return tax * taxMultiplier; }
    }

    // Property to get the current tax delay
    private float CurrentTaxDelay
    {
        get { return taxDelay * taxDelayMultiplier; }
    }

    // Method called when the script starts
    private void Start()
    {
        newResourceUI = GetComponentInChildren<NewResourceUIController>(); // Get the NewResourceUIController component
        taxRoutine = StartCoroutine(TaxRoutine()); // Start the tax routine
    }

    // Method called when the script is disabled
    private void OnDisable()
    {
        if (taxRoutine != null)
        {
            StopCoroutine(taxRoutine); // Stop the tax routine
            taxRoutine = null;
        }
    }

    public Vector3 GetEntrancePosition()
    {
        return transform.position + entranceOffset;
    }

    public int GetTaxValue()
    {
        return (int)(CurrentTax * Manager.Population.GetPopulation());
    }

    // Tax collection routine
    private IEnumerator TaxRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(CurrentTaxDelay); // Wait for the current tax delay
            Manager.Resources.AddToResource(ResourceType.Money, GetTaxValue()); // Add the value to the resources
            newResourceUI.Show(); // Show the new resource UI
        }
    }

    // Method called when the script is destroyed
    private void OnDestroy()
    {
        Manager.Game.ExitGame(); // Call the method to exit the game
    }
}
