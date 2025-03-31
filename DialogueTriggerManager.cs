using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueTriggerManager : MonoBehaviour
{
    // === UI References ===
    [Header("UI References")]
    public TextMeshProUGUI dialogueText; // Main dialogue text
    public TextMeshProUGUI characterNameText; // Character name text
    public Image portraitImage; // Character portrait
    public Button skipButton; // Button to skip dialogue or typing

    // === Settings ===
    [Header("Typing Settings")]
    public float typingSpeed = 0.02f; // Speed of text typing animation

    [Header("Dimension Settings")]
    public string normalDimensionName = "Normal NPC"; // Default character name for the normal world
    public string mirrorDimensionName = "Mirror NPC"; // Default character name for the mirror world
    public Color normalDimensionNameColor = Color.white; // Name text color for normal world
    public Color mirrorDimensionNameColor = Color.red; // Name text color for mirror world
    public Sprite normalDimensionPortrait; // Portrait for normal world
    public Sprite mirrorDimensionPortrait; // Portrait for mirror world

    // === Trigger Zone ===
    [Header("Trigger Settings")]
    public LayerMask playerLayer; // Specify player layer for the trigger zone
    public bool autoStartDialogue = true; // Automatically start dialogue when entering the trigger zone

    // === Internal Variables ===
    private Queue<string> sentences = new Queue<string>(); // Dialogue sentences queue
    private bool isTyping = false; // Is text currently typing
    private bool isInMirrorWorld = false; // Track which world the player is in
    private bool dialogueActive = false; // Is dialogue currently active?

    private void Awake()
    {
        if (skipButton != null)
        {
            skipButton.onClick.AddListener(SkipOrNextSentence);
        }
    }

    private void Update()
    {
        // Optional: Toggle between dimensions for testing
        if (Input.GetKeyDown(KeyCode.T)) // Replace 'T' with your desired toggle key
        {
            ToggleWorld();
        }

        // Optional: Advance dialogue with a key press
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space)) // Replace 'Space' with your preferred key
        {
            SkipOrNextSentence();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer.value) != 0 && autoStartDialogue)
        {
            StartDialogue(CreateDimensionSpecificDialogue());
        }
    }

    /// <summary>
    /// Toggle between dimensions and update visuals accordingly.
    /// </summary>
    public void ToggleWorld()
    {
        isInMirrorWorld = !isInMirrorWorld;
        UpdateDimensionVisuals();
    }

    /// <summary>
    /// Dynamically update character visuals based on the current dimension.
    /// </summary>
    private void UpdateDimensionVisuals()
    {
        if (isInMirrorWorld)
        {
            characterNameText.text = mirrorDimensionName;
            characterNameText.color = mirrorDimensionNameColor;
            portraitImage.sprite = mirrorDimensionPortrait;
        }
        else
        {
            characterNameText.text = normalDimensionName;
            characterNameText.color = normalDimensionNameColor;
            portraitImage.sprite = normalDimensionPortrait;
        }
    }

    /// <summary>
    /// Dynamically create dialogue data specific to the current dimension.
    /// </summary>
    private Dialogue CreateDimensionSpecificDialogue()
    {
        return isInMirrorWorld
            ? new Dialogue
            {
                characterName = mirrorDimensionName,
                sentences = new string[]
                {
                    "Welcome to the Mirror World!",
                    "Here, everything is reversed...",
                    "Do you feel the difference?"
                },
                characterPortrait = mirrorDimensionPortrait
            }
            : new Dialogue
            {
                characterName = normalDimensionName,
                sentences = new string[]
                {
                    "Welcome to the Normal World!",
                    "This is where everything started.",
                    "Feel at home, brave adventurer."
                },
                characterPortrait = normalDimensionPortrait
            };
    }

    /// <summary>
    /// Starts the dialogue process.
    /// </summary>
    public void StartDialogue(Dialogue dialogue)
    {
        dialogueActive = true;
        UpdateDimensionVisuals();

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    /// <summary>
    /// Displays the next sentence in the dialogue.
    /// </summary>
    public void DisplayNextSentence()
    {
        if (isTyping) return;

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// Types out a sentence character by character.
    /// </summary>
    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    /// <summary>
    /// Skips typing animation or advances to the next sentence.
    /// </summary>
    public void SkipOrNextSentence()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = sentences.Peek();
            isTyping = false;
        }
        else
        {
            DisplayNextSentence();
        }
    }

    /// <summary>
    /// Ends the dialogue and clears the UI.
    /// </summary>
    private void EndDialogue()
    {
        dialogueText.text = "";
        characterNameText.text = "";
        portraitImage.sprite = null;
        dialogueActive = false;
    }

    [System.Serializable]
    public class Dialogue
    {
        public string characterName;
        public Sprite characterPortrait;
        [TextArea(3, 10)]
        public string[] sentences;
    }
}
