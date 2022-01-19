using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartControl : MonoBehaviour
{
  public GameObject marioPrefab;

  public void Restart()
  {

    Debug.Log("Restart " + SceneManager.GetActiveScene().buildIndex);
    // SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    StartCoroutine(InitMario());



    // StartCoroutine();
    // StartCoroutine(GameManager.Instance.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene().buildIndex));

  }

  // IEnumerator LoadingSavedGame()
  // {
  //   Scene currentScene = SceneManager.GetActiveScene();

  //   // The Application loads the Scene in the background at the same time as the current Scene.
  //   AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

  //   // Wait until the last operation fully loads to return anything
  //   while (!asyncLoad.isDone)
  //   {
  //     Debug.Log("...Loading Done");
  //     yield return null;
  //   }

  //   Debug.Log("Init");

  //   GameObject gameObject = Instantiate(marioPrefab, new Vector2(0, 5), Quaternion.identity);

  //   gameObject.transform.position = new Vector2(0, 5);
  //   int sceneIndex = GameManager.Instance.LoadSavedGame(gameObject.GetComponent<MarioController>());
  //   // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
  //   SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByBuildIndex(sceneIndex));
  //   // Unload the previous Scene
  //   yield return SceneManager.UnloadSceneAsync(currentScene);
  // }

  IEnumerator LoadingSavedGame()
  {
    Scene currentScene = SceneManager.GetActiveScene();


    // Load Scene
    string path = Path.Combine(Application.persistentDataPath, "player.hd");
    FileStream fileStream = File.OpenRead(path);
    if (fileStream != null)
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      PlayerData playerData = (PlayerData)binaryFormatter.Deserialize(fileStream);


      fileStream.Close();

      // The Application loads the Scene in the background at the same time as the current Scene.
      AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(playerData.sceneIndex, LoadSceneMode.Additive);

      // Wait until the last operation fully loads to return anything
      while (!asyncLoad.isDone)
      {
        Debug.Log("...Loading Done");
        yield return null;
      }

      Debug.Log("Init");

      GameObject gameObject = Instantiate(marioPrefab, new Vector2(0, 5), Quaternion.identity);

      MarioController controller = gameObject.GetComponent<MarioController>();
      // Setup controller
      controller.Health = playerData.health;
      controller.MaxHealth = playerData.maxHealth;
      controller.transform.position = new Vector2(playerData.position[0], playerData.position[1]);
      controller.level = playerData.level;
      controller.isChangeMario = true;
      controller.TotalTime = playerData.totalTime;
      // gameObject.transform.position = new Vector2(0, 5);
      // int sceneIndex = GameManager.Instance.LoadSavedGame(gameObject.GetComponent<MarioController>());
      // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
      SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByBuildIndex(playerData.sceneIndex));
      // Unload the previous Scene
      yield return SceneManager.UnloadSceneAsync(currentScene);
    }
    else
    {
      yield return 0;
    }
  }

  IEnumerator InitMario()
  {
    // SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    // GameManager.Instance.LoadSavedScene(gameObject);
    // yield return null;
    // yield return SceneManager.UnloadSceneAsync("DieMenu");
    yield return LoadingSavedGame();
  }

  public void MainMenu()
  {
    SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
  }
}
