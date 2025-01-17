using System;
using UnityEngine;

public class SoldierTentController : PrefabSpawner
{
    public override void Init() { }

    public override void DidSpawnPrefab(GameObject prefab)
    {
        SoldierMovable soldierMovable = prefab.GetComponent<SoldierMovable>();
        // TODO: Make soldier return to tent
    }

    public override GameObject GetPrefab()
    {
        return Prefabs.Soldier;
    }
}
