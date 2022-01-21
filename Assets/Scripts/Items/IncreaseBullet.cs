using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IncreaseBullet : MonoBehaviour
{
  public int ItemSize = 1;
  // Start is called before the first frame update
  private void OnCollisionEnter2D(Collision2D other)
  {
    MarioController controller = other.gameObject.GetComponent<MarioController>();
    if (controller)
    {
      controller.PickIncreaseSizeOfBullet(ItemSize);
      Destroy(gameObject);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {

    MarioController controller = other.gameObject.GetComponent<MarioController>();
    if (controller)
    {
      controller.PickIncreaseSizeOfBullet(ItemSize);
      Destroy(gameObject);
    }
  }
}
