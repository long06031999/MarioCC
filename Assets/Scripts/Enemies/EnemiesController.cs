using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    private float velocity;
    private bool isMoveLeft = true;
    private Vector2 direction;
 

    void Start()
    {
        velocity = 2f;
        direction = transform.position;
        InvokeRepeating("time", 0.2f, 0.2f);
    }

    void time()
    {
        if(transform.position.x == direction.x)
        {
            onDirection();
        }else
        {
            direction = transform.position;
        }
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

    void onDirection()
    {
        isMoveLeft = !isMoveLeft;
        Vector2 direction = transform.localScale;
        direction.x *= -1;
        transform.localScale = direction;
    }
}
