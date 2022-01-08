using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{

  public float FlyingTime = 2f;
  private float timer;
  public int Speed;
  public bool IsHorizontal = true;
  public bool IsBehind = true;

  // Start is called before the first frame update
  void Start()
  {
    timer = FlyingTime;
  }

  // Update is called once per frame
  void Update()
  {
    if (timer < 0)
    {
      timer = FlyingTime;
      ChangeDirection();
    }
    else
    {
      timer -= Time.deltaTime;
      MovePosition();

    }
  }

  void ChangeDirection()
  {
    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    IsBehind = !IsBehind;
  }

  void MovePosition()
  {
    transform.position += (Vector3)GetDirection() * Speed * Time.deltaTime;
  }

  Vector2 GetDirection()
  {
    if (IsBehind && IsHorizontal) return Vector2.left;
    if (!IsBehind && IsHorizontal) return Vector2.right;
    if (IsBehind && !IsHorizontal) return Vector2.up;
    return Vector2.down;
  }
}
