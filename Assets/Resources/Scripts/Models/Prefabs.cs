using UnityEngine;
using UnityEngine.XR;

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
        BomberTower,
        SniperTower,
        Church,
        SoldierTent_Droppable,
        AntitankSoldierTent_Droppable,
        House_Droppable,
        ArcherTower_Droppable,
        BomberTower_Droppable,
        SniperTower_Droppable,
        Church_Droppable
    }

    public enum ActionType
    {
        PlantASeed,
        PlantASeed_Droppable,
        SmallTrees,
        SmallTrees_Droppable,
        MediumTrees,
        MediumTrees_Droppable,
        LargeTrees,
        LargeTrees_Droppable,
        RepairAnStructure,
        RepairAnStructure_Droppable
    }

    public enum HandActionType
    {
        DrawACard,
        DrawACard_Droppable,
        DiscardAllAndDraw5,
        DiscardAllAndDraw5_Droppable,
        DiscardAndDraw,
        DiscardAndDraw_Droppable,
        AddTree,
        AddTree_Droppable,
        AddHouse,
        AddHouse_Droppable,
        AddSoldier,
        AddSoldier_Droppable
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
        AntitankBullet,
        Bomb
    }

    public enum OtherType
    {
        Spawner,
        LineDrawer,
        CardUI,
        CardBackCounters,
    }

    public enum AlertType
    {
        WaveIsComing
    }

    public enum Modals
    {
        FirstDraw,
        AddCard
    }

    public enum SoundType
    {
        Alert,
        CardFlip,
        CardDiscarded
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
            case StructureType.BomberTower:
                return Resources.Load<GameObject>("Prefabs/Structures/BomberTower");
            case StructureType.SniperTower:
                return Resources.Load<GameObject>("Prefabs/Structures/SniperTower");
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
            case StructureType.BomberTower_Droppable:
                return Resources.Load<GameObject>("Prefabs/Structures/Droppables/BomberTower");
            case StructureType.SniperTower_Droppable:
                return Resources.Load<GameObject>("Prefabs/Structures/Droppables/SniperTower");
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
            case ActionType.PlantASeed_Droppable:
                return Resources.Load<GameObject>("Prefabs/Actions/Droppables/PlantASeed");
            case ActionType.RepairAnStructure:
                return Resources.Load<GameObject>("Prefabs/Actions/RepairAnStructure");
            case ActionType.RepairAnStructure_Droppable:
                return Resources.Load<GameObject>("Prefabs/Actions/Droppables/RepairAnStructure");
            case ActionType.SmallTrees:
                return Resources.Load<GameObject>("Prefabs/Actions/SmallTrees");
            case ActionType.SmallTrees_Droppable:
                return Resources.Load<GameObject>("Prefabs/Actions/Droppables/SmallTrees");
            case ActionType.MediumTrees:
                return Resources.Load<GameObject>("Prefabs/Actions/MediumTrees");
            case ActionType.MediumTrees_Droppable:
                return Resources.Load<GameObject>("Prefabs/Actions/Droppables/MediumTrees");
            case ActionType.LargeTrees:
                return Resources.Load<GameObject>("Prefabs/Actions/LargeTrees");
            case ActionType.LargeTrees_Droppable:
                return Resources.Load<GameObject>("Prefabs/Actions/Droppables/LargeTrees");
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
            case BulletType.Bomb:
                return Resources.Load<GameObject>("Prefabs/Bullets/Bomb");
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

    public static GameObject GetPrefab(HandActionType handActionType)
    {
        switch (handActionType)
        {
            case HandActionType.DrawACard:
                return Resources.Load<GameObject>("Prefabs/HandActions/DrawACard");
            case HandActionType.DrawACard_Droppable:
                return Resources.Load<GameObject>("Prefabs/HandActions/Droppables/DrawACard");
            case HandActionType.DiscardAllAndDraw5:
                return Resources.Load<GameObject>("Prefabs/HandActions/DiscardAllAndDraw5");
            case HandActionType.DiscardAllAndDraw5_Droppable:
                return Resources.Load<GameObject>(
                    "Prefabs/HandActions/Droppables/DiscardAllAndDraw5"
                );
            case HandActionType.DiscardAndDraw:
                return Resources.Load<GameObject>("Prefabs/HandActions/DiscardAndDraw");
            case HandActionType.DiscardAndDraw_Droppable:
                return Resources.Load<GameObject>("Prefabs/HandActions/Droppables/DiscardAndDraw");
            case HandActionType.AddTree:
                return Resources.Load<GameObject>("Prefabs/HandActions/AddTree");
            case HandActionType.AddTree_Droppable:
                return Resources.Load<GameObject>("Prefabs/HandActions/Droppables/AddTree");
            case HandActionType.AddHouse:
                return Resources.Load<GameObject>("Prefabs/HandActions/AddHouse");
            case HandActionType.AddHouse_Droppable:
                return Resources.Load<GameObject>("Prefabs/HandActions/Droppables/AddHouse");
            case HandActionType.AddSoldier:
                return Resources.Load<GameObject>("Prefabs/HandActions/AddSoldier");
            case HandActionType.AddSoldier_Droppable:
                return Resources.Load<GameObject>("Prefabs/HandActions/Droppables/AddSoldier");
            default:
                return null;
        }
    }

    public static GameObject GetPrefab(Modals modal)
    {
        switch (modal)
        {
            case Modals.FirstDraw:
                return Resources.Load<GameObject>("Prefabs/UI/Modals/FirstDrawModal");
            case Modals.AddCard:
                return Resources.Load<GameObject>("Prefabs/UI/Modals/AddCardModal");
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
            case SoundType.CardFlip:
                return Resources.Load<AudioClip>("Sounds/cardflip");
            case SoundType.CardDiscarded:
                return Resources.Load<AudioClip>("Sounds/discarded");
            default:
                return null;
        }
    }
}
