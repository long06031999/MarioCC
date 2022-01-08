using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemie : MonoBehaviour
{
    private Vector2 positionDie;
    public GameObject mushRoomDie;

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
    }
}
