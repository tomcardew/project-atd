using UnityEngine;

public class AntitankSoldierTentController : SoldierSpawner
{
    public override void DidSpawnSoldier(GameObject soldier)
    {
        AntitankSoldierMovable soldierMovable = soldier.GetComponent<AntitankSoldierMovable>();
        // TODO: Make soldier return to tent
    }

    public override GameObject GetPrefab()
    {
        return Prefabs.AntitankSoldier;
    }
}
