using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingGame : MonoBehaviour
{
    public RectTransform fish;
    public RectTransform catcher;
    public RawImage fishImage;
    public bool isOverlapping = false;

    public Slider successSlider;
    float successIncrement = 15f;
    float failDecrement = 12f;

    float successThreshold = 100f;
    float failThreshold = -100f;

    float successCounter = 0f;

    FishData currentFish;
    // Start is called before the first frame update
    void Start()
    {
        // Set fish data of Fish Movement
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckOverlap(fish, catcher)){
            isOverlapping = true;
        } else {
            isOverlapping = false;
        }

        OverLappingCalculation();
    }

    private void OverLappingCalculation() {
        if (isOverlapping){
            successCounter += successIncrement * Time.deltaTime;
        } else {
            successCounter -= failDecrement * Time.deltaTime;
        }

        successCounter = Mathf.Clamp(successCounter, failThreshold, successThreshold);
        successSlider.value = successCounter;

        if(successCounter == successThreshold){
           
            successCounter = 0f;
            successSlider.value = 0f;
            FishingSystem.instance.EndMiniGame(true, currentFish);
            
        } else if(successCounter == failThreshold){

            successCounter = 0f;
            successSlider.value = 0f;
            FishingSystem.instance.EndMiniGame(false, currentFish);

        }
    }

    private bool CheckOverlap(RectTransform rect1, RectTransform rect2){
        if(rect1 == null || rect2 == null){
            return false;
        }

        if(rect1 == rect2){
            return false;
        }

        Rect r1 = new Rect(rect1.localPosition.x, rect1.localPosition.y, rect1.rect.width, rect1.rect.height);
        Rect r2 = new Rect(rect2.localPosition.x, rect2.localPosition.y, rect2.rect.width, rect2.rect.height);

        return r1.Overlaps(r2);
    }

    public void SetFish(FishData fishData){
        currentFish = fishData;
        fishImage.texture = fishData.texture;
    }
}
