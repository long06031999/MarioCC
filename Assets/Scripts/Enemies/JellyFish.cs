using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(DameArea))]
[RequireComponent(typeof(Rigidbody2D))]
public class JellyFish : MonoBehaviour
{
  public Sprite[] Sprites;
  MarioController marioController;
  [SerializeField] float _finderTimer = 1f;
  [SerializeField] float _moveTimer = 1f;
  [SerializeField] float _MoveTime = 1f;
  [SerializeField] float _distance;

  bool OnMove = false;
  Vector2 _vectorDistance;

  [SerializeField] bool _isInWater = true;

  public void SetInWater(bool IsInWater)
  {
    _isInWater = IsInWater;
    if (_isInWater)
    {
      GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    else
    {
      GetComponent<Rigidbody2D>().gravityScale = 10;
    }
  }

  private void Update()
  {
    if (!marioController)
    {
      if (_finderTimer <= 0)
      {
        FindMario();
        _finderTimer = 1f;
      }
      else
      {
        _finderTimer -= Time.deltaTime;
      }
    }
    else
    {
      if (!OnMove)
      {
        if (_moveTimer <= 0)
        {
          _moveTimer = _MoveTime;

          StartCoroutine(GoingToMario());
        }
        else
        {
          _moveTimer -= Time.deltaTime;
        }
      }

    }
  }

  float CaculateDistance(Vector2 target)
  {
    _vectorDistance = target - (Vector2)transform.position;
    return Mathf.Sqrt(_vectorDistance.x * _vectorDistance.x + _vectorDistance.y * _vectorDistance.y);
  }
  void FindMario()
  {
    marioController = GameObject.FindObjectOfType<MarioController>();
    if (marioController)
    {

      float CurrentDistance = CaculateDistance(marioController.transform.position);
      if (CurrentDistance > _distance)
      {
        marioController = null;

      }
    }
  }

  IEnumerator GoingToMario()
  {
    if (Sprites.Length < 2)
    {
      yield break;
    }
    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
    renderer.sprite = Sprites[0];
    OnMove = true;

    Vector2 VectorDistance = (Vector2)marioController.transform.position - (Vector2)transform.position;
    float rotationDelay = 1f;
    float rotationDelayTimer = 1f;
    float targetAngle = Mathf.Atan2(VectorDistance.y, VectorDistance.x) * Mathf.Rad2Deg - 90;

    while (rotationDelayTimer >= 0)
    {
      rotationDelayTimer -= Time.deltaTime;
      //   Vector3 dir = marioController.transform.position - transform.position;
      float angle = targetAngle * (1 - rotationDelayTimer / rotationDelay);
      //   float angle = targetAngle;
      transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
      yield return null;
    }


    if (marioController && (CaculateDistance(marioController.transform.position) <= _distance))
    {
      float moveDelay = 2.5f;
      float moveDelayTimer = moveDelay;
      Vector2 targetPosition = marioController.transform.position;
      Debug.Log(marioController.transform.position + " / " + targetPosition);
      Vector2 currentPosition = transform.position;
      Vector2 distanceVector = (targetPosition - (Vector2)currentPosition) * 1.5f;
      renderer.sprite = Sprites[1];

      while (moveDelayTimer >= 0)
      {
        moveDelayTimer -= Time.deltaTime;
        GetComponent<Rigidbody2D>().MovePosition(currentPosition + distanceVector * (1 - moveDelayTimer / moveDelay));
        yield return null;
      }

    }
    else
    {
      marioController = null;
    }

    OnMove = false;
    renderer.sprite = Sprites[0];
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Ground")
    {
      GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 0));
    }
  }
}
