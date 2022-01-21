using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IncreaseMaxhealth : MonoBehaviour
{
  public int ItemSize = 20;
  // Start is called before the first frame update
  private void OnCollisionEnter2D(Collision2D other)
  {
    MarioController controller = other.gameObject.GetComponent<MarioController>();
    if (controller)
    {
      controller.PickMaxHealthIncrease(ItemSize);
      Destroy(gameObject);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {

    MarioController controller = other.gameObject.GetComponent<MarioController>();
    if (controller)
    {
      controller.PickMaxHealthIncrease(ItemSize);
      Destroy(gameObject);
    }
  }

}
