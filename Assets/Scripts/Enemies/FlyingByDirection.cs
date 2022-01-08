using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyingByDirection : MonoBehaviour
{
  public float Speed = 5;
  public Vector2 direction = Vector2.up;
  public float distance = 10;
  public float dame = 10;
  //   new Rigidbody2D rigidbody2D;
  Vector2 startPos;

  private void Awake()
  {
    // rigidbody2D = GetComponent<Rigidbody2D>();
  }
  // Start is called before the first frame update
  void Start()
  {
    Debug.Log("Start");
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
}
