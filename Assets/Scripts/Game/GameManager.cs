using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager
{
  private static bool GameIsPause = false;
  public static bool IsPause { get { return GameIsPause; } }
  public bool ReloadGame = false;
  private static GameManager _instance;
  public static GameManager Instance
  {
    get
    {
      if (_instance == null)
      {
        _instance = new GameManager();
      }

      return _instance;
    }
  }

  public void PauseGame()
  {
    Time.timeScale = 0f;
    GameIsPause = true;
  }
  public void ResumeGame()
  {
    Time.timeScale = 1f;
    GameIsPause = false;
  }

  public void SaveGame()
  {

  }


  public void FinishGame()
  {
    StorePlayerScore();
    SceneManager.LoadScene(0);
  }
  public void StartGame()
  {
    ReloadGame = false;
  }

  public void GoToScene(int Scene)
  {
    SceneManager.LoadScene(Scene);
  }

  public void SaveGame(MarioController marioController)
  {
    // Get Player Data
    int sceneIndex = marioController.gameObject.scene.buildIndex;
    PlayerData playerData = new PlayerData(marioController.Health, marioController.MaxHealth, marioController.transform.position,
      sceneIndex, marioController.TotalTime, marioController.CurrentLevel, marioController.bulletNumber, marioController.LifePoint);

    // Create File
    string path = Path.Combine(Application.persistentDataPath, "player.hd");
    FileStream fileStream = File.Create(path);

    // Write
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    binaryFormatter.Serialize(fileStream, playerData);

    fileStream.Close();
    Debug.Log("...Saved");
  }

  public void SaveTopChallenge(float ponit, string name)
  {
    // Get Player Data
    Top top = new Top(ponit, name);

    // Create File
    string path = Path.Combine(Application.persistentDataPath, "top.hd");
    FileStream fileStream = File.Create(path);

    // Write
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    binaryFormatter.Serialize(fileStream, top);

    fileStream.Close();
    Debug.Log("...Saved");
  }

  public Top GetTopChallenge()
  {
    string path = Path.Combine(Application.persistentDataPath, "top.hd");
    FileStream fileStream = File.OpenRead(path);
    if (fileStream != null)
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      Top top = (Top)binaryFormatter.Deserialize(fileStream);

      return top;
    }
    else { return null; }
  }

  public PlayerData GetSavedPlayerData()
  {
    //Open File;
    string path = Path.Combine(Application.persistentDataPath, "player.hd");
    FileStream fileStream = File.OpenRead(path);
    if (fileStream != null)
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      PlayerData playerData = (PlayerData)binaryFormatter.Deserialize(fileStream);
      fileStream.Close();
      return playerData;
    }
    return null;
  }

  public IEnumerator LoadSavedScene(GameObject mario)
  {
    ReloadGame = true;
    string path = Path.Combine(Application.persistentDataPath, "player.hd");
    FileStream fileStream = File.OpenRead(path);
    if (fileStream != null)
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      PlayerData playerData = (PlayerData)binaryFormatter.Deserialize(fileStream);

      LoadSavedGame(mario.GetComponent<MarioController>());

      yield return MoveGameObjectToScene(mario, playerData.sceneIndex);
    }
    else
    {
      yield return null;
    }
  }

  public int LoadSavedGame(MarioController controller)
  {
    //Open File;
    string path = Path.Combine(Application.persistentDataPath, "player.hd");
    FileStream fileStream = File.OpenRead(path);
    if (fileStream != null)
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      PlayerData playerData = (PlayerData)binaryFormatter.Deserialize(fileStream);

      // Setup controller
      controller.Health = playerData.health;
      controller.MaxHealth = playerData.maxHealth;
      controller.transform.position = new Vector2(playerData.position[0], playerData.position[1]);
      controller.CurrentLevel = (MarioLevelEnum)playerData.level;
      controller.isChangeMario = true;
      controller.TotalTime = playerData.totalTime;
      return playerData.sceneIndex;
    }
    return 1;
  }

  public IEnumerator MoveGameObjectToScene(GameObject other, int sceneIndex)
  {
    // SceneManager.LoadScene(nextScene);

    // Set the current Scene to be able to unload it later
    Scene currentScene = SceneManager.GetActiveScene();

    // The Application loads the Scene in the background at the same time as the current Scene.
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

    // Wait until the last operation fully loads to return anything
    while (!asyncLoad.isDone)
    {
      yield return null;
    }

    other.transform.position = new Vector2(0, 5);
    // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
    SceneManager.MoveGameObjectToScene(other, SceneManager.GetSceneByBuildIndex(sceneIndex));
    // Unload the previous Scene
    SceneManager.UnloadSceneAsync(currentScene);
  }
  //=====PRIVATE METHOD=====
  void StorePlayerScore()
  {

  }
}

[Serializable]
public class Top
{
  public float ponit;
  public string name;

  public Top(float ponit, string name)
  {
    this.ponit = ponit;
    this.name = name;
  }
}