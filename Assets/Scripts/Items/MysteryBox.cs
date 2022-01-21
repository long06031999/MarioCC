using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MysteryBox : MonoBehaviour
{
  string _AudioString = "smb_coin";
  public GameObject[] MysteryItems;
  public Sprite EmptySprite;
  public int CountGetMysteryItem = 1;
  public float timer = 1f;
  bool _canGenerate = true;

  private void FixedUpdate()
  {
    if (!_canGenerate)
    {
      timer -= Time.fixedDeltaTime;
      if (timer <= 0)
      {
        _canGenerate = true;
      }
    }
  }
  private void OnCollisionEnter2D(Collision2D other)
  {
    // Debug.Log("Colision");
    if (other.contacts[0].normal.y > 0.5)
    {
      MarioController controller = other.gameObject.GetComponent<MarioController>();
      if (controller && CountGetMysteryItem > 0 && _canGenerate)
      {
        _canGenerate = false;
        CountGetMysteryItem--;
        StartCoroutine(GenerateMysteryItem());
        controller.CreateAudio(_AudioString);
      }
      if (CountGetMysteryItem == 0)
      {

        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = EmptySprite;
      }
    }
  }

  IEnumerator GenerateMysteryItem()
  {
    Vector2 currentPosition = transform.position;
    float distance = 0.2f;
    // float destinationY = currentPosition.y + 1;
    float delay = 0.2f;
    float delayTimer = delay;

    while (delayTimer > 0)
    {
      float percent = 1 - delayTimer / delay;
      Debug.Log(percent);
      transform.position = new Vector2(currentPosition.x, currentPosition.y + percent * distance);
      delayTimer -= Time.fixedDeltaTime;
      yield return null;
    }


    if (MysteryItems.Length < 1) { }
    else if (MysteryItems.Length == 1)
    {
      GameObject g = Instantiate(MysteryItems[0], new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
      g.transform.parent = transform;
      g.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200);
    }
    else
    {
      int number = Random.Range(0, MysteryItems.Length);
      GameObject g = Instantiate(MysteryItems[number], new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
      g.transform.parent = transform;

      g.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200);
    }

    delayTimer = delay;
    while (delayTimer > 0)
    {
      float percent = delayTimer / delay;
      transform.position = new Vector2(currentPosition.x, currentPosition.y + percent * distance);
      delayTimer -= Time.fixedDeltaTime;
      yield return null;
    }
  }
}
