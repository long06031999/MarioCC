using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseControl : MonoBehaviour
{

  // PlayerInputAction playerInputAction;
  public static bool gameIsPaused;
  public static Scene scene;

  // private void Awake()
  // {

  //   if (MarioController.playerInputAction == null)
  //   {
  //     MarioController.playerInputAction = new PlayerInputAction();
  //     MarioController.playerInputAction.Enable();
  //   }

  //   if (MarioController.playerInputAction.PlayerInputActions.OpenPauseMenu.performed == null)
  //   {

  //   }
  //    += OnOpenPauseMenu;
  // }

  // public void OnOpenPauseMenu(InputAction.CallbackContext context)
  // {
  //   PauseControl.gameIsPaused = !PauseControl.gameIsPaused;
  //   // PauseGame();
  // }

  void Update()
  {
    // if (Input.GetKeyDown(KeyCode.Escape))
    // {

    // }
  }

  // public static void PauseGame()
  // {
  //   SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
  //   GameManager.Instance.PauseGame();
  // }

  public static void ResumeGame()
  {
    PauseControl.gameIsPaused = !PauseControl.gameIsPaused;
    GameManager.Instance.ResumeGame();
  }


  public void GoToMainMenu()
  {

    PauseControl.ResumeGame();
    SceneManager.LoadScene(0);
  }

  public void SaveGame()
  {
    MarioController marioController = GameObject.FindObjectOfType<MarioController>();
    GameManager.Instance.SaveGame(marioController);
    GoToMainMenu();
  }
}
