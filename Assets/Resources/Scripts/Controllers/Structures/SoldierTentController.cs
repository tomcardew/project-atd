using System;
using UnityEngine;

public class SoldierTentController : SoldierSpawner
{
    public override void DidSpawnSoldier(GameObject soldier)
    {
        SoldierMovable soldierMovable = soldier.GetComponent<SoldierMovable>();
        // TODO: Make soldier return to tent
    }

    public override GameObject GetPrefab()
    {
        return Prefabs.Soldier;
    }
}
