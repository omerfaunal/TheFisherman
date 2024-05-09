using System;
using UnityEngine;
using System.Collections;

public class BearManager : MonoBehaviour
{
    public Vector3 startPoint; // Start point of the corridor
    public Transform endPoint; // End point of the corridor
    public float walkSpeed = 3f; // Speed of the bear's walk
    public float rotationSpeed = 5f; // Speed of rotation
    public float waitTime = 1f; // Time to wait at each end before turning around

    private bool walkingForward = true;

    void Start()
    {
        startPoint = transform.position;
        StartCoroutine(WalkCoroutine());
    }

    IEnumerator WalkCoroutine()
    {
        while (true)
        {
            Vector3 target;

            if (walkingForward)
            {
                target = endPoint.position;
            }
            else
            {
                target = startPoint;
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
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //GameManager.EndGame()
        }
    }
}