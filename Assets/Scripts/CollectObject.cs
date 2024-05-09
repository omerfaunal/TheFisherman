using UnityEngine;
using TMPro;
using System.Collections;

public class CollectObject : MonoBehaviour
{
    public string itemName;
    public int quantity = 1;
    public int inventoryLimit = -1;
    public TextMeshProUGUI collectText;
    public TextMeshProUGUI errorText;
    private TaskManager taskManager;

    private bool isPlayerNear = false; // To check if player is near the collectible
    private Color defaultColor = new Color(1f, 0.882f, 0.420f, 1f);

    void Start() {
        collectText.gameObject.SetActive(false); // Ensure the text is hidden initially
        taskManager = GameObject.Find("GameManager").GetComponent<TaskManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider is the player
        {
            UIManager.instance.DisplayInfoMessage("Press F to collect " + itemName); // Show the collect text
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.HideInfoMessage(); // Hide the collect text
            isPlayerNear = false;
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F)) // Check if F is pressed when player is near
        {
            InventoryManagement playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManagement>();
            var itemQuantity = playerInventory.GetItemQuantity(itemName);
            if (inventoryLimit != -1 && itemQuantity >= inventoryLimit)
            {
                UIManager.instance.DisplayErrorMessage("I can't carry more " + itemName + "s.");
            }
            else
            {
                playerInventory.AddItem(itemName, CollectibleItemType.Crafting);
                taskManager.CheckTaskState();
                switch (itemName)
                {
                    case "fishing rod":
                        if (playerInventory.GetItemQuantity("log") == 3)
                        {
                            UIManager.instance.DisplaySuccessMessage("Nice! Now you need a bucket to put the fish you caught. Go to deadwood forest following pink trees");
                        }
                        else
                        {
                            UIManager.instance.DisplaySuccessMessage("Great! You found a fishing rod but it's broken. You need to collect 3 logs to fix it.");
                        }
                        
                        break;
                    case "log":
                        if (itemQuantity != 2)
                        {
                            UIManager.instance.DisplaySuccessMessage("You need " + (2 - itemQuantity) + " logs more");
                        }
                        else
                        {
                            UIManager.instance.DisplaySuccessMessage("Nice! You fixed your fishing rod. Now you need a bucket to put the fish you caught. Go to deadwood forest following pink trees");
                        }
                        break;
                    case "bucket":    
                        UIManager.instance.DisplaySuccessMessage("Good news: You are ready to catch delicious fishes!");
                        break;
                }
                playerInventory.DisplayInventory();
                Destroy(gameObject); // Collect the item and destroy it
            }
            collectText.gameObject.SetActive(false); // Hide the collect text
            
        }
    }


}
