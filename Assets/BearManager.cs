using System;
using UnityEngine;
using System.Collections;
using BLINK;

public class BearManager : MonoBehaviour
{
    public Vector3 startPoint; // Start point of the corridor
    public Transform endPoint; // End point of the corridor
    public float walkSpeed = 3f; // Speed of the bear's walk
    public float rotationSpeed = 5f; // Speed of rotation
    public GameManager gameManager;
    public float waitTime = 1f; // Time to wait at each end before turning arounds
    public Animator animator;
    public Transform player;
    public float fovAngle = 90f;
    public float viewDistance = 10f;
    public LayerMask obstacleMask;
    public float attackDistance = 2f; 
    private bool isAttacking = false;
    private bool walkingForward = true;

    void Start()
    {
        startPoint = transform.position;
        animator.SetBool("Run Forward", true);
        animator.SetTrigger("Run Forward");
        StartCoroutine(WalkCoroutine());
    }
    
    void Update()
    {
        // Calculate direction to the player
        Vector3 directionToPlayer = player.position - transform.position;

        // Check if the guard can see the player
        if (CanSeePlayer())
        {
            Debug.Log("see");
            // Move towards the player
            transform.position += directionToPlayer.normalized * walkSpeed * Time.deltaTime;
            
        }
    }

    IEnumerator WalkCoroutine()
    {
        while (true)
        {
            Vector3 target;
            if (isAttacking)
            {
                target = player.position;
                walkSpeed -= 5;
            }
            else
            {
                if (walkingForward)
                {
                    target = endPoint.position;
                }
                else
                {
                    target = startPoint;
                }

            }


            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                // Move the bear towards the target
                transform.position = Vector3.MoveTowards(transform.position, target, walkSpeed * Time.deltaTime);
                yield return null;
            }

            // Wait for the specified time
            yield return new WaitForSeconds(waitTime);

            // Change direction
            walkingForward = !walkingForward;
            transform.Rotate(Vector3.up, 180f);
        }
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

        Gizmos.color = new Color(1, 0, 0, 0.1f);
        Gizmos.DrawSphere(transform.position + transform.forward * viewDistance, 0.1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.GameOver(false);
        }
    }
}