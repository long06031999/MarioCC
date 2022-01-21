using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Undead : MonoBehaviour
{
  public float duration = 5f;

  private void OnTriggerEnter2D(Collider2D other)
  {
    MarioController controller = other.gameObject.GetComponent<MarioController>();
    if (controller)
    {
      controller.SetUndeadDuration(duration);
      Destroy(gameObject);
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    MarioController controller = other.gameObject.GetComponent<MarioController>();
    if (controller)
    {
      controller.SetUndeadDuration(duration);
      Destroy(gameObject);
    }
  }

}
