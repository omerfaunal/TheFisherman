using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaterSource {Lake, River, Sea}

public class FishingSystem : MonoBehaviour
{
    public static FishingSystem instance{get;  set;}
    public GameObject miniGame;
    public GameObject pullingGame;
    public PullingGameTarget pullingGameTarget;
    public List<FishData> lakeFishList = new List<FishData>();
    public List<FishData> riverFishList = new List<FishData>();
    public List<FishData> seaFishList = new List<FishData>();
    public TaskManager taskManager;

    public bool isThereABite = false;
    public bool hasPulled = false;

    private void Awake() {
        if(instance != null && instance != this){
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    internal void StartFishing(WaterSource waterSource)
    {
        UIManager.instance.HideInfoMessage();
        CameraSwitch cameraSwitch = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraSwitch>();
        cameraSwitch.SetCameraType(CameraType.FirstPerson, true);
        StartCoroutine(CatchFish(waterSource));
    }

    IEnumerator CatchFish(WaterSource waterSource){
        if(!isThereABite){
            Debug.Log("Catching fish");
            yield return new WaitForSeconds(0.2f);
            FishData fishData = ChooseRandomFish(waterSource);
            if (fishData.fishName == "NoBite"){
                UIManager.instance.DisplayErrorMessage("No fish bite");
                EndFishing();
            } else {
                isThereABite = true;
                Debug.LogWarning("Fish bite " + fishData.fishName);
                StartCoroutine(StartFishChallenge(fishData));
            }
        }
        
    }

    IEnumerator StartFishChallenge(FishData fishData)
    {

        PushRod();
        while (!hasPulled){
            // if(!displayingMessage){
            //     UIManager.instance.DisplayInfoMessage("Press G to pull the fish");
            //     displayingMessage = true;
            // }
            // if(Input.GetKeyDown(KeyCode.G)){
            //     hasPulled = true;
            //     UIManager.instance.HideInfoMessage();
            // }
            if(!isThereABite){
                yield break;
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Starting fish challenge");
        StartMiniGame(fishData);
        Debug.Log("Caught fish: " + fishData.fishName);
    }

    private void PushRod()
    {
        // GetNewTarget method is called from PullingGameTarget script
        PullingGameTarget pgt = pullingGameTarget.GetComponent<PullingGameTarget>();
        pgt.GetNewTarget();
        pullingGame.SetActive(true);
    }

    public void EndPushing(bool success)
    {
        if(success){
            hasPulled = true;
            pullingGame.SetActive(false);
        } else {
            UIManager.instance.DisplayErrorMessage("You failed to push the road");
            pullingGame.SetActive(false);
            EndFishing();
        }
    }

    public void EndFishing() {
        isThereABite = false;
        hasPulled = false;
        pullingGame.SetActive(false);
        miniGame.SetActive(false);
        CameraSwitch cameraSwitch = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraSwitch>();
        cameraSwitch.ResetToDefaultCamera();
        Debug.Log("Ending fishing");
    }

    private void StartMiniGame(FishData fishData)
    {
        FishingGame fishingGame = miniGame.GetComponent<FishingGame>();
        fishingGame.SetFish(fishData);
        miniGame.SetActive(true);
    }

    public void EndMiniGame(bool success, FishData currentFish)
    {
        miniGame.SetActive(false);
        EndFishing();

        if(success){
            InventoryManagement playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManagement>();

            playerInventory.AddItem(currentFish.fishName, CollectibleItemType.Fish);
            playerInventory.DisplayInventory();
            UIManager.instance.DisplaySuccessMessage("Caught " + currentFish.fishName);
            taskManager.CheckTaskState();

        } else {
            UIManager.instance.DisplayErrorMessage("Fish got away");
        }
        miniGame.SetActive(false);
    }

    private FishData ChooseRandomFish(WaterSource waterSource)
    {
        List<FishData> fishList = GetFishList(waterSource);

        float totalProbability = 0;

        foreach(FishData fishData in fishList){
            totalProbability += fishData.probability;
        }

        float randomValue = Random.Range(0, totalProbability);

        float currentProbability = 0;

        foreach(FishData fishData in fishList){
            currentProbability += fishData.probability;
            if(randomValue <= currentProbability){
                return fishData;
            }
        }

        return null;
    }

    private List<FishData> GetFishList(WaterSource waterSource)
    {
        switch(waterSource){
            case WaterSource.Lake:
                return lakeFishList;
            case WaterSource.River:
                return riverFishList;
            case WaterSource.Sea:
                return seaFishList;
            default:
                return null;
        }
    }


}
