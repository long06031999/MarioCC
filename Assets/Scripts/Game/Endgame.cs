using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Endgame : MonoBehaviour
{

  public GameObject EndingMenu;

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      MarioController controller = other.gameObject.GetComponent<MarioController>();
      if (controller)
      {
        StartCoroutine(Save(controller.TotalTime));
      }
    }
  }

  IEnumerator Save(float time)
  {
    Debug.Log("Loading Old Top...");
    Top old = GameManager.Instance.GetTopChallenge();
    yield return new WaitForSeconds(1);
    Debug.Log("Updating Top...");
    if (old == null || old.ponit > time)
    {
      Debug.Log("Change...");
      GameManager.Instance.SaveTopChallenge(time, "No Name");
    }
    Debug.Log("Show");
    EndingMenu.SetActive(true);

    Debug.Log("Pause Game...");
    GameManager.Instance.PauseGame();
  }
}
