using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float moveSpeed = 2f;    // Movement speed
    public bool isMovingRight = true; // Start moving right
    
    public float patrolDistance = 3f; // Distance NPC will patrol
    private Vector2 startPosition;

    private void Start()
    {
        // Store the initial position of the NPC
        startPosition = transform.position;
    }

    private void Update()
    {
        // Patrol between two points
        if (isMovingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, startPosition) > patrolDistance)
            {
                isMovingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, startPosition) > patrolDistance)
            {
                isMovingRight = true;
            }
        }
    }
}
