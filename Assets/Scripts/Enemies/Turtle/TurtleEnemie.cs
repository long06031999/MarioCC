using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEnemie : MonoBehaviour
{
    private Vector2 positionDie;
    public GameObject turtleDie;
    private GameObject mario;
    private AudioSource audioSource;

    private void Awake()
    {
        mario = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        positionDie = transform.localPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && collision.contacts[0].normal.y < 0)
        {
            audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/smb_kick"));
            Instantiate(turtleDie, transform.position, Quaternion.identity);
            turtleDie.transform.localPosition = positionDie;
            Destroy(gameObject);
        }
        else if (collision.collider.tag == "Player" && (collision.contacts[0].normal.x < 0 || collision.contacts[0].normal.x > 0))
        {
            collision.gameObject.GetComponent<MarioController>().Health -= 40;
            Destroy(gameObject);
        }
    }
}
