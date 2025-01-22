using System.Linq;
using UnityEngine;

public class EnemyRoundSpawner : PrefabSpawner
{
    public int currentRound = 0; // Current round number

    public override void Init()
    {
        GameManager.OnWaveEnd += HandleWaveEnd; // Subscribe to the OnWaveEnd event
    }

    public override void DidSpawnPrefab(GameObject prefab) { }

    public override GameObject GetPrefab()
    {
        // Get the list of prefabs that can appear in the current round
        // and order it by weight
        var availablePrefabs = GetUnlockedEnemies().OrderByDescending(c => c.weight).ToArray();

        float totalWeight = availablePrefabs.Sum(c => c.weight);

        float randomValue = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;

        foreach (var weightedPrefab in availablePrefabs)
        {
            cumulativeWeight += weightedPrefab.weight;
            if (randomValue < cumulativeWeight)
            {
                return weightedPrefab.prefab;
            }
        }

        return availablePrefabs[0].prefab;
    }

    private void HandleWaveEnd()
    {
        // Update delay and quantity multiplier based on whether new cards were added
        delayMultiplier = GetDelayMultiplier(); // Decrease delay multiplier with a minimum of 0.1f
        quantityMultiplier = GetQuantityMultiplier(); // Increase quantity multiplier

        Debug.Log(
            $"Updated -> Delay multiplier: {delayMultiplier}, Quantity multiplier: {quantityMultiplier}"
        );
    }

    private float GetDelayMultiplier()
    {
        int totalUnlockedEnemies = GetUnlockedEnemies().Length;
        int numberOfStructures = Utils
            .FindAllNearGameObjectsWithTag(transform, Tags.Structure, float.MaxValue)
            .Count();
        int totalPopulation = Manager.Population.GetPopulation();
        int availableWood = Manager.Resources.GetResourceValue(ResourceType.Wood);
        int availableMoney = Manager.Resources.GetResourceValue(ResourceType.Money);

        // Weights
        float weightRound = 0.2f;
        float weightEnemies = 0.2f;
        float weightStructures = 0.3f;
        float weightPopulation = 0.1f;
        float weightWood = 0.1f;
        float weightMoney = 0.1f;

        // Normalize
        float normalizedEnemies = Mathf.Clamp01(totalUnlockedEnemies / 4f);
        float normalizedWeightRound = Mathf.Clamp01(currentRound / 25f);
        float normalizedStructures = Mathf.Clamp01(numberOfStructures / 10f);
        float normalizedPopulation = Mathf.Clamp01(totalPopulation / 50f);
        float normalizedWood = Mathf.Clamp01(availableWood / 50f);
        float normalizedMoney = Mathf.Clamp01(availableMoney / 500f);

        // Calculate
        float delayMultiplier =
            1f
            / (
                (normalizedEnemies * weightEnemies)
                + (normalizedWeightRound * weightRound)
                + (normalizedStructures * weightStructures)
                + (normalizedPopulation * weightPopulation)
                + (normalizedWood * weightWood)
                + (normalizedMoney * weightMoney)
            );

        // Make sure it is never zero
        delayMultiplier = Mathf.Max(delayMultiplier, 0.1f); // Ajusta el valor mínimo según sea necesario

        return delayMultiplier;
    }

    private float GetQuantityMultiplier()
    {
        return 1 + (currentRound / 10); // Increase quantity multiplier by 0.1f
    }

    private Unit[] GetUnlockedEnemies()
    {
        // Get the list of prefabs that can appear in the current round
        return Enemies.GetAll().Where(p => p.appearAtRound <= currentRound).ToArray();
    }
}
