using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{

    private float velocity;
    private bool isMoveLeft = true;

    void Start()
    {
        velocity = 2f;
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
        if(collision.collider.tag != "Player" && collision.contacts[0].normal.x > 0)
        {
            onDirection();
        }
        else if(collision.collider.tag != "Player" && collision.contacts[0].normal.x < 0)
        {
            onDirection();
        }
    }

    void onDirection()
    {
        isMoveLeft = !isMoveLeft;
        Vector2 direction = transform.localScale;
        direction.x *= -1;
        transform.localScale = direction;
    }
}
