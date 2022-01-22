using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bounce : MonoBehaviour
{
  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Player")
    {

      Rigidbody2D rigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();

      if (rigidbody2D)
      {
        Vector2 vector2 = other.contacts[0].normal * -1;
        rigidbody2D.AddForce(vector2 * 5000);
      }
    }
  }
}
