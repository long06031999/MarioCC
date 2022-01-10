using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CanBreak : MonoBehaviour
{
  public int Stiffness = 2;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnCollisionExit2D(Collision2D other)
  {
    if (other.gameObject.tag == "Player")
    {

      if (Stiffness <= 0)
      {
        Destroy(gameObject);
      }
      else
      {
        Stiffness -= 1;
      }
    }
  }
}
