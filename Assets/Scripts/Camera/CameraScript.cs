using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
  private GameObject player;
  public float minX = 0, maxX = 500;
  public float minY = 0, maxY = 255;

  public bool followPlayer = false;
  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    if (followPlayer)
    {
      if (player == null)
      {
        player = GameObject.FindWithTag("Player");
      }
      else
      {

        Vector3 position = transform.position;
        position.x = player.transform.position.x;
        position.y = player.transform.position.y;
        if (position.y < minY) position.y = minY;
        if (position.x < minX) position.x = minX;
        if (position.x >= maxX) position.x = maxX;
        transform.position = position;
      }
    }
  }
}
