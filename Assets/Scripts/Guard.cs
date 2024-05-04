using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float attackDistance = 2f; // Distance threshold for attacking
    public float moveSpeed = 2f; // Speed of movement towards the player
    public float boxCastDistance = 5f; // Adjust this value to change the distance of the box cast
    public float boxCastWidth = 3f; // Adjust this value to change the width of the box cast
    public float boxCastHeight = 3f; // Adjust this value to change the height of the box cast
    public Animator animator; // Reference to the Animator component for animation
    public LayerMask obstacleMask; // Layer mask for obstacles that block the guard's vision

    public float fovAngle = 90f;
    public float viewDistance = 10f;

    private bool isAttacking = false;
    RaycastHit hit;

    void Update()
    {
        // Calculate direction to the player
        Vector3 directionToPlayer = player.position - transform.position;

        // Check if the guard can see the player
        if (CanSeePlayer())
        {
            // Move towards the player
            transform.position += directionToPlayer.normalized * moveSpeed * Time.deltaTime;

            // Check if the guard is within attacking distance
            if (directionToPlayer.magnitude <= attackDistance && !isAttacking)
            {
                // Stop the guard's movement and trigger an attack animation
                StartCoroutine(AttackCoroutine());
            }
            else
            {
                animator.SetTrigger("Run");
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        // Set the attacking flag to true to prevent multiple attacks
        isAttacking = true;

        // Trigger the attack animation
        animator.SetTrigger("Attack");

        // Wait for the duration of the attack animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Trigger the game over event
        //GameManager.instance.GameOver(false, true, false);

        // Reset the attacking flag
        isAttacking = false;
    }

    private bool CanSeePlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        if (Vector3.Angle(transform.forward, directionToPlayer) < fovAngle / 2f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, viewDistance, obstacleMask))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {

        Vector3 fovLine1 = Quaternion.AngleAxis(fovAngle / 2f, transform.up) * transform.forward * viewDistance;
        Vector3 fovLine2 = Quaternion.AngleAxis(-fovAngle / 2f, transform.up) * transform.forward * viewDistance;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);
    }

    private void DrawBoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float distance)
    {
        // Calculate the corners of the box
        Vector3 p1 = center + orientation * new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);
        Vector3 p2 = center + orientation * new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);
        Vector3 p3 = center + orientation * new Vector3(halfExtents.x, -halfExtents.y, halfExtents.z);
        Vector3 p4 = center + orientation * new Vector3(-halfExtents.x, -halfExtents.y, halfExtents.z);
        Vector3 p5 = center + orientation * new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
        Vector3 p6 = center + orientation * new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
        Vector3 p7 = center + orientation * new Vector3(halfExtents.x, halfExtents.y, halfExtents.z);
        Vector3 p8 = center + orientation * new Vector3(-halfExtents.x, halfExtents.y, halfExtents.z);

        // Set the color to green
        Color color = Color.green;

        // Draw the bottom of the box
        Debug.DrawLine(p1, p2, color);
        Debug.DrawLine(p2, p3, color);
        Debug.DrawLine(p3, p4, color);
        Debug.DrawLine(p4, p1, color);

        // Draw the top of the box
        Debug.DrawLine(p5, p6, color);
        Debug.DrawLine(p6, p7, color);
        Debug.DrawLine(p7, p8, color);
        Debug.DrawLine(p8, p5, color);

        // Draw the vertical edges
        Debug.DrawLine(p1, p5, color);
        Debug.DrawLine(p2, p6, color);
        Debug.DrawLine(p3, p7, color);
        Debug.DrawLine(p4, p8, color);
    }

}
