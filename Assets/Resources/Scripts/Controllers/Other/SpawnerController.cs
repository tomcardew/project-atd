using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    // Public properties
    public GameObject[] prefabs; // Array of prefabs to instantiate
    public int quantity; // Number of objects to spawn each time
    public float delay; // Delay between spawns
    public int maxObjects; // Maximum number of objects allowed
    public bool infiniteObjects = false; // Flag to allow infinite objects
    public float spawnRadius = 1f; // Radius within which to spawn objects
    public bool shouldGenerate = true; // Flag to enable/disable the spawner

    [Header("Enemy Spawn Configuration")]
    public bool useEnemySpawnConfiguration = false;
    public int currentWave = 0;

    [NonSerialized]
    public bool spawnAsChildren = true; // Flag to spawn objects as children of the spawner

    // Private properties
    private Coroutine spawnRoutine; // Coroutine for the spawn routine

    private void Start()
    {
        // Start the spawn routine coroutine
        spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    private void OnDisable()
    {
        // Stop the spawn routine coroutine if it is running
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }

    /// <summary>
    /// Coroutine to handle the spawning of objects.
    /// </summary>
    /// <returns>IEnumerator for coroutine.</returns>
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // Wait for the specified delay
            yield return new WaitForSeconds(delay);
            // Check if we can spawn more objects
            if (shouldGenerate && (infiniteObjects || transform.childCount < maxObjects))
            {
                // Spawn the specified quantity of objects
                for (int i = 0; i < quantity; i++)
                {
                    // Calculate a random position within the spawn radius
                    Vector2 randomPosition =
                        (Vector2)transform.position
                        + UnityEngine.Random.insideUnitCircle * spawnRadius;

                    // Instantiate the prefab at the random position
                    InstantiatePrefab(randomPosition);
                }
            }
        }
    }

    /// <summary>
    /// Instantiate the prefab at the given position.
    /// </summary>
    /// <param name="position">Position to instantiate the prefab.</param>
    private void InstantiatePrefab(Vector2 position)
    {
        GameObject prefabToInstantiate = GetNextPrefab();
        if (spawnAsChildren)
        {
            Instantiate(prefabToInstantiate, position, Quaternion.identity, transform);
        }
        else
        {
            // Instantiate the prefab at a random position with no rotation
            GameObject prefabInstance = Instantiate(
                prefabToInstantiate,
                position,
                Quaternion.identity
            );

            // Instantiate a placeholder GameObject at the spawner's position
            GameObject placeholderInstance = Instantiate(
                new GameObject("placeholder"),
                transform.position,
                Quaternion.identity,
                transform
            );

            // Add the LinkedObject component to the placeholder and link it to the prefab instance
            LinkedObject linkedObject = placeholderInstance.AddComponent<LinkedObject>();
            linkedObject.linkedObject = prefabInstance;

            // Add the LinkedObject component to the prefab instance and link it to the placeholder
            LinkedObject linkedObject2 = prefabInstance.AddComponent<LinkedObject>();
            linkedObject2.linkedObject = placeholderInstance;
        }
    }

    /// <summary>
    /// Get the next prefab to instantiate.
    /// </summary>
    /// <returns>GameObject prefab to instantiate.</returns>
    private GameObject GetNextPrefab()
    {
        var _prefabs = new List<GameObject>(prefabs);
        if (useEnemySpawnConfiguration)
        {
            _prefabs = Enemies
                .GetAll()
                .Where(unit => unit.appearAtRound <= currentWave)
                .Select(unit => unit.prefab)
                .ToList();
        }
        return _prefabs[UnityEngine.Random.Range(0, _prefabs.Count)];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
