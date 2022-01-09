using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleDie : MonoBehaviour
{
    private float velocity = 15f;
    private bool isMoveLeft = true;
    private Vector2 direction;
    private bool isFastMove = false;

    private void Start()
    {
        direction = transform.position;
        InvokeRepeating("time", 0.2f, 0.2f);
    }


    void time()
    {
        if (transform.position.x == direction.x)
        {
            onDirection();
        }
        else
        {
            direction = transform.position;
        }
    }

    void onDirection()
    {
        isMoveLeft = !isMoveLeft;
        Vector2 direction = transform.localScale;
        direction.x *= -1;
        transform.localScale = direction;
    }


    private void FixedUpdate()
    {
        Vector2 position = transform.localPosition;
        if (isMoveLeft)
        {
            position.x -= velocity * Time.deltaTime;
        }
        else
        {
            position.x += velocity * Time.deltaTime;
        }
        transform.position = position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if(collision.contacts[0].normal.x > 0)
            {
                //direction = Vector2.right;
                isMoveLeft = false;
            }
            else
            {
                isMoveLeft = true;
            }
            if(isFastMove && collision.contacts[0].normal.y < 0) {
                Destroy(gameObject);
                return;
            }
            isFastMove = true;
        }
        if(isFastMove && collision.collider.tag == "Player")
        {
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
                Destroy(gameObject);
            }
        }
    }
}
