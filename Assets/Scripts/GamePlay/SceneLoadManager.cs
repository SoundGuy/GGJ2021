using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    string activeLevel;

    public void ChangeScene(string sceneName)
    {
        if (activeLevel != "")
        {
            SceneManager.UnloadSceneAsync(activeLevel);
        }

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        if (loadMode == LoadSceneMode.Additive)
        {
            SceneManager.SetActiveScene(scene);           
        }
    }
}
