using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullingGame : MonoBehaviour
{
    public RectTransform target;
    public RectTransform catcher;

     public int health = 3;

     private int attempts = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // If mouse clicked
        if(Input.GetMouseButtonDown(0)){
            // If target and catcher are overlapping
            if(CheckOverlap(target, catcher)){
                Debug.Log("Overlapping");
                FishingSystem.instance.EndPushing(true);
                attempts = 0;
            } else {
                Debug.Log("Not overlapping");
                attempts += 1;
            }
            if(attempts >= health){
                FishingSystem.instance.EndPushing(false);
                attempts = 0;
            }
        }
    }

     private bool CheckOverlap(RectTransform rect1, RectTransform rect2){
        if(rect1 == null || rect2 == null){
            return false;
        }

        if(rect1 == rect2){
            return false;
        }

        // Check if rect1 overlaps rect2
        Rect r1 = new Rect(rect1.localPosition.x, rect1.localPosition.y, rect1.rect.width, rect1.rect.height);
        Rect r2 = new Rect(rect2.localPosition.x, rect2.localPosition.y, rect2.rect.width, rect2.rect.height);

        return r1.Overlaps(r2);
    }
}
