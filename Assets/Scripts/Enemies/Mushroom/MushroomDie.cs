using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomDie : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimationMushroomDie());
    }

    IEnumerator AnimationMushroomDie()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
