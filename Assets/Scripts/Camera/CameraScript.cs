using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
  private Transform player;
  public float minX = 0, maxX = 500;
  public float minY = 0, maxY = 255;

  public bool followPlayer = true;
  // Start is called before the first frame update
  void Start()
  {
    player = GameObject.FindWithTag("Player").transform;
  }

  // Update is called once per frame
  void Update()
  {
    if (player != null && followPlayer)
    {
      Vector3 position = transform.position;
      position.x = player.position.x;
      position.y = player.position.y;
      if (position.y < minY) position.y = minY;
      if (position.x < minX) position.x = minX;
      if (position.x >= maxX) position.x = maxX;
      transform.position = position;
    }
  }
}
