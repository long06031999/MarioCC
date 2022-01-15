using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossGame : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    GameManager.Instance.PauseGame();
  }

  // Update is called once per frame
  public void ResumeGame()
  {
    GameManager.Instance.ResumeGame();
    gameObject.SetActive(false);
  }
}
