using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartControl : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Restart()
  {
    SceneManager.LoadSceneAsync("Level 1", LoadSceneMode.Single);
  }

  public void MainMenu()
  {
    SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
  }
}
