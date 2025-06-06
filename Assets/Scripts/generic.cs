using UnityEngine;

public static class Generic
{
    public static void VelocityLimit(float limit, Rigidbody2D rb)
    {
        int gravityMultiplier = (int)(Mathf.Abs(rb.gravityScale) / rb.gravityScale);
        float velocityY = rb.linearVelocity.y;

        if (velocityY * gravityMultiplier < -limit)
        {
            velocityY = -limit * gravityMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, velocityY);
        }
    }


    public static void CreateGamemode(Rigidbody2D rb, Movement host, bool onGroundRequired, float initialVelocity, float gravityScale, bool canHold = false, bool flipOnClick = false, float rotationMod = 0, float yVelocityLimit = Mathf.Infinity)
    {
        // Set gravity
        rb.gravityScale = gravityScale * host.Gravity;

        // Limit fall speed
        VelocityLimit(yVelocityLimit, rb);

        bool mouseDown = Input.GetMouseButton(0);

        // Reset clickProcessed when button is released
        if (!mouseDown)
            host.clickProcessed = false;

        // Jump trigger
        if (mouseDown && !host.clickProcessed && (!onGroundRequired || host.OnGround()))
        {
            host.clickProcessed = true;
            rb.linearVelocity = host.Gravity * initialVelocity * Vector2.up;

            // Flip gravity if needed
            if (flipOnClick)
            {
                host.Gravity *= -1;
                rb.gravityScale = gravityScale * host.Gravity;
            }
        }

        // Sprite Rotation
        if (host.OnGround() || !onGroundRequired)
        {
            Vector3 rot = host.Sprite.rotation.eulerAngles;
            rot.z = Mathf.Round(rot.z / 90f) * 90f;
            host.Sprite.rotation = Quaternion.Euler(rot);
        }
        else
        {
            host.Sprite.Rotate(Vector3.back, rotationMod * Time.deltaTime * host.Gravity);
        }
    }

}
