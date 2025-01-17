using UnityEngine;

public class AntitankSoldierTentController : PrefabSpawner
{
    public override void Init() { }

    public override void DidSpawnPrefab(GameObject gameobject)
    {
        AntitankSoldierMovable soldierMovable = gameobject.GetComponent<AntitankSoldierMovable>();
        // TODO: Make soldier return to tent
    }

    public override GameObject GetPrefab()
    {
        return Prefabs.GetPrefab(Prefabs.UnitType.AntitankSoldier);
    }
}
