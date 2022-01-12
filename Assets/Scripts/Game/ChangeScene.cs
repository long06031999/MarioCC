using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
  public float waitingTime = 2f;
  float timer = 0;
  // Start is called before the first frame update
  void Start()
  {
    timer = waitingTime;
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    timer = waitingTime;
  }

  private void OnTriggerStay2D(Collider2D other)
  {
    if (timer <= 0)
    {
      Debug.Log("Scene Count: " + SceneManager.sceneCount);
      int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
      // SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(nextScene));
      // SceneManager.MoveGameObjectToScene(other.gameObject, SceneManager.GetSceneByBuildIndex(nextScene));
      SceneManager.LoadScene(nextScene);
    }
    else
    {
      timer -= Time.deltaTime;
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    timer = waitingTime;
  }
}
