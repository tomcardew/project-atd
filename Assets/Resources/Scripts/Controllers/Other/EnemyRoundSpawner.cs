using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyRoundSpawner : PrefabSpawner
{
    public int currentRound = 0; // Current round number
    private List<Unit> prefabs; // List of all enemy prefabs
    private int availablePrefabsCount; // Count of available prefabs for the current round
    private bool hasAddedCards = false; // Flag to check if new cards have been added

    public override void Init()
    {
        prefabs = Enemies.GetAll().ToList();
        availablePrefabsCount = 0; // Initialize available prefabs count
        GameManager.OnWaveEnd += HandleWaveEnd; // Subscribe to the OnWaveEnd event
    }

    public override void DidSpawnPrefab(GameObject prefab) { }

    public override GameObject GetPrefab()
    {
        // Get the list of prefabs that can appear in the current round
        var availablePrefabs = prefabs
            .Where(p => p.appearAtRound <= currentRound)
            .Select(p => p.prefab)
            .ToList();

        // Check if the count of available prefabs has changed
        if (availablePrefabsCount < availablePrefabs.Count)
        {
            availablePrefabsCount = availablePrefabs.Count; // Update the count
            hasAddedCards = true; // Set the flag to true
        }

        // Return a random prefab from the available prefabs
        return availablePrefabs[Random.Range(0, availablePrefabs.Count)];
    }

    private void HandleWaveEnd()
    {
        // Update delay and quantity multiplier based on whether new cards were added
        if (hasAddedCards)
        {
            delayMultiplier = 1.0f; // Reset delay multiplier
            quantityMultiplier = 1.0f; // Reset quantity multiplier
            hasAddedCards = false; // Reset the flag
        }
        else
        {
            delayMultiplier = Mathf.Max(0.1f, delayMultiplier - 0.01f * currentRound); // Decrease delay multiplier with a minimum of 0.1f
            quantityMultiplier = 1 + (int)currentRound / 5; // Increase quantity multiplier
        }
    }
}
