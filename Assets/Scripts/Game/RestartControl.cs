using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartControl : MonoBehaviour
{

  public void Restart()
  {
    Debug.Log("Restart " + SceneManager.GetActiveScene().buildIndex);
    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
  }

  public void MainMenu()
  {
    SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
  }
}
