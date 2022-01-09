using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownArea : MonoBehaviour
{
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
    MarioController controller = other.gameObject.GetComponent<MarioController>();

    if (controller)
    {
      Debug.Log("Enter");
    }
  }

  private void OnCollisionStay2D(Collision2D other)
  {
    MarioController controller = other.gameObject.GetComponent<MarioController>();

    if (controller)
    {
      Debug.Log("Stay");
    }
  }

  private void OnCollisionExit2D(Collision2D other)
  {
    MarioController controller = other.gameObject.GetComponent<MarioController>();

    if (controller)
    {
      Debug.Log("Exit");
    }
  }
}
