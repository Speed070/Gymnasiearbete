// MainMenuManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class MainMenuManager : MonoBehaviour
{
    public Button[] saveSlotButtons; // Assign buttons for save slots in Inspector
    public Button saveButton; // Button to save over current slot
    public Button settingsButton;
    public Button resumeButton;
    public Button mainMenuButton;
    public Button creditsButton;
    public Button loadButton;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public GameObject loadPanel;
    public GameObject pauseMenuUI;
    public string gameSceneName = "GameScene";
    private bool isPaused = false;

    private void Start()
    {
        
        // Ensure main menu is active on start
        mainMenuPanel.SetActive(true);
        pauseMenuUI.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        loadPanel.SetActive(false);
        
        // Attach click listeners
        for (int i = 0; i < saveSlotButtons.Length; i++)
        {
            int slotIndex = i;
            saveSlotButtons[i].onClick.AddListener(() => LoadGame(slotIndex));
        }
        
        saveButton.onClick.AddListener(SaveCurrentGame);
        settingsButton.onClick.AddListener(OpenSettings);
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        creditsButton.onClick.AddListener(OpenCredits);
        loadButton.onClick.AddListener(OpenLoadPanel);
        
    }

   public void LoadGame(int slotIndex)
{
    if (SaveSystem.SaveExists(slotIndex))
    {
        Debug.Log("Loading existing save slot: " + slotIndex);
        PlayerPrefs.SetInt("SelectedSaveSlot", slotIndex);
        SceneManager.LoadScene(gameSceneName);
    }
    else
    {
        Debug.LogWarning("No save file found for slot " + slotIndex + ". Starting a new game.");
        StartNewGame(slotIndex);
    }
}

// New function to start a new game if no save exists
private void StartNewGame(int slotIndex)
{
    Debug.Log("Starting a new game in slot " + slotIndex);
    PlayerPrefs.SetInt("SelectedSaveSlot", slotIndex);

    // Save default new game data
    SaveData newGameData = new SaveData
    {
        playerHealth = 100, // Default health
        playerPositionX = 0f, // Default starting position
        playerPositionY = 0f,
        playerPositionZ = 0f
    };

    SaveSystem.SaveGame(slotIndex, newGameData);
    
    // Load the game scene
    SceneManager.LoadScene(gameSceneName);
}


    public void SaveCurrentGame()
    {
        int slotIndex = PlayerPrefs.GetInt("SelectedSaveSlot", 0);
        SaveSystem.SaveGame(slotIndex, new SaveData()); // Replace with actual game state
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        loadPanel.SetActive(false);
        pauseMenuUI.SetActive(false);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void OpenLoadPanel()
    {
        loadPanel.SetActive(true);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
public void StartGame()
    {
        SceneManager.LoadScene("ForestScene"); // Change to your actual game scene name
    }
}

// SaveSystem.cs



public static class SaveSystem
{
    private static string GetSavePath(int slotIndex)
    {
        return Application.persistentDataPath + "/saveSlot" + slotIndex + ".json";
    }

    public static void SaveGame(int slotIndex, object saveData)
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(GetSavePath(slotIndex), json);
    }

    public static T LoadGame<T>(int slotIndex)
    {
        string path = GetSavePath(slotIndex);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }
        return default;
    }

    public static bool SaveExists(int slotIndex)
    {
        return File.Exists(GetSavePath(slotIndex));
    }
}

[System.Serializable]
public class SaveData
{
    public int playerHealth;
    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;
    // Add any other data you want to save here
}



public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
    {
        Debug.Log("Escape Key Pressed!");
        isPaused = true;
        Time.timeScale = 0f; // Freeze time to see if pause is working
    }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
