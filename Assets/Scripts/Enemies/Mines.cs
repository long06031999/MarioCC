using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Mines : MonoBehaviour
{
  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      Vector2 vector2 = other.contacts[0].normal * -1;

      Rigidbody2D rigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();
      if (rigidbody2D)
      {
        rigidbody2D.AddRelativeForce(vector2 * 5000);
      }
    }
  }
}
