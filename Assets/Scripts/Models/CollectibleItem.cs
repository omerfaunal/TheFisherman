[System.Serializable]
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
