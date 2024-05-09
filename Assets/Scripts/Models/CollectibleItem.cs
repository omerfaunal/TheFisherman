using System;

[System.Serializable]

// Create enum

public enum CollectibleItemType
{
    Fish,
    Crafting,
}

public class CollectibleItem
{
    public string itemName;
    public int quantity;
    public CollectibleItemType type;

    public CollectibleItem(string name, CollectibleItemType itemType)
    {
        itemName = name;
        quantity = 1;
        type = itemType;
    }
}
