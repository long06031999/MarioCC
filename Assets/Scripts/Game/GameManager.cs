using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
  private static bool GameIsPause = false;
  public static bool IsPause { get { return GameIsPause; } }
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

  public void LoadGame()
  {

  }

  public void FinishGame()
  {
    StorePlayerScore();
    SceneManager.LoadScene(0);
  }
  public void StartGame() { }

  public void GoToScene(int Scene)
  {
    SceneManager.LoadScene(Scene);
  }

  //=====PRIVATE METHOD=====
  void StorePlayerScore()
  {

  }
}
