using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControl : MonoBehaviour
{
  public static bool gameIsPaused;
  public static Scene scene;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      PauseControl.gameIsPaused = !PauseControl.gameIsPaused;
      PauseGame();
    }
  }

  public static void PauseGame()
  {
    if (PauseControl.gameIsPaused)
    {
      Time.timeScale = 0f;
      SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    }
    else
    {
      Time.timeScale = 1;
      SceneManager.UnloadSceneAsync("Pause");
    }
  }

  public static void ResumeGame()
  {
    PauseControl.gameIsPaused = !PauseControl.gameIsPaused;
    PauseControl.PauseGame();
  }


  public void GoToMainMenu()
  {
    SceneManager.LoadScene(0);
    PauseControl.ResumeGame();
  }
}
