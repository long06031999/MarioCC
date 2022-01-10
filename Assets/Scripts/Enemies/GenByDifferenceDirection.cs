using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GenProjectile))]
[RequireComponent(typeof(Flying))]
public class GenByDifferenceDirection : MonoBehaviour
{

  GenProjectile genProjectile;
  Flying flying;

  private void Awake()
  {
    genProjectile = GetComponent<GenProjectile>();
    flying = GetComponent<Flying>();
  }
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (flying.IsBehind)
    {
      genProjectile.direction = new Vector2(-1, -1);
    }
    else
    {
      genProjectile.direction = new Vector2(1, -1);
    }
  }
}
