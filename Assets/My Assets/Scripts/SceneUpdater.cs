using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene loading

public class SceneLoader : MonoBehaviour
{
    // Call this method from a button
    public void LoadSceneByName(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene not found: " + sceneName);
        }
    }

    public void QuitGame() 
    {         
        Debug.Log("Quitting the game...");
#if UNITY_EDITOR
        // Stop play mode in the editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    

}