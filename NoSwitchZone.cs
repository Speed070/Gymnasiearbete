using UnityEngine;

public class NoSwitchZone : MonoBehaviour
{
    private bool playerInZone = false; // Tracks if the player is inside

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true; // Player entered the NoSwitchZone
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false; // Player left the NoSwitchZone
        }
    }

    public bool IsPlayerInNoSwitchZone()
    {
        return playerInZone;
    }
}
