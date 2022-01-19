using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockItems : MonoBehaviour
{

  private float bounce = 0.5f;
  private float speed = 4f;
  public bool isItemNotEmpty = true;
  private Vector2 originalPosition;
  // public GameObject mario;
  public GameObject blockEmpty;
  public GameObject eMushroom;
  public GameObject weapon;
  // Start is called before the first frame update


  private void Awake()
  {
    // mario = GameObject.FindGameObjectWithTag("Player");
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider.tag == "Player" && collision.contacts[0].normal.y > 0)
    {
      originalPosition = transform.position;
      CreateItem(collision.gameObject);
    }
  }

  private void CreateItem(GameObject mario)
  {
    if (isItemNotEmpty)
    {
      isItemNotEmpty = false;
      StartCoroutine(AnimationCreateItem());
      Instantiate(blockEmpty, originalPosition, Quaternion.identity);
      if (mario.GetComponent<MarioController>().level == 0)
      {
        Vector2 positionOfItem = new Vector2(originalPosition.x, originalPosition.y + 1f);
        eMushroom = Instantiate(eMushroom, positionOfItem, Quaternion.identity);
        eMushroom.transform.SetParent(transform.parent);
      }
      else
      {
        Vector2 positionOfItem = new Vector2(transform.position.x, transform.position.y + 1f);
        weapon = Instantiate(weapon, positionOfItem, Quaternion.identity);
        weapon.transform.SetParent(transform.parent);
      }
    }
  }

  IEnumerator AnimationCreateItem()
  {
    while (true)
    {
      transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + speed * Time.deltaTime);
      if (transform.localPosition.y >= originalPosition.y + bounce)
      {
        break;
      }
      yield return null;
    }
    while (true)
    {
      transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speed * Time.deltaTime);
      if (transform.localPosition.y <= originalPosition.y)
      {
        break;
      }
      Destroy(gameObject);
      yield return null;
    }
  }
}
