using UnityEngine;

public static class Prefabs
{
    public enum UnitType
    {
        Person,
        Soldier,
        AntitankSoldier
    }

    public enum EnemyType
    {
        Enemy,
        LargeEnemy,
        Tank,
        Assasin
    }

    public enum StructureType
    {
        Castle,
        SoldierTent,
        AntitankSoldierTent,
        House,
        ArcherTower,
        Church,
        SoldierTent_Droppable,
        AntitankSoldierTent_Droppable,
        House_Droppable,
        ArcherTower_Droppable,
        Church_Droppable
    }

    public enum ActionType
    {
        PlantASeed,
        RepairAnStructure,
        PlantASeed_Droppable,
        RepairAnStructure_Droppable
    }

    public enum ResourceType
    {
        LargeTree,
        MediumTree,
        SmallTree
    }

    public enum BulletType
    {
        Arrow,
        TankBullet,
        AntitankBullet
    }

    public enum OtherType
    {
        Spawner,
        LineDrawer,
        CardUI,
        CardBackCounters
    }

    public enum AlertType
    {
        WaveIsComing
    }

    public enum SoundType
    {
        Alert
    }

    public static GameObject GetPrefab(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.Person:
                return Resources.Load<GameObject>("Prefabs/Units/Person");
            case UnitType.Soldier:
                return Resources.Load<GameObject>("Prefabs/Units/Soldier");
            case UnitType.AntitankSoldier:
                return Resources.Load<GameObject>("Prefabs/Units/AntitankSoldier");
            default:
                return null;
        }
    }

    public static GameObject GetPrefab(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Enemy:
                return Resources.Load<GameObject>("Prefabs/Units/Enemies/Enemy");
            case EnemyType.LargeEnemy:
                return Resources.Load<GameObject>("Prefabs/Units/Enemies/LargeEnemy");
            case EnemyType.Tank:
                return Resources.Load<GameObject>("Prefabs/Units/Enemies/Tank");
            case EnemyType.Assasin:
                return Resources.Load<GameObject>("Prefabs/Units/Enemies/Assasin");
            default:
                return null;
        }
    }

    public static GameObject GetPrefab(StructureType structureType)
    {
        switch (structureType)
        {
            case StructureType.Castle:
                return Resources.Load<GameObject>("Prefabs/Structures/Castle");
            case StructureType.SoldierTent:
                return Resources.Load<GameObject>("Prefabs/Structures/SoldierTent");
            case StructureType.AntitankSoldierTent:
                return Resources.Load<GameObject>("Prefabs/Structures/AntitankSoldierTent");
            case StructureType.House:
                return Resources.Load<GameObject>("Prefabs/Structures/House");
            case StructureType.ArcherTower:
                return Resources.Load<GameObject>("Prefabs/Structures/ArcherTower");
            case StructureType.Church:
                return Resources.Load<GameObject>("Prefabs/Structures/Church");
            case StructureType.SoldierTent_Droppable:
                return Resources.Load<GameObject>("Prefabs/Structures/Droppables/SoldierTent");
            case StructureType.AntitankSoldierTent_Droppable:
                return Resources.Load<GameObject>(
                    "Prefabs/Structures/Droppables/AntitankSoldierTent"
                );
            case StructureType.House_Droppable:
                return Resources.Load<GameObject>("Prefabs/Structures/Droppables/House");
            case StructureType.ArcherTower_Droppable:
                return Resources.Load<GameObject>("Prefabs/Structures/Droppables/ArcherTower");
            case StructureType.Church_Droppable:
                return Resources.Load<GameObject>("Prefabs/Structures/Droppables/Church");
            default:
                return null;
        }
    }

    public static GameObject GetPrefab(ActionType actionType)
    {
        switch (actionType)
        {
            case ActionType.PlantASeed:
                return Resources.Load<GameObject>("Prefabs/Actions/PlantASeed");
            case ActionType.RepairAnStructure:
                return Resources.Load<GameObject>("Prefabs/Actions/RepairAnStructure");
            case ActionType.PlantASeed_Droppable:
                return Resources.Load<GameObject>("Prefabs/Actions/Droppables/PlantASeed");
            case ActionType.RepairAnStructure_Droppable:
                return Resources.Load<GameObject>("Prefabs/Actions/Droppables/RepairAnStructure");
            default:
                return null;
        }
    }

    public static GameObject GetPrefab(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.LargeTree:
                return Resources.Load<GameObject>("Prefabs/Resources/LargeTree");
            case ResourceType.MediumTree:
                return Resources.Load<GameObject>("Prefabs/Resources/MediumTree");
            case ResourceType.SmallTree:
                return Resources.Load<GameObject>("Prefabs/Resources/SmallTree");
            default:
                return null;
        }
    }

    public static GameObject GetPrefab(BulletType bulletType)
    {
        switch (bulletType)
        {
            case BulletType.Arrow:
                return Resources.Load<GameObject>("Prefabs/Bullets/Arrow");
            case BulletType.TankBullet:
                return Resources.Load<GameObject>("Prefabs/Bullets/TankBullet");
            case BulletType.AntitankBullet:
                return Resources.Load<GameObject>("Prefabs/Bullets/AntitankBullet");
            default:
                return null;
        }
    }

    public static GameObject GetPrefab(OtherType otherType)
    {
        switch (otherType)
        {
            case OtherType.Spawner:
                return Resources.Load<GameObject>("Prefabs/Utils/Spawner");
            case OtherType.LineDrawer:
                return Resources.Load<GameObject>("Prefabs/Utils/LineDrawer");
            case OtherType.CardUI:
                return Resources.Load<GameObject>("Prefabs/UI/Card");
            case OtherType.CardBackCounters:
                return Resources.Load<GameObject>("Prefabs/UI/CardBackCounters");
            default:
                return null;
        }
    }

    public static GameObject GetPrefab(AlertType alertType)
    {
        switch (alertType)
        {
            case AlertType.WaveIsComing:
                return Resources.Load<GameObject>("Prefabs/UI/Alerts/WaveIsComing");
            default:
                return null;
        }
    }

    public static AudioClip GetSound(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.Alert:
                return Resources.Load<AudioClip>("Sounds/alert2");
            default:
                return null;
        }
    }
}
