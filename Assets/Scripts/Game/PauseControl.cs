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
    SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    GameManager.Instance.PauseGame();
  }

  public static void ResumeGame()
  {
    PauseControl.gameIsPaused = !PauseControl.gameIsPaused;

    SceneManager.UnloadSceneAsync("Pause");
    GameManager.Instance.ResumeGame();
  }


  public void GoToMainMenu()
  {
    SceneManager.LoadScene(0);
    PauseControl.ResumeGame();
  }
}
