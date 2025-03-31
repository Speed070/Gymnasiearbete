using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WorldSwitcher : MonoBehaviour
{
    // References to the worlds
    public GameObject normalWorld;
    public GameObject mirrorWorld;

    // UI for feedback
    public Text feedbackText; // Reference to a UI Text element
    public AudioClip noSwitchSound; // Sound effect for failed switch
    private AudioSource audioSource; // For playing the sound

    // Cooldown and switching controls
    public float switchCooldown = 1f; // Time before switching is allowed again
    private bool isInMirrorWorld = false; // Tracks which world is active
    private bool canSwitchWorld = true; // Controls if switching is allowed
    private bool isSwitching = false; // Prevents multiple switches at once

    // Optional fade effect
    public CanvasGroup fadeCanvasGroup;
    public float transitionDuration = 0.5f;

    // Declare an event for world switch
    public static event Action OnWorldSwitch;

    // Public property to expose isInMirrorWorld
    public bool IsInMirrorWorld
    {
        get { return isInMirrorWorld; }
    }

    void Start()
    {
        // Start the game in the normal world
        normalWorld.SetActive(true);
        mirrorWorld.SetActive(false);

        // Set up audio source for sound effects
        audioSource = gameObject.AddComponent<AudioSource>();

        // Hide feedback text at start
        if (feedbackText != null)
        {
            feedbackText.text = "";
        }

        // Set the fadeCanvasGroup's alpha to 0 to ensure it's invisible at the start
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f;
        }
        {
    // Ensure the fade image is invisible at the start
    if (fadeCanvasGroup != null)
    {
        fadeCanvasGroup.alpha = 0f; // Hide the fade image initially
    }

    // Initialize worlds: Start in Normal World
    normalWorld.SetActive(true);
    mirrorWorld.SetActive(false);
}
    }

    void Update()
    {
        // Attempt to switch worlds when Spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canSwitchWorld && !isSwitching)
            {
                StartCoroutine(SwitchWorlds());
            }
            else
            {
                // Show feedback if world switching is disabled
                if (!canSwitchWorld)
                {
                    ShowFeedback("You cannot switch worlds here!");

                    // Play the no-switch sound
                    if (noSwitchSound != null)
                    {
                        audioSource.PlayOneShot(noSwitchSound);
                    }
                }
            }
        }
    }

    private IEnumerator SwitchWorlds()
    {
        isSwitching = true;

        // Optional: Fade out before switching
        if (fadeCanvasGroup != null)
        {
            yield return StartCoroutine(FadeScreen(0f, 1f, transitionDuration));
        }

        // Switch the worlds
        isInMirrorWorld = !isInMirrorWorld;
        normalWorld.SetActive(!isInMirrorWorld);
        mirrorWorld.SetActive(isInMirrorWorld);

        // Invoke the OnWorldSwitch event, notifying all listeners
        OnWorldSwitch?.Invoke();

        // Optional: Fade in after switching
        if (fadeCanvasGroup != null)
        {
            yield return StartCoroutine(FadeScreen(1f, 0f, transitionDuration));
        }

        // Cooldown before next switch is allowed
        yield return new WaitForSeconds(switchCooldown);
        isSwitching = false;
    }

    // Show a message for a short time
    private void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            CancelInvoke("HideFeedback");
            Invoke("HideFeedback", 2f); // Hide message after 2 seconds
        }
    }

    // Hide the feedback message
    private void HideFeedback()
    {
        if (feedbackText != null)
        {
            feedbackText.text = "";
        }
    }

    // Optional fade effect for smoother transitions
    private IEnumerator FadeScreen(float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        fadeCanvasGroup.alpha = to;
    }

    // Prevent switching in no-switch zones
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NoSwitchZone"))
        {
            Debug.Log("Entered No Switch Zone. Switching disabled.");
            canSwitchWorld = false;
        }
    }

    // Allow switching when exiting no-switch zones
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NoSwitchZone"))
        {
            Debug.Log("Exited No Switch Zone. Switching enabled.");
            canSwitchWorld = true;
        }
    }

}
