using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneEnd : MonoBehaviour
{
    public void EndCutscene()
    {
        FindObjectOfType<NewGameManager>().OnCutsceneFinished();
    }
}