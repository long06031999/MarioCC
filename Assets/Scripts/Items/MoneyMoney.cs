using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MoneyMoney : MonoBehaviour
{
  public int count = 1;

  private void OnCollisionEnter2D(Collision2D other)
  {
    MarioController controller = other.gameObject.GetComponent<MarioController>();
    if (controller)
    {
      controller.PickCoin(count);
      Destroy(gameObject);
    }
  }
  // Start is called before the first frame update
}
