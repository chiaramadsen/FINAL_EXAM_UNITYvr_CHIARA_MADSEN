using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Method to change the scene by name
    public void ChangeScene(string sceneName)
    {
        // Check if the scene is valid
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene " + sceneName + " cannot be loaded. Check the scene name and ensure it is added to the Build Settings.");
        }
    }

    // Method to change the scene by index
    public void ChangeScene(int sceneIndex)
    {
        // Check if the scene index is valid
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("Scene index " + sceneIndex + " is out of range. Check the Build Settings to ensure the index is valid.");
        }
    }
}
