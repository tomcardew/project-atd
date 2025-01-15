using System;
using UnityEngine;

public class SoldierPostController : MonoBehaviour
{
    // Public properties
    [Header("Spawn settings")]
    public int initialQuantity;
    public float quantityMultiplier = 1.0f;
    public float initialDelay;
    public float delayMultiplier = 1.0f;
    public int initialMaxSoldiers;
    public float maxSoldiersMultiplier = 1.0f;

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

    private void Start()
    {
        SpawnerController spawner = Instantiate(
                Prefabs.Spawner,
                transform.position,
                Quaternion.identity,
                transform
            )
            .GetComponent<SpawnerController>();
        spawner.prefabs = new GameObject[] { Prefabs.Soldier };
        spawner.quantity = (int)CurrentQuantity;
        spawner.delay = CurrentDelay;
        spawner.maxObjects = (int)CurrentMaxSoldiers;
        spawner.spawnAsChildren = false;
        spawner.spawnRadius = 0.1f;
    }
}
