using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DameArea : MonoBehaviour
{
    // Start is called before the first frame updateprivate GameObject mario;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            MarioController marioController = collision.GetComponent<MarioController>();
            if (marioController)
            {
                if (marioController.level == 0)
                {
                    Destroy(collision.gameObject);
                } else
                {
                    marioController.level -= 1;
                    marioController.isChangeMario = true;
                }
            }
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            MarioController marioController = collision.gameObject.GetComponent<MarioController>();
            if (marioController)
            {
                if (marioController.level == 0)
                {
                    Destroy(collision.gameObject);
                }
                else
                {
                    marioController.level -= 1;
                    marioController.isChangeMario = true;
                }
            }
        }
    }
}
