using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform player;
    private float minX = 0, maxX=204;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 position = transform.position;
            position.x = player.position.x;
            if (position.x < minX) position.x = 0;
            if (position.x >= maxX) position.x = maxX;
            transform.position = position;
        }
    }
}
