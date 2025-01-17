using System;
using System.Collections;
using UnityEngine;

public abstract class PrefabSpawner : MonoBehaviour
{
    // Public properties
    [Header("Spawn settings")]
    public int initialQuantity;
    public float quantityMultiplier = 1.0f;
    public float initialDelay;
    public float delayMultiplier = 1.0f;
    public int initialMaxSoldiers;
    public float maxSoldiersMultiplier = 1.0f;

    public abstract GameObject GetPrefab(); // Get the prefab to spawn
    public abstract void DidSpawnPrefab(GameObject prefab); // Called when a prefab is spawned

    // Private properties
    private float CurrentQuantity
    {
        get { return initialQuantity * quantityMultiplier; }
    }

    private float CurrentDelay
    {
        get { return initialDelay * delayMultiplier; }
    }

    private float CurrentMaxSoldiers
    {
        get { return initialMaxSoldiers * maxSoldiersMultiplier; }
    }

    private Coroutine spawnRoutine;
    private GameObject prefabHolder;

    private void Start()
    {
        spawnRoutine = StartCoroutine(SpawnRoutine());
        prefabHolder = new GameObject("PrefabHolder");
    }

    private void OnDisable()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(CurrentDelay);
        for (int i = 0; i < CurrentQuantity; i++)
        {
            if (prefabHolder.transform.childCount < CurrentMaxSoldiers)
            {
                GameObject prefab = Instantiate(
                    GetPrefab(),
                    transform.position,
                    Quaternion.identity,
                    prefabHolder.transform
                );
                DidSpawnPrefab(prefab);
            }
        }
    }
}
