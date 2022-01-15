using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameMenu : MonoBehaviour
{
  // Start is called before the first frame update
  public void ReturnMainMenu()
  {
    Time.timeScale = 1f;
    SceneManager.LoadScene(0);
  }
}
