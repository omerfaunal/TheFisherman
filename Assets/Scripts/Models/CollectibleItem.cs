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

    public CollectibleItem(string name, int count)
    {
        itemName = name;
        quantity = count;
    }
}
