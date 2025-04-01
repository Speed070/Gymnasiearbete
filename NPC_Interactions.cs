using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public string npcMessage = "Hello, traveler!";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Replace this with your dialogue system or UI display
            Debug.Log(npcMessage);
        }
    }
}
