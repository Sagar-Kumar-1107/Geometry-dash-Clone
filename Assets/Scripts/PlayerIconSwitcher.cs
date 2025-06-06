using UnityEngine;

public class PlayerIconSwitcher : MonoBehaviour
{
    public Sprite cubeIcon;
    public Sprite shipIcon;
    public Sprite ballIcon;
    public Sprite ufoIcon;
    public Sprite waveIcon;
    public Sprite robotIcon;
    public Sprite spiderIcon;

    private SpriteRenderer squareRenderer;

    void Awake()
    {
        // Find the child named "Square" under player GameObject
        Transform square = transform.Find("Square");
        if (square == null)
        {
            Debug.LogError("Square child not found under player!");
            return;
        }

        squareRenderer = square.GetComponent<SpriteRenderer>();
        if (squareRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on Square GameObject!");
        }
    }

    public void SetIcon(Gamemodes mode)
    {
        if (squareRenderer == null)
        {
            Debug.LogError("SquareRenderer is null — can't set icon");
            return;
        }

        Sprite selected = cubeIcon;

        switch (mode)
        {
            case Gamemodes.cube:
                selected = cubeIcon;
                break;
            case Gamemodes.ship:
                selected = shipIcon;
                break;
            case Gamemodes.Ball:
                selected = ballIcon;
                break;
            case Gamemodes.UFO:
                selected = ufoIcon;
                break;
            case Gamemodes.Wave:
                selected = waveIcon;
                break;
            case Gamemodes.Robot:
                selected = robotIcon;
                break;
            case Gamemodes.Spider:
                selected = spiderIcon;
                break;
        }

        squareRenderer.sprite = selected;
        Debug.Log("Icon changed to: " + selected.name);
    }
}
