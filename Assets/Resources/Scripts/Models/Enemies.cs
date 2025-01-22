using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct Unit
{
    public string name;
    public GameObject prefab;
    public float weight;
    public int appearAtRound;

    public Unit(string name, float weight, int appearAtRound = 0)
    {
        this.name = name;
        this.weight = weight;
        this.appearAtRound = appearAtRound;
        this.prefab = null;
    }
}

public static class Enemies
{
    public static Unit Enemy { get; } = new Unit("Enemy", 1.0f, 0);
    public static Unit LargeEnemy { get; } = new Unit("LargeEnemy", 0.9f, 2);
    public static Unit Assasin { get; } = new Unit("Assasin", 0.5f, 4);
    public static Unit Tank { get; } = new Unit("Tank", 0.2f, 10);

    public static Unit GetUnit(Prefabs.EnemyType enemyType)
    {
        Unit unit;
        switch (enemyType)
        {
            case Prefabs.EnemyType.Enemy:
                unit = Enemy;
                break;
            case Prefabs.EnemyType.LargeEnemy:
                unit = LargeEnemy;
                break;
            case Prefabs.EnemyType.Assasin:
                unit = Assasin;
                break;
            case Prefabs.EnemyType.Tank:
                unit = Tank;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        unit.prefab = Prefabs.GetPrefab(enemyType);
        return unit;
    }

    public static Unit[] GetAll()
    {
        var units = new List<Unit>();
        var list = new List<Prefabs.EnemyType>
        {
            Prefabs.EnemyType.Enemy,
            Prefabs.EnemyType.LargeEnemy,
            Prefabs.EnemyType.Assasin,
            Prefabs.EnemyType.Tank
        };
        foreach (var item in list)
        {
            units.Add(GetUnit(item));
        }
        return units.ToArray();
    }
}
