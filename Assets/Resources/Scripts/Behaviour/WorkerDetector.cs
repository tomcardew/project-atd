using UnityEngine;

public class WorkerDetector : MonoBehaviour
{
    // Reference to the resource generator
    private ResourceGenerator resourceGenerator;

    private void Start()
    {
        // Get the ResourceGenerator component from the parent object
        resourceGenerator = transform.parent.GetComponentInChildren<ResourceGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Get the PersonMovable component from the object entering the trigger
        var person = other.GetComponentInParent<PersonMovable>();
        // If the person is not going home
        if (!person.isGoingHome)
        {
            // Add the person as a worker to the resource generator
            resourceGenerator.AddWorker(person.person);
            // Destroy the parent object of the collider
            Destroy(other.transform.parent.gameObject);
        }
    }
}
