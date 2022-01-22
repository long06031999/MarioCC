using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpItem : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;
    private bool isHasPlayer = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isHasPlayer", false);
        InvokeRepeating("time", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void time()
    {
        if (isHasPlayer)
        {
            animator.SetBool("isHasPlayer", false);
            isHasPlayer = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();

        if (collision.contacts[0].normal.y <0 && collision.collider.tag == "Player")
        {
            animator.SetBool("isHasPlayer", true);
            rigidbody2D.AddForce((Vector2.up) * 900);
            isHasPlayer = true;
        }
    }
}
