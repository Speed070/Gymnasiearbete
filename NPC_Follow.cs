using UnityEngine;

public class NPCFollow : MonoBehaviour
{
    public Transform player; // Reference to the player transform
    public float moveSpeed = 3f; // NPC movement speed
    public float stoppingDistance = 2f; // Distance at which the NPC stops

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Only move if the distance is greater than the stopping distance
        if (distanceToPlayer > stoppingDistance)
        {
            // Calculate the direction from the NPC to the player
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction * moveSpeed; // Multiply by movement speed
        }
        else
        {
            // Stop movement if within stopping distance
            movement = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        // Apply the movement using Rigidbody2D's velocity for physics-based movement
        rb.velocity = movement;
    }

    void OnDisable()
    {
        // Stop NPC movement when disabled or removed from the scene
        rb.velocity = Vector2.zero;
    }
}
