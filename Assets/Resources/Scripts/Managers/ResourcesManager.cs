using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ResourceToQuantity
{
    public ResourceType Resource { get; }
    public int Quantity { get; set; }

    public ResourceToQuantity(ResourceType resource, int quantity)
    {
        Resource = resource;
        Quantity = quantity;
    }
}

public class ResourcesManager : MonoBehaviour
{
    // Public properties
    public bool everythingFree = false;

    // Private properties
    private List<ResourceToQuantity> resources;

    private void Awake()
    {
        resources = new List<ResourceToQuantity>
        {
            new(ResourceType.Money, 0),
            new(ResourceType.Wood, 0),
            new(ResourceType.Faith, 0),
        };
    }

    public void AddToResource(ResourceType resource, int value)
    {
        var r = resources.First(r => r.Resource == resource);
        r.Quantity += value;
    }

    public int GetResourceValue(ResourceType resource)
    {
        return resources.First(r => r.Resource == resource).Quantity;
    }

    public bool CanPay(Card card)
    {
        if (everythingFree)
            return true;

        foreach (var item in card.resources)
        {
            if (GetResourceValue(item.resource) < item.value)
            {
                return false;
            }
        }
        return true;
    }

    public void Pay(Card card)
    {
        foreach (var item in card.resources)
        {
            AddToResource(item.resource, -item.value);
        }
    }
}
