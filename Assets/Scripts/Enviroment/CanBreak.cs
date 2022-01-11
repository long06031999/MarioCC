using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CanBreak : MonoBehaviour
{
  public int Stiffness = 2;
  public bool isBottom = true;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      bool isCollision = false;
      if (isBottom)
      {
        Debug.Log("normal y: " + other.contacts[0].normal.y);
        if (other.contacts[0].normal.y > 0)
        {
          isCollision = true;
        }
      }
      else
      {
        isCollision = true;
      }

      if (isCollision)
      {
        if (Stiffness <= 0)
        {
          other.gameObject.GetComponent<MarioController>().CreateAudio("smb_breakblock");
          Destroy(gameObject);
        }
        else
        {
          Stiffness -= 1;
        }
      }

    }
  }
}
