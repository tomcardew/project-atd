using UnityEngine;

public class ArcherSoldierTentController : PrefabSpawner
{
    public override void Init() { }

    public override void DidSpawnPrefab(GameObject gameobject)
    {
        ArcherMovable archerMovable = gameobject.GetComponent<ArcherMovable>();
        // TODO: Make soldier return to tent
    }

    public override GameObject GetPrefab()
    {
        return Prefabs.GetPrefab(Prefabs.UnitType.ArcherSoldier);
    }
}
