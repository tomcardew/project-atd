using System;
using System.Collections;
using UnityEngine;

public abstract class PrefabSpawner : MonoBehaviour
{
    // Public properties
    [Header("Spawn settings")]
    public int initialQuantity; // Initial number of prefabs to spawn
    public float quantityMultiplier = 1.0f; // Multiplier for the quantity of prefabs
    public float initialDelay; // Initial delay before spawning starts
    public float delayMultiplier = 1.0f; // Multiplier for the delay between spawns
    public bool isInfinite = false; // Flag to control whether spawning is infinite
    public int initialMaxQuantity; // Initial maximum number of prefabs that can be spawned
    public float maxQuantityMultiplier = 1.0f; // Multiplier for the maximum quantity of prefabs
    public bool shouldSpawn = true; // Flag to control whether spawning should occur

    public abstract void Init(); // Abstract method to initialize the spawner
    public abstract GameObject GetPrefab(); // Abstract method to get the prefab to spawn
    public abstract void DidSpawnPrefab(GameObject prefab); // Abstract method called when a prefab is spawned

    // Private properties
    private float CurrentQuantity
    {
        get { return initialQuantity * quantityMultiplier; } // Calculate the current quantity of prefabs to spawn
    }

    private float CurrentDelay
    {
        get { return initialDelay * delayMultiplier; } // Calculate the current delay between spawns
    }

    private float CurrentMaxQuantity
    {
        get { return initialMaxQuantity * maxQuantityMultiplier; } // Calculate the current maximum quantity of prefabs
    }

    private Coroutine spawnRoutine; // Coroutine for the spawning routine
    private GameObject prefabHolder; // GameObject to hold the spawned prefabs

    private void Start()
    {
        Init(); // Initialize the spawner
        prefabHolder = new GameObject("PrefabHolder"); // Create a new GameObject to hold the prefabs
        spawnRoutine = StartCoroutine(SpawnRoutine()); // Start the spawning routine
    }

    private void OnDisable()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine); // Stop the spawning routine if it is running
            spawnRoutine = null; // Clear the reference to the coroutine
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(CurrentDelay); // Wait for the current delay before spawning
            if (shouldSpawn)
            {
                for (int i = 0; i < CurrentQuantity; i++) // Spawn the current quantity of prefabs
                {
                    if (isInfinite || prefabHolder.transform.childCount < CurrentMaxQuantity) // Check if the current number of prefabs is less than the maximum
                    {
                        GameObject prefab = Instantiate(
                            GetPrefab(), // Get the prefab to spawn
                            transform.position, // Set the position to spawn the prefab
                            Quaternion.identity, // Set the rotation to identity
                            prefabHolder.transform // Set the parent to the prefab holder
                        );
                        DidSpawnPrefab(prefab); // Call the method to handle the spawned prefab
                    }
                }
            }
        }
    }
}
