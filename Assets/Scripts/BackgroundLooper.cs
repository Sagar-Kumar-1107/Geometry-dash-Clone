using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public Transform player;
    public Transform background1; // Assign Background
    public Transform background2; // Assign Background1

    private float backgroundWidth;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        backgroundWidth = background1.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Only proceed if player still exists and has not been destroyed
        if (player == null)
            return;

        // Check and reposition background1
        if (player != null && player.position.x > background1.position.x + backgroundWidth)
        {
            background1.position += Vector3.right * backgroundWidth * 2;
        }

        // Check and reposition background2
        if (player != null && player.position.x > background2.position.x + backgroundWidth)
        {
            background2.position += Vector3.right * backgroundWidth * 2;
        }
    }
}
