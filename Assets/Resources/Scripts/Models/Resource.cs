using System;

[Serializable]
public enum ResourceType
{
    Money,
    Wood,
    Food,
    Faith
}

[Serializable]
public struct Resource
{
    public ResourceType Type { get; }
    public string Name { get; }
    public string Icon { get; } // icon path
    public string Sound { get; } // sound path

    public Resource(string name, ResourceType type, string icon, string sound)
    {
        Name = name;
        Type = type;
        Icon = icon;
        Sound = sound;
    }
}

public static class ResourceList
{
    public static Resource Money { get; } =
        new Resource("Money", ResourceType.Money, "Icon/coin", "coin");
    public static Resource Food { get; } =
        new Resource("Food", ResourceType.Food, "Icon/food", "food");
    public static Resource Wood { get; } =
        new Resource("Wood", ResourceType.Wood, "Icon/wood", "axe");
    public static Resource Faith { get; } =
        new Resource("Faith", ResourceType.Faith, "Icon/faith", "bell");

    public static Resource GetResourceByType(ResourceType type)
    {
        return type switch
        {
            ResourceType.Money => Money,
            ResourceType.Wood => Wood,
            ResourceType.Faith => Faith,
            ResourceType.Food => Food,
            _ => throw new ArgumentException("Invalid resource type", nameof(type))
        };
    }
}
