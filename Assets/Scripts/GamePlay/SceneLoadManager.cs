using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private const string playerControllerTag = "Player";
    private const string playerSpawnTag = "PlayerSpawn";

    private string activeLevel = "";

    private GameObject playerController;

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
        playerController = GameObject.FindGameObjectWithTag(playerControllerTag);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        if (loadMode == LoadSceneMode.Additive)
        {
            SceneManager.SetActiveScene(scene);

            GameObject playerSpawn = GameObject.FindGameObjectWithTag(playerSpawnTag);

            if (playerSpawn == null)
            {
                throw new System.Exception("No GameObject with the " + playerSpawnTag + " was found in scene - " + scene.name);
            }

            playerController.transform.SetPositionAndRotation(playerSpawn.transform.position, playerSpawn.transform.rotation);
        }
    }
}
