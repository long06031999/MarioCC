using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{

  // PlayerInputAction playerInputAction;
  // public GameObject PauseMenu;

  // private void Start()
  // {
  //   if (MarioController.playerInputAction == null)
  //   {
  //     MarioController.playerInputAction = new PlayerInputAction();
  //     MarioController.playerInputAction.Enable();
  //   }
  //   MarioController.playerInputAction.PlayerInputActions.OpenPauseMenu.performed += OnOpenPauseMenu;
  // }

  // public void OnOpenPauseMenu(InputAction.CallbackContext context)
  // {
  //   Debug.Log("Escape");
  //   OpenPauseMenu();
  // }
  private void Update()
  {
    // if (Input.GetKeyDown(KeyCode.Escape))
    // {
    // Debug.Log("Escape");
    // OpenPauseMenu();
    // }
  }

  public void PauseGame()
  {
    GameManager.Instance.PauseGame();
  }

  // public void ResumeGame()
  // {
  //   GameManager.Instance.ResumeGame();
  //   // PauseMenu.SetActive(false);
  // }

  // private void OpenPauseMenu()
  // {
  //   GameManager.Instance.PauseGame();
  //   PauseMenu.SetActive(true);
  // }

  public void SaveGame()
  {
    MarioController marioController = GameObject.FindObjectOfType<MarioController>();
    GameManager.Instance.SaveGame(marioController);
    ReturnMainMenu();
  }

  public void ReturnMainMenu()
  {
    GameManager.Instance.ResumeGame();
    GameManager.Instance.GoToScene(0);

  }
}
