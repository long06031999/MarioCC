using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Endgame : MonoBehaviour
{

  public GameObject EndingMenu;

  private void OnCollisionEnter2D(Collision2D other)
  {
    GameManager.Instance.PauseGame();
    EndingMenu.SetActive(true);
  }
}
