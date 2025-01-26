using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmController : MonoBehaviour
{
    public List<ResourceGenerator> resourceGenerators;
    public string multiplierIdentifier;

    private void Awake()
    {
        resourceGenerators = new();
        multiplierIdentifier = System.Guid.NewGuid().ToString();
    }

    private void OnDestroy()
    {
        foreach (ResourceGenerator rg in resourceGenerators)
        {
            if (rg != null)
                rg.multipliers.RemoveAll(m => m.id == multiplierIdentifier);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ResourceGenerator rg = other.gameObject.GetComponent<ResourceGenerator>();
        if (rg != null)
        {
            AddMultipliers(rg);
            resourceGenerators.Add(rg);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ResourceGenerator rg = other.gameObject.GetComponent<ResourceGenerator>();
        if (rg != null)
        {
            rg.multipliers.RemoveAll(m => m.id == multiplierIdentifier);
            resourceGenerators.Remove(rg);
        }
    }

    private void AddMultipliers(ResourceGenerator rg)
    {
        rg.multipliers.Add(
            new MultiplierItem
            {
                id = multiplierIdentifier,
                key = MultiplierTags.ResourceGenerator.GenerationQuantity,
                value = 0.5f
            }
        );
        rg.multipliers.Add(
            new MultiplierItem
            {
                id = multiplierIdentifier,
                key = MultiplierTags.ResourceGenerator.GenerationDelay,
                value = -0.1f
            }
        );
    }
}
