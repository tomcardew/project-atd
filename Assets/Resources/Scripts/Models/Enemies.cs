using System;
using UnityEngine;

[Serializable]
public struct Unit
{
    public string name;
    public GameObject prefab;
    public float weight;
    public int appearAtRound;

    public Unit(string name, GameObject prefab, float weight, int appearAtRound = 0)
    {
        this.name = name;
        this.prefab = prefab;
        this.weight = weight;
        this.appearAtRound = appearAtRound;
    }
}

public static class Enemies
{
    public static Unit Enemy { get; } = new Unit("Enemy", Prefabs.Enemy, 1.0f, 0);
    public static Unit LargeEnemy { get; } = new Unit("Large Enemy", Prefabs.LargeEnemy, 0.9f, 2);
    public static Unit Assasin { get; } = new Unit("Assasin", Prefabs.Assasin, 0.3f, 5);
    public static Unit Tank { get; } = new Unit("Tank", Prefabs.Tank, 0.5f, 8);

    public static Unit[] All { get; } = new Unit[] { Enemy, LargeEnemy, Assasin, Tank };
}
