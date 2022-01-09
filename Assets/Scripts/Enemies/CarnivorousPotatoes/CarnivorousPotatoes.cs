using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivorousPotatoes : MonoBehaviour
{

    public float bounce = 10f;
    public float speed = 10f;
    private Vector2 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        //InvokeRepeating("StartAnimate", 0.2f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        StartAnimate();
    }

    void StartAnimate()
    {
        StartCoroutine(AnimationCarnivorousPotatoes());
    }

    IEnumerator AnimationCarnivorousPotatoes()
    {
        Debug.Log("original y: "+ originalPosition.y);
        while (true)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
            Debug.Log("y" + originalPosition.y);
            Debug.Log("bounce: " + bounce);
            Debug.Log("limit: " + (originalPosition.y - bounce));
            if (transform.position.y <= originalPosition.y - bounce)
            {

                Debug.Log("Break");
                break;
            }
            yield return new WaitForSeconds(2f);
        }

        
        while (true)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            if (transform.position.y >= originalPosition.y)
            {
                break;
            }
            yield return new WaitForSeconds(2f);
        }

        
    }
}
