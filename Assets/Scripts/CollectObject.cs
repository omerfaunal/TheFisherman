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

    private bool isPlayerNear = false; // To check if player is near the collectible
    private Color defaultColor = new Color(1f, 0.882f, 0.420f, 1f);

    void Start() {
        collectText.gameObject.SetActive(false); // Ensure the text is hidden initially
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
            if (playerInventory != null)
            {
                if (inventoryLimit != -1 && playerInventory.GetItemQuantity(itemName) >= inventoryLimit)
                {
                    UIManager.instance.DisplayErrorMessage("I can't carry more " + itemName + "s.");
                }
                else
                {
                    playerInventory.AddItem(itemName, quantity);
                    playerInventory.DisplayInventory();
                    Destroy(gameObject); // Collect the item and destroy it
                }
                collectText.gameObject.SetActive(false); // Hide the collect text
            }
        }
    }


}
