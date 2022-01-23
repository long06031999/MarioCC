using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
  public float Speed = 100;
  public Vector2 direction = Vector2.right;
  public float distance = 30;
  public int dame = 50;
  //   new Rigidbody2D rigidbody2D;
  Vector2 startPos;


   private MarioController controller;

  private void Awake()
  {
        // rigidbody2D = GetComponent<Rigidbody2D>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<MarioController>();
  }
  // Start is called before the first frame update
  void Start()
  {
    GetComponent<Rigidbody2D>().AddForce(direction * Speed);
    startPos = transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    if (((Vector2)transform.position - startPos).magnitude > distance)
    {
      Destroy(gameObject);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    EnemyHealthPoint enemyHealthPoint = collision.GetComponent<EnemyHealthPoint>();
    if (enemyHealthPoint)
    {
            switch (controller.CurrentLevel)
            {
                case MarioLevelEnum.Normal:
                    dame = 10;
                    break;
                case MarioLevelEnum.Big:
                    dame = 30;
                    break;
                default:
                    dame = 50;
                    break;
            }
      enemyHealthPoint.TakeDame(dame);
    }
    Destroy(gameObject);
  }
}
