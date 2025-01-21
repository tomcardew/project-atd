using System;
using System.Linq;
using Unity.VisualScripting;
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

public enum CardForegroundType
{
    Normal,
    Structure,
    Action,
    WoodResource,
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
    public CardForegroundType foregroundType;

    public Card(
        string name,
        string description,
        string iconPath,
        CardResourceItem[] resources,
        GameObject prefab,
        GameObject droppablePrefab,
        int appearsAtRound = 0,
        CardForegroundType foregroundType = CardForegroundType.Normal
    )
    {
        this.name = name;
        this.description = description;
        this.iconPath = iconPath;
        this.resources = resources;
        this.prefab = prefab;
        this.droppablePrefab = droppablePrefab;
        this.appearsAtRound = appearsAtRound;
        this.foregroundType = foregroundType;
    }
}

public static class Cards
{
    public static Card House { get; } =
        new Card(
            "House",
            "Add +1 population",
            "Structures/House",
            new CardResourceItem[] { new(ResourceType.Wood, 10) },
            Prefabs.GetPrefab(Prefabs.StructureType.House),
            Prefabs.GetPrefab(Prefabs.StructureType.House_Droppable),
            0,
            CardForegroundType.Structure
        );

    public static Card SoldierTent { get; } =
        new Card(
            "Soldier",
            "Add +1 soldier",
            "Structures/Soldier Tent",
            new CardResourceItem[] { new(ResourceType.Money, 20), new(ResourceType.Wood, 10) },
            Prefabs.GetPrefab(Prefabs.StructureType.SoldierTent),
            Prefabs.GetPrefab(Prefabs.StructureType.SoldierTent_Droppable),
            0,
            CardForegroundType.Structure
        );

    public static Card SmallTrees { get; } =
        new Card(
            "Small Trees",
            "Plant a small group of trees",
            "Resources/Small Trees",
            new CardResourceItem[] { new(ResourceType.Money, 20) },
            Prefabs.GetPrefab(Prefabs.ActionType.SmallTrees),
            Prefabs.GetPrefab(Prefabs.ActionType.SmallTrees_Droppable),
            0,
            CardForegroundType.WoodResource
        );

    public static Card DrawACard { get; } =
        new Card(
            "Draw",
            "Draw 1",
            "HandActions/draw-a-card",
            new CardResourceItem[] { },
            Prefabs.GetPrefab(Prefabs.HandActionType.DrawACard),
            Prefabs.GetPrefab(Prefabs.HandActionType.DrawACard_Droppable),
            1
        );

    public static Card DiscardAndDraw { get; } =
        new Card(
            "Replace",
            "Discard 1, then draw 1",
            "HandActions/discard-and-draw",
            new CardResourceItem[] { new(ResourceType.Money, 25) },
            Prefabs.GetPrefab(Prefabs.HandActionType.DiscardAndDraw),
            Prefabs.GetPrefab(Prefabs.HandActionType.DiscardAndDraw_Droppable),
            1
        );

    public static Card ArcherTower { get; } =
        new Card(
            "Archer Tower",
            "Throw arrows from the distance",
            "Structures/ArcherTower",
            new CardResourceItem[] { new(ResourceType.Money, 40), new(ResourceType.Wood, 30) },
            Prefabs.GetPrefab(Prefabs.StructureType.ArcherTower),
            Prefabs.GetPrefab(Prefabs.StructureType.ArcherTower_Droppable),
            2,
            CardForegroundType.Structure
        );

    public static Card AddTree { get; } =
        new Card(
            "Give Me Trees",
            "Add a random Tree to your hand",
            "HandActions/add-tree",
            new CardResourceItem[] { new(ResourceType.Money, 15) },
            Prefabs.GetPrefab(Prefabs.HandActionType.AddTree),
            Prefabs.GetPrefab(Prefabs.HandActionType.AddTree_Droppable),
            2,
            CardForegroundType.WoodResource
        );

    public static Card AddHouse { get; } =
        new Card(
            "Give Me Houses",
            "Add a House to your hand",
            "HandActions/add-house",
            new CardResourceItem[] { new(ResourceType.Money, 10) },
            Prefabs.GetPrefab(Prefabs.HandActionType.AddHouse),
            Prefabs.GetPrefab(Prefabs.HandActionType.AddHouse_Droppable),
            2
        );

    public static Card AddSoldier { get; } =
        new Card(
            "Open Recruitment",
            "Add a Soldier Tent to your hand",
            "HandActions/add-soldier",
            new CardResourceItem[] { new(ResourceType.Money, 10) },
            Prefabs.GetPrefab(Prefabs.HandActionType.AddSoldier),
            Prefabs.GetPrefab(Prefabs.HandActionType.AddSoldier_Droppable),
            2
        );

    public static Card MediumTrees { get; } =
        new Card(
            "Medium Trees",
            "Plant a medium group of trees",
            "Resources/Medium Trees",
            new CardResourceItem[] { new(ResourceType.Money, 40) },
            Prefabs.GetPrefab(Prefabs.ActionType.MediumTrees),
            Prefabs.GetPrefab(Prefabs.ActionType.MediumTrees_Droppable),
            3,
            CardForegroundType.WoodResource
        );

    public static Card DiscardAllAndDraw5 { get; } =
        new Card(
            "Renewal",
            "Discard all and draw 5",
            "HandActions/discard-all-and-draw-5",
            new CardResourceItem[] { new(ResourceType.Money, 50) },
            Prefabs.GetPrefab(Prefabs.HandActionType.DiscardAllAndDraw5),
            Prefabs.GetPrefab(Prefabs.HandActionType.DiscardAllAndDraw5_Droppable),
            4,
            CardForegroundType.Normal
        );

    public static Card SniperTower { get; } =
        new Card(
            "Sniper Tower",
            "Instantly kill enemies from afar",
            "Structures/Sniper Tower",
            new CardResourceItem[] { new(ResourceType.Money, 50), new(ResourceType.Wood, 50) },
            Prefabs.GetPrefab(Prefabs.StructureType.SniperTower),
            Prefabs.GetPrefab(Prefabs.StructureType.SniperTower_Droppable),
            4,
            CardForegroundType.Structure
        );

    public static Card AntitankSoldierTent { get; } =
        new Card(
            "Antitank Soldier",
            "Add +1 soldier with an antitank weapon",
            "Structures/AntitankSoldier Tent",
            new CardResourceItem[] { new(ResourceType.Money, 100), new(ResourceType.Wood, 50) },
            Prefabs.GetPrefab(Prefabs.StructureType.AntitankSoldierTent),
            Prefabs.GetPrefab(Prefabs.StructureType.AntitankSoldierTent_Droppable),
            5,
            CardForegroundType.Structure
        );

    public static Card LargeTrees { get; } =
        new Card(
            "Large Trees",
            "Plant a large group of trees",
            "Resources/Large Trees",
            new CardResourceItem[] { new(ResourceType.Money, 60) },
            Prefabs.GetPrefab(Prefabs.ActionType.LargeTrees),
            Prefabs.GetPrefab(Prefabs.ActionType.LargeTrees_Droppable),
            6,
            CardForegroundType.WoodResource
        );

    public static Card BomberTower { get; } =
        new Card(
            "Bomber Tower",
            "Throw bombs at friends and enemyes",
            "Structures/BomberTower",
            new CardResourceItem[] { new(ResourceType.Money, 90), new(ResourceType.Wood, 75) },
            Prefabs.GetPrefab(Prefabs.StructureType.BomberTower),
            Prefabs.GetPrefab(Prefabs.StructureType.BomberTower_Droppable),
            7,
            CardForegroundType.Structure
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
            new CardResourceItem[] { new(ResourceType.Money, 100) },
            Prefabs.GetPrefab(Prefabs.ActionType.RepairAnStructure),
            Prefabs.GetPrefab(Prefabs.ActionType.RepairAnStructure_Droppable),
            9,
            CardForegroundType.Action
        );

    public static Card PlantASeed { get; } =
        new Card(
            "Plant A Seed",
            "Create a random group of trees",
            "Actions/small-tree",
            new CardResourceItem[] { new(ResourceType.Money, 40) },
            Prefabs.GetPrefab(Prefabs.ActionType.PlantASeed),
            Prefabs.GetPrefab(Prefabs.ActionType.PlantASeed_Droppable),
            10,
            CardForegroundType.Action
        );

    public static Card[] AllCards { get; } =
        new Card[]
        {
            House,
            ArcherTower,
            BomberTower,
            SniperTower,
            SoldierTent,
            // Church,
            PlantASeed,
            SmallTrees,
            MediumTrees,
            LargeTrees,
            RepairAnStructure,
            AntitankSoldierTent,
            DrawACard,
            DiscardAllAndDraw5,
            DiscardAndDraw,
            AddTree,
            AddHouse,
            AddSoldier,
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
