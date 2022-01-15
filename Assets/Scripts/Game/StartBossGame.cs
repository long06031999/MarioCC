using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossGame : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    Time.timeScale = 0f;
  }

  // Update is called once per frame
  public void ResumeGame()
  {
    Time.timeScale = 1f;
    gameObject.SetActive(false);
  }
}
