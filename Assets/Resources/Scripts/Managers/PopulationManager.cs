using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    public int GetPopulation()
    {
        int population = 0;
        Populates[] populations = FindObjectsByType<Populates>(FindObjectsSortMode.None);
        foreach (var item in populations)
        {
            population += item.population;
        }
        return population;
    }
}
