using UnityEngine;

public class NPCSpriteSwitcher : MonoBehaviour
{
    public Sprite normalWorldSprite;
    public Sprite mirrorWorldSprite;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Subscribe to the OnWorldSwitch event
        WorldSwitcher.OnWorldSwitch += SwitchSprite;

        // Set initial sprite based on the current world state
        if (FindObjectOfType<WorldSwitcher>().IsInMirrorWorld)
        {
            spriteRenderer.sprite = mirrorWorldSprite;
        }
        else
        {
            spriteRenderer.sprite = normalWorldSprite;
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the OnWorldSwitch event when this object is destroyed
        WorldSwitcher.OnWorldSwitch -= SwitchSprite;
    }

    // Switch the sprite based on the world state
    private void SwitchSprite()
    {
        if (FindObjectOfType<WorldSwitcher>().IsInMirrorWorld)
        {
            spriteRenderer.sprite = mirrorWorldSprite;
        }
        else
        {
            spriteRenderer.sprite = normalWorldSprite;
        }
    }
}
