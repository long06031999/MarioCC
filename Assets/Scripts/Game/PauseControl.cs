using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseControl : MonoBehaviour
{

  PlayerInputAction playerInputAction;
  public static bool gameIsPaused;
  public static Scene scene;

  private void Awake()
  {
    playerInputAction = new PlayerInputAction();
    playerInputAction.Enable();

    playerInputAction.PlayerInputActions.OpenPauseMenu.performed += OnOpenPauseMenu;
  }

  public void OnOpenPauseMenu(InputAction.CallbackContext context)
  {
    PauseControl.gameIsPaused = !PauseControl.gameIsPaused;
    PauseGame();
  }

  void Update()
  {
    // if (Input.GetKeyDown(KeyCode.Escape))
    // {

    // }
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
