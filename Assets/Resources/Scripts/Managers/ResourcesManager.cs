using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ResourceToQuantity
{
    public ResourceType Resource { get; }
    public int Quantity { get; set; }
    public int Limit { get; set; }

    public ResourceToQuantity(ResourceType resource, int quantity, int limit)
    {
        Resource = resource;
        Quantity = quantity;
        Limit = limit;
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
            new(ResourceType.Money, 0, 1000),
            new(ResourceType.Food, 0, 1000),
            new(ResourceType.Wood, 0, 250),
            new(ResourceType.Faith, 0, 250),
        };
    }

    public void AddToResource(ResourceType resource, int value)
    {
        var r = resources.First(r => r.Resource == resource);
        if (r.Quantity + value <= r.Limit)
        {
            r.Quantity += value;
        }
        else
        {
            r.Quantity = r.Limit;
        }
    }

    public int GetResourceValue(ResourceType resource)
    {
        return resources.First(r => r.Resource == resource).Quantity;
    }

    public bool HasReachedLimit(ResourceType resource)
    {
        ResourceToQuantity r = resources.First(r => r.Resource == resource);
        return r.Quantity == r.Limit;
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

    public bool CanPay(ResourceType resource, int value)
    {
        return everythingFree || GetResourceValue(resource) >= value;
    }

    public void Pay(Card card)
    {
        foreach (var item in card.resources)
        {
            AddToResource(item.resource, -item.value);
        }
    }
}
