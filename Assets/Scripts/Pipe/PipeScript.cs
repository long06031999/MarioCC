using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{

  public bool isOnPipe = false;
  MarioController marioController;
  public bool isHorizontal = false;
    private CameraScript cameraScript;
  // Start is called before the first frame update
  void Start()
  {
        cameraScript = GameObject.FindWithTag("MainCamera").GetComponent<CameraScript>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.contacts[0].normal.y < 0 && collision.gameObject.tag == "Player")
    {
      marioController = collision.gameObject.GetComponent<MarioController>();
      marioController.isOnPipe = true;
      marioController.downButton.SetActive(true);
      marioController.pipe = gameObject;
    }

    if (collision.contacts[0].normal.x > 0 && collision.gameObject.tag == "Player" && isHorizontal)
     {
            marioController = collision.gameObject.GetComponent<MarioController>();
            Action();
            cameraScript.minY = -2;
        }
    }

  private void OnCollisionExit2D(Collision2D collision)
  {
    if (marioController)
    {
      marioController.isOnPipe = false;
      marioController.downButton.SetActive(false);
      marioController.pipe = null;
      marioController = null;
    }
  }

  public bool OnPipe()
  {
    return isOnPipe;
  }

  public void Action()
  {
    marioController.transform.position = (Vector2)transform.GetChild(0).transform.position + new Vector2(0, 5);
        if(marioController.transform.position.y < -5)
        {
            cameraScript.minY = -17;
        }
  }
}
