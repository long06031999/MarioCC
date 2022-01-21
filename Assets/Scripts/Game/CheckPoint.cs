using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CheckPoint : MonoBehaviour
{
  //   private void OnCollisionEnter2D(Collision2D other)
  //   {
  //     MarioController controller = other.gameObject.GetComponent<MarioController>();
  //     if (controller)
  //     {
  //       GameManager.Instance.SaveGame(controller);
  //       GetComponent<SpriteRenderer>().color = Color.white;
  //     }
  //   }
  public bool IsActive = false;
  private void OnTriggerEnter2D(Collider2D other)
  {
    MarioController controller = other.gameObject.GetComponent<MarioController>();
    if (controller && !IsActive)
    {
      GameManager.Instance.SaveGame(controller);
      GetComponent<SpriteRenderer>().color = Color.white;
      IsActive = true;
    }
  }
}
