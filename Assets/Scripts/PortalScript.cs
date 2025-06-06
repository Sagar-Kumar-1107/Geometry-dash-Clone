   using UnityEngine;

public class PortalScript: MonoBehaviour
{
    public Gamemodes Gamemode;
    public Speeds Speed;
    public bool gravity;
    public int State;

    public void intiatePortal( Movement movement)
    {
        movement.ChangeThroughPortal(Gamemode, Speed, gravity ? 1 : -1, State , transform.position.y);
    }
}
