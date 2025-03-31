using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class NewGameManager : MonoBehaviour
{
    public string firstGameScene = "ForestScene"; // Default first scene
    public string cutsceneScene = "CutsceneScene"; // Replace with the actual cutscene scene name
    public CanvasGroup fadeCanvas; // Assign a UI Panel with CanvasGroup for fading

    private bool isOverwriting = false;

    void Start()
    {
        fadeCanvas.alpha = 0; // Make sure fade starts invisible
    }

    public void StartNewGame()
    {
        StartCoroutine(FadeAndStartNewGame());
    }

    private IEnumerator FadeAndStartNewGame()
    {
        // Fade out effect
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            fadeCanvas.alpha = t;
            yield return null;
        }

        fadeCanvas.alpha = 1;

        // Start the cutscene before the game
        SceneManager.LoadScene(cutsceneScene);
    }

    public void OnCutsceneFinished()
    {
        // After the cutscene ends, start the actual game
        SceneManager.LoadScene(firstGameScene);
    }

    public void EnableOverwriteMode()
    {
        isOverwriting = true;
    }

    public void SaveGame(int slotIndex)
    {
        if (isOverwriting)
        {
            SaveSystem.SaveGame(slotIndex, new SaveData());
            isOverwriting = false; // Reset after use
        }
    }
}