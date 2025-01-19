using System;
using System.Linq;
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
    public int appearsAtRound;

    public Card(
        string name,
        string description,
        string iconPath,
        CardResourceItem[] resources,
        GameObject prefab,
        GameObject droppablePrefab,
        int appearsAtRound = 0
    )
    {
        this.name = name;
        this.description = description;
        this.iconPath = iconPath;
        this.resources = resources;
        this.prefab = prefab;
        this.droppablePrefab = droppablePrefab;
        this.appearsAtRound = appearsAtRound;
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

    public static Card SoldierTent { get; } =
        new Card(
            "Soldier",
            "Add +1 soldier",
            "Structures/Soldier Tent",
            new CardResourceItem[] { new(ResourceType.Money, 10), new(ResourceType.Wood, 10) },
            Prefabs.GetPrefab(Prefabs.StructureType.SoldierTent),
            Prefabs.GetPrefab(Prefabs.StructureType.SoldierTent_Droppable)
        );

    public static Card SmallTrees { get; } =
        new Card(
            "Small Trees",
            "Plant a small group of trees",
            "Resources/Small Trees",
            new CardResourceItem[] { new(ResourceType.Money, 5) },
            Prefabs.GetPrefab(Prefabs.ActionType.SmallTrees),
            Prefabs.GetPrefab(Prefabs.ActionType.SmallTrees_Droppable)
        );

    public static Card ArcherTower { get; } =
        new Card(
            "Archer Tower",
            "Throw arrows from the distance",
            "Structures/ArcherTower",
            new CardResourceItem[] { new(ResourceType.Money, 25), new(ResourceType.Wood, 15) },
            Prefabs.GetPrefab(Prefabs.StructureType.ArcherTower),
            Prefabs.GetPrefab(Prefabs.StructureType.ArcherTower_Droppable),
            2
        );

    public static Card MediumTrees { get; } =
        new Card(
            "Medium Trees",
            "Plant a medium group of trees",
            "Resources/Medium Trees",
            new CardResourceItem[] { new(ResourceType.Money, 10) },
            Prefabs.GetPrefab(Prefabs.ActionType.MediumTrees),
            Prefabs.GetPrefab(Prefabs.ActionType.MediumTrees_Droppable),
            3
        );

    public static Card AntitankSoldierTent { get; } =
        new Card(
            "Antitank Soldier",
            "Add +1 soldier with an antitank weapon",
            "Structures/AntitankSoldier Tent",
            new CardResourceItem[] { new(ResourceType.Money, 30), new(ResourceType.Wood, 20) },
            Prefabs.GetPrefab(Prefabs.StructureType.AntitankSoldierTent),
            Prefabs.GetPrefab(Prefabs.StructureType.AntitankSoldierTent_Droppable),
            5
        );

    public static Card LargeTrees { get; } =
        new Card(
            "Large Trees",
            "Plant a large group of trees",
            "Resources/Large Trees",
            new CardResourceItem[] { new(ResourceType.Money, 15) },
            Prefabs.GetPrefab(Prefabs.ActionType.LargeTrees),
            Prefabs.GetPrefab(Prefabs.ActionType.LargeTrees_Droppable),
            6
        );

    public static Card BomberTower { get; } =
        new Card(
            "Bomber Tower",
            "Throw bombs and watch them explode",
            "Structures/BomberTower",
            new CardResourceItem[] { new(ResourceType.Money, 35), new(ResourceType.Wood, 20) },
            Prefabs.GetPrefab(Prefabs.StructureType.BomberTower),
            Prefabs.GetPrefab(Prefabs.StructureType.BomberTower_Droppable),
            7
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

    public static Card RepairAnStructure { get; } =
        new Card(
            "Repair An Structure",
            "Restore an structure's health back to 100%",
            "Actions/repair",
            new CardResourceItem[] { new(ResourceType.Money, 30) },
            Prefabs.GetPrefab(Prefabs.ActionType.RepairAnStructure),
            Prefabs.GetPrefab(Prefabs.ActionType.RepairAnStructure_Droppable),
            9
        );

    public static Card PlantASeed { get; } =
        new Card(
            "Plant A Seed",
            "Create a random group of trees",
            "Actions/small-tree",
            new CardResourceItem[] { new(ResourceType.Money, 10) },
            Prefabs.GetPrefab(Prefabs.ActionType.PlantASeed),
            Prefabs.GetPrefab(Prefabs.ActionType.PlantASeed_Droppable),
            10
        );

    public static Card[] AllCards { get; } =
        new Card[]
        {
            House,
            ArcherTower,
            BomberTower,
            SoldierTent,
            // Church,
            PlantASeed,
            SmallTrees,
            MediumTrees,
            LargeTrees,
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
            SoldierTent,
            SoldierTent,
            SoldierTent,
            SoldierTent,
            SmallTrees,
            SmallTrees,
        };

    public static Card[] GetAvailableCardAtRound(int round)
    {
        return AllCards.Where(c => c.appearsAtRound <= round).ToArray();
    }
}
