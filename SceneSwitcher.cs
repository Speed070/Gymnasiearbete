using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToLoad; // The name of the scene to load

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Save player position
            PlayerPrefs.SetFloat("PlayerX", other.transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", other.transform.position.y);
            PlayerPrefs.SetFloat("PlayerZ", other.transform.position.z);

            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name); // Save current scene
            PlayerPrefs.Save();

            // Load new scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
