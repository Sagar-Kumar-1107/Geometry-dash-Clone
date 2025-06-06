using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;
public enum Speeds { Slow = 0, Normal = 1, Fast = 2, Faster = 3, Fastest = 4 };
public enum Gamemodes { cube = 0 , ship = 1 , Ball = 2 , UFO =3, Wave =4 , Robot =5 ,Spider = 6};

public class Movement : MonoBehaviour
{
    public Speeds CurrentSpeed;
    public Gamemodes CurrentGamemode;
    //                       0      1      2       3      4
    float[] SpeedValues = { 8.6f, 10.4f, 12.96f, 15.6f, 19.27f };
    [System.NonSerialized] public int[] screenHeightValues = { 11, 10, 8, 10, 10, 11, 9 };
    [System.NonSerialized] public float yLastPortal = -2.3f;

    public float GroundCheckRadius;
  
    public LayerMask GroundMask;
    public Transform Sprite;

    Rigidbody2D rb;

    public int Gravity = 1;
    public bool clickProcessed = false;

    private PlayerIconSwitcher playerIconSwitcher;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Cache the PlayerIconSwitcher from children
        playerIconSwitcher = GetComponentInChildren<PlayerIconSwitcher>();

        if (playerIconSwitcher == null)
            Debug.LogError("PlayerIconSwitcher component not found in children!");
    }

    

    void FixedUpdate()
    {
        transform.position += SpeedValues[(int)CurrentSpeed] * Time.deltaTime * Vector3.right;

        string methodName = char.ToUpper(CurrentGamemode.ToString()[0]) + CurrentGamemode.ToString()[1..];
        Invoke(methodName, 0);
    }



    public bool OnGround()
    {
        return Physics2D.OverlapBox(transform.position + (0.5f * Gravity * Vector3.down), (Vector2.right * 1.1f) + (Vector2.up * GroundCheckRadius), 0, GroundMask);
    }

    bool TouchingWall()
    {
        return Physics2D.OverlapBox((Vector2)transform.position + (Vector2.right * 0.55f), Vector2.up * 0.8f + (Vector2.right * GroundCheckRadius), 0, GroundMask);
    }
    void Cube()
    {
        Generic.CreateGamemode(rb, this, true, 19.5261f, 9.057f, true , false , 409.1f);
    }

    void Ship()
    {
        rb.gravityScale = 2.93f * (Input.GetMouseButton(0)? -1 : 1) * Gravity;
        Generic.VelocityLimit(9.95f, rb);
        transform.rotation = Quaternion.Euler(0, 0, rb.linearVelocity.y * 2);
        
       
    }

    void Ball()
    {
        Generic.CreateGamemode(rb, this, true, 0, 6.2f,false, true);
    }

    void UFO()
    {
        Generic.CreateGamemode(rb, this,false, 10.841f, 4.1483f , false , false, 0, 10.841f);
    }

    void Wave()
    {
        float xSpeed = SpeedValues[(int)CurrentSpeed];
        float ySpeed = (Input.GetMouseButton(0) ? 1 : -1) * Gravity * xSpeed;

        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(xSpeed, ySpeed);

        transform.position += Vector3.right * xSpeed * Time.deltaTime;
    }

    float robotXtstart = -100;
    bool onGroundProcessed;
    bool gravityFlipped;


    void Robot()
    {
        if (!Input.GetMouseButton(0))
        {
            clickProcessed = false;
        }

        if(OnGround() && !clickProcessed && Input.GetMouseButton(0))
        {
            gravityFlipped = false;
            clickProcessed = true;
            robotXtstart = transform.position.x;
            onGroundProcessed = true;
        }

        if ((Mathf.Abs(robotXtstart) - transform.position.x) <= 3)
        {
            if (Input.GetMouseButton(0) && onGroundProcessed && !gravityFlipped)
            {
                rb.gravityScale = 0;
                rb.linearVelocity = Vector2.up * 10.4f * Gravity;
                return;
            }
        }

        else if (Input.GetMouseButton(0))
            onGroundProcessed = false;
        
        rb.gravityScale = 8.62f * Gravity;
        Generic.VelocityLimit(23.66f, rb);


    }
    void Spider()
    {
        Generic.CreateGamemode(rb, this, true, 238.29f,6.2f, false, true ,0,  238.29f);
    }



    public void ChangeThroughPortal(Gamemodes Gamemode, Speeds Speed, int gravity, int State, float yPortal)
    {
        switch (State)
        {
            case 0:
                CurrentSpeed = Speed;
                break;

            case 1:
                yLastPortal = yPortal;
                CurrentGamemode = Gamemode;
                if (playerIconSwitcher != null)
                {
                    playerIconSwitcher.SetIcon(CurrentGamemode);
                }
                else
                {
                    Debug.LogWarning("playerIconSwitcher is null, can't set icon");
                }
                break;

            case 2:
                Gravity = gravity;
                rb.gravityScale = Mathf.Abs(rb.gravityScale) * (int)gravity;
                gravityFlipped = true;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PortalScript portal = collision.GetComponent<PortalScript>();
        if (portal)
        {
            portal.intiatePortal(this);
        }
    }
}

