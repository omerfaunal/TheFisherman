using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTrigger : MonoBehaviour
{
    public WaterSource waterSource;

    private bool isPlayerNear = false;
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.CompareTag("Player")) // Check if the collider is the player
        {
            //FishingSystem.instance.StartFishing(waterSource);
            UIManager.instance.DisplayInfoMessage("Press F to start fishing");
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.HideInfoMessage(); // Hide the collect text
            FishingSystem.instance.EndFishing();
            UIManager.instance.DisplayErrorMessage("You left the fishing area");
            isPlayerNear = false;
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F)) // Check if F is pressed when player is near
        {
            InventoryManagement playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManagement>();
            if(!playerInventory.HasItem("fixed rod")){
                UIManager.instance.DisplayErrorMessage("You need a fixed fishing rod to fish");
                return;
            } 
            if (!playerInventory.HasItem("bucket")){
                UIManager.instance.DisplayErrorMessage("You need a bucket to fish");
                return;
            }
            FishingSystem.instance.StartFishing(waterSource);
        }
    }

}
