using System;
using UnityEngine;

public class CardResourceItem
{
    public ResourceType resource;
    public int value;

    public CardResourceItem(ResourceType resource, int value)
    {
        this.resource = resource;
        this.value = value;
    }
}

[Serializable]
public class Card
{
    // Public properties
    public string name;
    public string description;
    public string iconPath;
    public CardResourceItem[] resources;
    public GameObject prefab;
    public GameObject droppablePrefab;

    public Card(
        string name,
        string description,
        string iconPath,
        CardResourceItem[] resources,
        GameObject prefab,
        GameObject droppablePrefab
    )
    {
        this.name = name;
        this.description = description;
        this.iconPath = iconPath;
        this.resources = resources;
        this.prefab = prefab;
        this.droppablePrefab = droppablePrefab;
    }
}

public static class Cards
{
    public static Card House { get; } =
        new Card(
            "House",
            "Add +1 population",
            "Structures/House",
            new CardResourceItem[] { new(ResourceType.Wood, 3) },
            Prefabs.GetPrefab(Prefabs.StructureType.House),
            Prefabs.GetPrefab(Prefabs.StructureType.House_Droppable)
        );

    public static Card ArcherTower { get; } =
        new Card(
            "Archer Tower",
            "Throw arrows from the distance",
            "Structures/ArcherTower",
            new CardResourceItem[] { new(ResourceType.Money, 25), new(ResourceType.Wood, 15) },
            Prefabs.GetPrefab(Prefabs.StructureType.ArcherTower),
            Prefabs.GetPrefab(Prefabs.StructureType.ArcherTower_Droppable)
        );

    public static Card SoldierTent { get; } =
        new Card(
            "Soldier",
            "Add +1 soldier",
            "Structures/Soldier Tent",
            new CardResourceItem[] { new(ResourceType.Money, 10), new(ResourceType.Wood, 10) },
            Prefabs.GetPrefab(Prefabs.StructureType.SoldierTent),
            Prefabs.GetPrefab(Prefabs.StructureType.SoldierTent_Droppable)
        );

    // public static Card Church { get; } =
    //     new Card(
    //         "Church",
    //         "Generate faith",
    //         "Structures/Church",
    //         new CardResourceItem[] { new(ResourceType.Money, 30), new(ResourceType.Wood, 50) },
    //         Prefabs.Structures.Church,
    //         Prefabs.Structures.Church_Droppable
    //     );

    public static Card PlantASeed { get; } =
        new Card(
            "Plant A Seed",
            "Create some trees",
            "Actions/small-tree",
            new CardResourceItem[] { new(ResourceType.Money, 10) },
            Prefabs.GetPrefab(Prefabs.ActionType.PlantASeed),
            Prefabs.GetPrefab(Prefabs.ActionType.PlantASeed_Droppable)
        );

    public static Card RepairAnStructure { get; } =
        new Card(
            "Repair An Structure",
            "Restore an structure's health back to 100%",
            "Actions/repair",
            new CardResourceItem[] { new(ResourceType.Money, 30) },
            Prefabs.GetPrefab(Prefabs.ActionType.RepairAnStructure),
            Prefabs.GetPrefab(Prefabs.ActionType.RepairAnStructure_Droppable)
        );

    public static Card AntitankSoldierTent { get; } =
        new Card(
            "Antitank Soldier",
            "Add +1 soldier with an antitank weapon",
            "Structures/AntitankSoldier Tent",
            new CardResourceItem[] { new(ResourceType.Money, 30), new(ResourceType.Wood, 20) },
            Prefabs.GetPrefab(Prefabs.StructureType.AntitankSoldierTent),
            Prefabs.GetPrefab(Prefabs.StructureType.AntitankSoldierTent_Droppable)
        );

    public static Card[] AllCards { get; } =
        new Card[]
        {
            House,
            ArcherTower,
            SoldierTent,
            // Church,
            PlantASeed,
            RepairAnStructure,
            AntitankSoldierTent
        };

    public static Card[] StartDeck { get; } =
        new Card[]
        {
            House,
            House,
            House,
            House,
            ArcherTower,
            ArcherTower,
            ArcherTower,
            ArcherTower,
            SoldierTent,
            SoldierTent,
            SoldierTent,
            SoldierTent,
            AntitankSoldierTent,
            AntitankSoldierTent,
            AntitankSoldierTent,
            AntitankSoldierTent,
            PlantASeed,
            PlantASeed,
            PlantASeed,
            RepairAnStructure,
        };
}
