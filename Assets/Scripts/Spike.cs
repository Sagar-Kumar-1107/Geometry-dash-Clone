using UnityEngine;

public class Spike : MonoBehaviour
{
    // This method is called when another collider enters this trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is tagged "Player"
        if (other.CompareTag("Player"))
        {
            // Optional: Check if the player object is not null before using it
            if (other != null && other.gameObject != null)
            {
                // Example: destroy the player object
                Destroy(other.gameObject);

                // TODO: Replace the above line with your own player death or respawn logic if needed
                // For example, you could call a method on a Player script:
                // other.GetComponent<Player>().Die();
            }
        }
    }
}
