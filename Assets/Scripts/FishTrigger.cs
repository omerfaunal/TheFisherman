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
            isPlayerNear = false;
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F)) // Check if F is pressed when player is near
        {
            FishingSystem.instance.StartFishing(waterSource);
        }
    }

}
