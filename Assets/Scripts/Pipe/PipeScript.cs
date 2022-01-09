using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{

    public bool isOnPipe = false;
    MarioController marioController;
    // Start is called before the first frame update
    void Start()
    {
        
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
            marioController.pipe = gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (marioController)
        {
            marioController.isOnPipe = false;
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
    }
}
