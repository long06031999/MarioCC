using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameMenu : MonoBehaviour
{
  // Start is called before the first frame update
  public void ReturnMainMenu()
  {
    SceneManager.LoadScene(0);
  }
}
