using UnityEngine;

public static class Prefabs
{
    // Units
    public static GameObject Person { get; } = Resources.Load<GameObject>("Prefabs/Units/Person");
    public static GameObject Soldier { get; } = Resources.Load<GameObject>("Prefabs/Units/Soldier");
    public static GameObject Enemy { get; } =
        Resources.Load<GameObject>("Prefabs/Units/Enemies/Enemy");
    public static GameObject LargeEnemy { get; } =
        Resources.Load<GameObject>("Prefabs/Units/Enemies/LargeEnemy");
    public static GameObject Tank { get; } =
        Resources.Load<GameObject>("Prefabs/Units/Enemies/Tank");

    // Structures
    public static GameObject Castle { get; } =
        Resources.Load<GameObject>("Prefabs/Structures/Castle");
    public static GameObject SoldierPost { get; } =
        Resources.Load<GameObject>("Prefabs/Structures/SoldierPost");
    public static GameObject House { get; } =
        Resources.Load<GameObject>("Prefabs/Structures/House");
    public static GameObject ArcherTower { get; } =
        Resources.Load<GameObject>("Prefabs/Structures/ArcherTower");
    public static GameObject Church { get; } =
        Resources.Load<GameObject>("Prefabs/Structures/Church");

    // Structure droppables
    public static GameObject SoldierPost_Droppable { get; } =
        Resources.Load<GameObject>("Prefabs/Structures/Droppables/SoldierPost");
    public static GameObject House_Droppable { get; } =
        Resources.Load<GameObject>("Prefabs/Structures/Droppables/House");
    public static GameObject ArcherTower_Droppable { get; } =
        Resources.Load<GameObject>("Prefabs/Structures/Droppables/ArcherTower");
    public static GameObject Church_Droppable { get; } =
        Resources.Load<GameObject>("Prefabs/Structures/Droppables/Church");

    // Actions
    public static GameObject PlantASeed { get; } =
        Resources.Load<GameObject>("Prefabs/Actions/PlantASeed");
    public static GameObject RepairAnStructure { get; } =
        Resources.Load<GameObject>("Prefabs/Actions/RepairAnStructure");

    // Action droppables
    public static GameObject PlantASeed_Droppable { get; } =
        Resources.Load<GameObject>("Prefabs/Actions/Droppables/PlantASeed");
    public static GameObject RepairAnStructure_Droppable { get; } =
        Resources.Load<GameObject>("Prefabs/Actions/Droppables/RepairAnStructure");

    // Resources
    public static GameObject LargeTree { get; } =
        Resources.Load<GameObject>("Prefabs/Resources/LargeTree");
    public static GameObject MediumTree { get; } =
        Resources.Load<GameObject>("Prefabs/Resources/MediumTree");
    public static GameObject SmallTree { get; } =
        Resources.Load<GameObject>("Prefabs/Resources/SmallTree");

    // Bullets
    public static GameObject Arrow { get; } = Resources.Load<GameObject>("Prefabs/Bullets/Arrow");
    public static GameObject TankBullet { get; } =
        Resources.Load<GameObject>("Prefabs/Bullets/TankBullet");

    // Other
    public static GameObject Spawner { get; } = Resources.Load<GameObject>("Prefabs/Utils/Spawner");
    public static GameObject LineDrawer { get; } =
        Resources.Load<GameObject>("Prefabs/Utils/LineDrawer");
    public static GameObject CardUI { get; } = Resources.Load<GameObject>("Prefabs/UI/Card");
}
