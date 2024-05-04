using UnityEngine;

public class PullingGameCatcher : MonoBehaviour
{
    public float maxHeight = 120f;
    public float minHeight = -120f;
    public float moveSpeed = 200f;

    private float targetHeight; // Current target height for the catcher

    void Start()
    {
        // Initialize the target to move towards the maxHeight initially
        targetHeight = maxHeight;
    }

    void Update()
    {
        // Move towards the current target height
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, targetHeight, transform.localPosition.z), moveSpeed * Time.deltaTime);

        // Check if the catcher reached the target height
        if (Mathf.Approximately(transform.localPosition.y, targetHeight))
        {
            // Toggle the target between minHeight and maxHeight
            if (targetHeight == maxHeight)
                targetHeight = minHeight;
            else
                targetHeight = maxHeight;
        }
    }
}
