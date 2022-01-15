using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

  public GameObject PauseMenu;

  private void Start()
  {
  }
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      Debug.Log("Escape");
      OpenPauseMenu();
    }
  }

  public void PauseGame()
  {
    GameManager.Instance.PauseGame();
  }

  public void ResumeGame()
  {
    GameManager.Instance.ResumeGame();
    PauseMenu.SetActive(false);
  }

  private void OpenPauseMenu()
  {
    GameManager.Instance.PauseGame();
    PauseMenu.SetActive(true);
  }

  public void ReturnMainMenu()
  {
    GameManager.Instance.ResumeGame();
    GameManager.Instance.GoToScene(0);

  }
}
