using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemie : MonoBehaviour
{
    private Vector2 positionDie;
    public GameObject mushRoomDie;
    private GameObject mario;

    private void Awake()
    {
        mario = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        positionDie = transform.localPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.collider.tag == "Player" && collision.contacts[0].normal.y < 0)
        {
            Destroy(gameObject);
            Instantiate(mushRoomDie, transform.position, Quaternion.identity);
            mushRoomDie.transform.localPosition = positionDie;
        }
        else if(collision.collider.tag == "Player" && (collision.contacts[0].normal.x < 0 || collision.contacts[0].normal.x > 0))
        {
            if (mario.GetComponent<MarioController>().level == 0)
            {
                //Destroy(mario);
                Instantiate(mushRoomDie, transform.position, Quaternion.identity);
                mushRoomDie.transform.localPosition = positionDie;
                Destroy(gameObject);
            }
            else
            {
                mario.GetComponent<MarioController>().level -= 1;
                mario.GetComponent<MarioController>().isChangeMario = true;
                Destroy(gameObject);
            }
        }
    }
}
