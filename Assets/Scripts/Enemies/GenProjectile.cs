using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenProjectile : MonoBehaviour
{
  public GameObject projectile;

  public bool Auto = true;
  public float time = 3f;
  float timer;
  public Vector2 direction = Vector2.up;

  // Start is called before the first frame update
  void Start()
  {
    timer = time;
  }

  // Update is called once per frame
  void Update()
  {
    if (timer < 0)
    {
      timer = time;
      GameObject g = Instantiate(projectile, transform.position, Quaternion.identity);
      g.GetComponent<FlyingByDirection>().direction = direction;
    }
    else
    {
      timer -= Time.deltaTime;

    }
  }
}
