using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public System.Action OnNewSceneActive;

    private const string playerControllerTag = "Player";
    private const string playerSpawnTag = "PlayerSpawn";

    private string activeLevel = "";

    private GameObject playerController = null;
    private FlatController flatController = null;

    public void ChangeScene(string sceneName)
    {
        if (activeLevel != "")
        {
            SceneManager.UnloadSceneAsync(activeLevel);
        }

        activeLevel = sceneName;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        playerController = GameObject.FindGameObjectWithTag(playerControllerTag);
        flatController = playerController.GetComponent<FlatController>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        if (string.IsNullOrEmpty(activeLevel) && scene.name != "MainScene")
        {
          activeLevel = scene.name;
        }
        if (loadMode == LoadSceneMode.Additive)
        {
            SceneManager.SetActiveScene(scene);

            GameObject playerSpawn = GameObject.FindGameObjectWithTag(playerSpawnTag);

            if (playerSpawn == null)
            {
                throw new System.Exception("No GameObject with the " + playerSpawnTag + " was found in scene - " + scene.name);
            }

            playerController.transform.SetPositionAndRotation(playerSpawn.transform.position, playerSpawn.transform.rotation);
            flatController.ResetValues();

            OnNewSceneActive?.Invoke();
        }
    }
}
