using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateTile : MonoBehaviour
{
    [Header("Tile Settings")]
    [Tooltip("The color group this tile belongs to. Tiles with the same group are connected.")]
    public string tileColorGroup;

    [Tooltip("Initial state of the tile. True = On (pressed down), False = Off (not pressed).")]
    public bool initialState = false;

    [Tooltip("The sprite or material to display when the tile is On.")]
    public Sprite onSprite;

    [Tooltip("The sprite or material to display when the tile is Off.")]
    public Sprite offSprite;

    [Tooltip("The sound effect to play when the tile is activated.")]
    public AudioClip activationSound;

    [Tooltip("Custom Unity Event triggered when the tile is toggled On.")]
    public UnityEvent onTileActivated;

    [Tooltip("Custom Unity Event triggered when the tile is toggled Off.")]
    public UnityEvent onTileDeactivated;

    [Header("Interactivity")]
    [Tooltip("Can this tile be toggled by the player?")]
    public bool interactWithPlayer = true;

    [Tooltip("Can this tile be toggled by other objects like NPCs or crates?")]
    public bool interactWithObjects = true;

    private bool isPressed; // Tracks the current state of the tile
    private SpriteRenderer spriteRenderer; // For changing appearance
    private AudioSource audioSource; // For playing sounds

    // Static dictionary to manage color groups
    private static Dictionary<string, List<PressurePlateTile>> colorGroups = new Dictionary<string, List<PressurePlateTile>>();

    private void Awake()
    {
        // Initialize components
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Set initial state
        isPressed = initialState;
        UpdateVisuals();

        // Register this tile in its color group
        if (!colorGroups.ContainsKey(tileColorGroup))
        {
            colorGroups[tileColorGroup] = new List<PressurePlateTile>();
        }
        colorGroups[tileColorGroup].Add(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the tile should respond to the collider
        if ((interactWithPlayer && collision.CompareTag("Player")) ||
            (interactWithObjects && collision.CompareTag("Interactable")))
        {
            ToggleState();
        }
    }

    private void ToggleState()
    {
        // Activate all tiles in the same color group
        foreach (PressurePlateTile tile in colorGroups[tileColorGroup])
        {
            tile.SetState(true);
        }
    }

    public void SetState(bool state)
    {
        if (isPressed == state) return; // If already in the desired state, do nothing

        isPressed = state;
        UpdateVisuals();

        // Trigger Unity Events based on state
        if (isPressed)
        {
            onTileActivated.Invoke();
            PlaySound(activationSound);
        }
        else
        {
            onTileDeactivated.Invoke();
        }
    }

    private void UpdateVisuals()
    {
        // Change the appearance of the tile based on its state
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = isPressed ? onSprite : offSprite;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private void OnDestroy()
    {
        // Remove the tile from its color group when destroyed
        if (colorGroups.ContainsKey(tileColorGroup))
        {
            colorGroups[tileColorGroup].Remove(this);
        }
    }
}
