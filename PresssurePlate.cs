using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [Header("Tile Settings")]
    public string tileColor; // Group identifier (e.g., "pink", "blue")
    public bool isPressed; // Current state (On/Off)
    public Sprite onSprite; // Sprite to display when "On"
    public Sprite offSprite; // Sprite to display when "Off"

    [Header("Audio Settings")]
    public AudioClip pressSound;
    public AudioClip releaseSound;
    public AudioSource audioSource; // Drag an AudioSource here

    [Header("Group Settings")]
    public List<PressurePlate> linkedTiles; // Manually link tiles of the same color group in the inspector

    [Header("Events")]
    public UnityEvent onPress; // Actions to trigger when pressed
    public UnityEvent onRelease; // Actions to trigger when released

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateAppearance();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Trigger only if not already pressed
        if (!isPressed)
        {
            ActivateTileGroup();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPressed)
        {
            DeactivateTileGroup();
        }
    }

    private void ActivateTileGroup()
    {
        foreach (var tile in linkedTiles)
        {
            tile.SetState(true);
        }
    }

    private void DeactivateTileGroup()
    {
        foreach (var tile in linkedTiles)
        {
            tile.SetState(false);
        }
    }

    public void SetState(bool pressed)
    {
        isPressed = pressed;
        UpdateAppearance();

        // Play the appropriate sound
        if (audioSource != null)
        {
            audioSource.PlayOneShot(pressed ? pressSound : releaseSound);
        }

        // Trigger events
        if (pressed)
            onPress.Invoke();
        else
            onRelease.Invoke();
    }

    private void UpdateAppearance()
    {
        // Update sprite based on state
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = isPressed ? onSprite : offSprite;
        }
    }
}
