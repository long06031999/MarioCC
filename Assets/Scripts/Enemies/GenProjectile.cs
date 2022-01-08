using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenProjectile : MonoBehaviour
{

  public GameObject projectile;

  public bool Auto = true;
  public float time = 3f;
  public float timer;

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
      Instantiate(projectile, transform.position, Quaternion.identity);
    }
    else
    {
      timer -= Time.deltaTime;

    }
  }
}
