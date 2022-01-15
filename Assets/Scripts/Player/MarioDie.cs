using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MarioDie : MonoBehaviour
{
  // Start is called before the first frame update

  float speed = 15f;
  float bounce = 50f;
  // Update is called once per frame


  void Update()
  {
    StartCoroutine(AnimationMarioDie());
  }

  IEnumerator AnimationMarioDie()
  {

    while (true)
    {
      transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + speed * Time.deltaTime);
      if (transform.localPosition.y >= bounce + 1)
      {
        break;
      }
      yield return null;
      transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speed * Time.deltaTime);
      if (transform.localPosition.y <= -30f)
      {
        Destroy(gameObject);
        break;
      }
      yield return null;
    }

    SceneManager.LoadScene("DieMenu", LoadSceneMode.Additive);
  }

}
