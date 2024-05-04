using UnityEngine;
using System.Collections;

public class WaypointWalker : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints defining the route
    public float moveSpeed = 2f; // Speed of movement between waypoints
    private Animator animator; 

    private int currentWaypointIndex = 0; // Index of the current waypoint
    private bool movingForward = true; // Flag to indicate the direction of movement

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component
        StartCoroutine(WalkBetweenWaypoints());
    }

    IEnumerator WalkBetweenWaypoints()
    {
        while (true)
        {
            // Move towards the current waypoint
            Transform currentWaypoint = waypoints[currentWaypointIndex];
            while (Vector3.Distance(transform.position, currentWaypoint.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
                animator.SetFloat("Speed", moveSpeed);
                yield return null;
            }

            // If moving forward and reached the last waypoint, turn 180 degrees
            if (movingForward && currentWaypointIndex == waypoints.Length - 1)
            {
                Rotate180Degrees();
                movingForward = false;
            }
            // If moving backward and reached the first waypoint, start moving forward
            else if (!movingForward && currentWaypointIndex == 0)
            {
                Rotate180Degrees();
                movingForward = true;
            }

            // Update the current waypoint index based on movement direction
            currentWaypointIndex += movingForward ? 1 : -1;
            yield return null;
        }
    }

    void Rotate180Degrees()
    {
        // Rotate the character 180 degrees around the Y axis
        transform.Rotate(Vector3.up, 180f);
    }
}
