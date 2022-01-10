using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MoveTogether : MonoBehaviour
{

  BoxCollider2D boxCollider2D;

  private void Awake()
  {
    boxCollider2D = GetComponent<BoxCollider2D>();
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }


  private void OnCollisionStay2D(Collision2D other)
  {
    // Debug.Log(other.contacts[0].normal.y);
    if (other.contacts[0].normal.y < 0)
    {
      other.transform.position = new Vector2(gameObject.transform.position.x, other.transform.position.y);
    }
  }

}
