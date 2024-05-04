using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishMovement : MonoBehaviour
{
    public float maxLeft = -250f;
    public float maxRight = 250f;
    public float moveSpeed = 200f;
    public float moveFrequency = 0.01f;

    public float targetPosition;
    public bool isMovingRight = true;
    


    void Start()
    {
        targetPosition = Random.Range(maxLeft, maxRight);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(targetPosition, transform.localPosition.y, transform.localPosition.z), moveSpeed * Time.deltaTime);

        if(Mathf.Approximately(transform.localPosition.x, targetPosition)){
            targetPosition = Random.Range(maxLeft, maxRight);
        }

        if (Random.value < moveFrequency){
            isMovingRight = !isMovingRight;
            targetPosition = isMovingRight ? maxRight : maxLeft;
        }
    }

}
