using System.Collections.Generic;
using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    public List<CollectibleItem> items = new List<CollectibleItem>();

    public void AddItem(string itemName,CollectibleItemType type)
    {
        // Check if the item already exists in the inventory
        CollectibleItem existingItem = items.Find(item => item.itemName == itemName);
        if (existingItem != null)
        {
            existingItem.quantity += 1; // Increase quantity if item exists
        }
        else
        {
            items.Add(new CollectibleItem(itemName, type)); // Add new item if not exists
        }
    }

    // Optional: Method to remove item from inventory
    public void RemoveItem(string itemName)
    {
        CollectibleItem existingItem = items.Find(item => item.itemName == itemName);
        if (existingItem != null && existingItem.quantity >= 1)
        {
            existingItem.quantity -= 1;
            if (existingItem.quantity == 0)
            {
                items.Remove(existingItem); // Remove the item if quantity is zero
            }
        }
        else
        {
            Debug.Log("Not enough items to remove or item not found!");
        }
    }

    public int GetItemQuantity(string itemName)
    {
        CollectibleItem existingItem = items.Find(item => item.itemName == itemName);
        return existingItem != null ? existingItem.quantity : 0;
    }

    // Method to display inventory contents for debugging
    public void DisplayInventory()
    {
        foreach (CollectibleItem item in items)
        {
           Debug.Log(item.itemName + ": " + item.quantity);
        }
    }

  
}
