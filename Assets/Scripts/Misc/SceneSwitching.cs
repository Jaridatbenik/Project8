using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneSwitching : MonoBehaviour
{
    public string targetSceneName = "SceneName";

    public void LoadScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}