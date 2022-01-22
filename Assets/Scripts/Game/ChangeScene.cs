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
    if (other.tag == "Player")
      if (timer <= 0)
      {
        MarioController marioController = other.gameObject.GetComponent<MarioController>();
        if (marioController)
        {
          // marioController.SetInWater(false);
          // Debug.Log("Scene Count: " + SceneManager.sceneCount);
          int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
          StartCoroutine(GameManager.Instance.MoveGameObjectToScene(other.gameObject, nextScene));

          timer = waitingTime;
        }

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
