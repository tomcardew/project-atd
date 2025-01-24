using UnityEngine;

public enum ObjectType
{
    Structure,
    Resource,
    Unit
}

public class BoxBuilder
{
    public static GameObject CreateDetailsBox(string name, GameObject obj, ObjectType type)
    {
        switch (type)
        {
            case ObjectType.Structure:
                return CreateStructureDetailsBox(name, obj);
            case ObjectType.Resource:
                return CreateResourceDetailsBox(name, obj);
            // case ObjectType.Unit:
            //     return CreateUnitDetailsBox(obj);
        }
        return obj;
    }

    private static GameObject CreateStructureDetailsBox(string name, GameObject obj)
    {
        GameObject box = CreateBox();
        BoxController boxController = box.GetComponent<BoxController>();

        AddTypeLabel(ObjectType.Structure, boxController);
        AddName(name, boxController);

        return box;
    }

    private static GameObject CreateResourceDetailsBox(string name, GameObject obj)
    {
        GameObject box = CreateBox();
        BoxController boxController = box.GetComponent<BoxController>();

        AddTypeLabel(ObjectType.Resource, boxController);
        AddName(name, boxController);
        AddSubtitle("Resource Generation", boxController);

        return box;
    }

    private static GameObject CreateBox()
    {
        GameObject boxPrefab = Prefabs.GetPrefab(Prefabs.Boxes.DetailsBox);
        GameObject box = Object.Instantiate(boxPrefab, Vector2.zero, Quaternion.identity);
        return box;
    }

    private static void AddTypeLabel(ObjectType type, BoxController boxController)
    {
        GameObject typeLabel = Object.Instantiate(Prefabs.GetPrefab(Prefabs.Boxes.TypeLabel));
        TypeLabelController typeLabelController = typeLabel.GetComponent<TypeLabelController>();
        if (type == ObjectType.Structure)
        {
            typeLabelController.cardType = CardType.Structure;
        }
        else if (type == ObjectType.Resource)
        {
            typeLabelController.cardType = CardType.WoodResource;
        }

        boxController.AddElement(typeLabel);
    }

    private static void AddName(string name, BoxController boxController)
    {
        GameObject nameObject = Object.Instantiate(Prefabs.GetPrefab(Prefabs.Boxes.NameContainer));
        NameContainer nameController = nameObject.GetComponent<NameContainer>();
        nameController.text = name;

        boxController.AddElement(nameObject);
    }

    private static void AddSubtitle(string text, BoxController boxController)
    {
        GameObject subtitle = Object.Instantiate(Prefabs.GetPrefab(Prefabs.Boxes.Subtitle));
        SubtitleController subtitleController = subtitle.GetComponent<SubtitleController>();
        subtitleController.text = text;

        boxController.AddElement(subtitle);
    }
}
