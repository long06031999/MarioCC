using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ParapolFlying : MonoBehaviour
{

  private Rigidbody2D rigidbody2D;
  public Vector2 direction = Vector2.up;
  public float Speed = 15;
  float timer = 5f;

  private void Awake()
  {
    rigidbody2D = GetComponent<Rigidbody2D>();
  }
  // Start is called before the first frame update
  void Start()
  {
    rigidbody2D.AddForce(direction * Speed);
  }

  // Update is called once per frame
  void Update()
  {
    if (timer <= 0)
    {
      Destroy(gameObject);
    }
    else
    {
      timer -= Time.deltaTime;
      transform.Rotate(new Vector3(0, 0, 15));
    }
  }
}
