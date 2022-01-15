using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Endgame : MonoBehaviour
{

  public GameObject EndingMenu;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    Time.timeScale = 0f;
    EndingMenu.SetActive(true);
  }
}
