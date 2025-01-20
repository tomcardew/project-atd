using System;
using UnityEngine;

public class PersonMovable : Movable
{
    // Public properties
    public Person person;

    [NonSerialized]
    public bool isGoingHome = false;

    // Initialize the PersonMovable object
    public override void Init() { }

    // Update the target position based on the current state
    public override GameObject NextTarget(GameObject _target)
    {
        if (rotateTowards != null)
            rotateTowards.target = target;
        if (isGoingHome)
        {
            // If the person has no population, destroy the game object
            if (person.population == null)
            {
                Destroy(gameObject);
                return null;
            }
            // Set the target to the population's position
            return person.population.gameObject;
        }
        else
        {
            // Find the next target resource
            return FindNextTarget();
        }
    }

    // Find the next available resource generator and set it as the target
    private GameObject FindNextTarget()
    {
        ResourceGenerator generator = Utils.GetAvailableResource(
            gameObject.transform.position,
            float.MaxValue
        );
        return generator.gameObject;
    }
}
