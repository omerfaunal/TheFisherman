using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Color gizmoColor = Color.blue; // Color of the waypoint gizmos
    public Color lineColor = Color.red;
    public float gizmoRadius = 0.5f; // Radius of the waypoint gizmos

    // Draw gizmos in the scene view
    void OnDrawGizmos()
    {
        // Set gizmo color
        foreach(Transform t in transform)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(t.position, gizmoRadius);
        }

        Gizmos.color = lineColor;
        for(int i = 0; i < transform.childCount - 1; i++)
        {
            // Draw a line from this waypoint to the other waypoint
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i+1).position);
        }
        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }
}
