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
      GameManager.Instance.PauseGame();

      Top old = GameManager.Instance.GetTopChallenge();
      if (old.ponit > other.gameObject.GetComponent<MarioController>().TotalTime)
        GameManager.Instance.SaveTopChallenge(other.gameObject.GetComponent<MarioController>().TotalTime, "No Name");
      EndingMenu.SetActive(true);
    }
  }
}
