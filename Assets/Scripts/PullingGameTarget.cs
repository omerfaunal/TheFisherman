using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullingGameTarget : MonoBehaviour
{
    public float maxHeight = 120;
    public float minHeight = -120;

   

    // Start is called before the first frame update
    void Start()
    {
        // Set the target position to a random value between the min and max height
        transform.localPosition = new Vector3(transform.localPosition.x, Random.Range(minHeight, maxHeight), transform.localPosition.z);   
        transform.localScale = new Vector3(1, Random.Range(0.3f, 1),1);   
    }

}
