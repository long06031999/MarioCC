using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Collider))]
public class DeadZone : MonoBehaviour
{
  // Start is called before the first frame update
  private void OnCollisionEnter2D(Collision2D other)
  {
    MarioController controller = other.gameObject.GetComponent<MarioController>();
    if (controller)
    {
      Debug.Log("mario Die");
      controller.DestroyMario();
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    MarioController controller = other.gameObject.GetComponent<MarioController>();
    if (controller)
    {
      Debug.Log("mario Die");
      controller.DestroyMario();
    }
  }
}
