using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Water : MonoBehaviour
{

  private void OnTriggerEnter2D(Collider2D other)
  {
    MarioController controller = other.gameObject.GetComponent<MarioController>();
    if (controller)
    {
      controller.SetInWater(true);
    }
  }
  private void OnTriggerStay2D(Collider2D other)
  {
    MarioController controller = other.gameObject.GetComponent<MarioController>();
    if (controller)
    {
      controller.SetInWater(true);
      Debug.Log("In");
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    MarioController controller = other.gameObject.GetComponent<MarioController>();
    if (controller)
    {
      controller.SetInWater(false);
      Debug.Log("Out");
    }
  }
}
