using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
  public float timer = 60f;

  GameObject mario;
  public GameObject TilemapBlock;
  bool isCombatWithBoss = false;


  public float FirstTile = 1f;
  public GameObject FirstTileGameObject;
  public float SecondTile = 1.5f;
  public GameObject SecondTileGameObject;
  public float ThirdTile = 0.5f;
  public GameObject ThirdTileGameObject;

  public Vector2 playerDirection;
  // Start is called before the first frame update
  void Start()
  {
    // mario = GameObject.FindGameObjectWithTag("Player").GetComponent<mario>();
  }

  // Update is called once per frame
  void Update()
  {
    if (mario)
    {
      if (((mario.transform.position.x > 4.3 && mario.transform.position.y < 2) || mario.transform.position.x > 4.5) && !isCombatWithBoss)
      {
        isCombatWithBoss = true;
        CombatWithBoss();
      }
      if (isCombatWithBoss)
      {
        playerDirection = mario.transform.position - gameObject.transform.position;
        if (timer % FirstTile <= Time.deltaTime)
        {
          FirstProjectile();
        }
        if (timer % SecondTile <= Time.deltaTime)
        {
          SecondProjectile();
        }
        if (timer % ThirdTile <= Time.deltaTime)
        {
          ThirdProjectile();
        }
        timer -= Time.deltaTime;
      }
      if (timer < 0)
      {
        WinGame();

      }
    }
    else
    {
      mario = GameObject.FindGameObjectWithTag("Player");
    }

  }

  void CombatWithBoss()
  {
    TilemapBlock.SetActive(true);
    CameraScript cameraScript = GameObject.FindObjectOfType<CameraScript>();
    cameraScript.followPlayer = false;
    cameraScript.transform.position = new Vector3(15.64f, 5.49f, -10);
  }

  void WinGame()
  {
    Debug.Log("Win Game");
    GameObject.FindObjectOfType<CameraScript>().followPlayer = true;
    TilemapBlock.SetActive(false);
    Destroy(gameObject);
  }

  void FirstProjectile()
  {

    GameObject g = Instantiate(FirstTileGameObject, transform.position, Quaternion.identity);
    g.GetComponent<FlyingByDirection>().direction = playerDirection;
    // Debug.Log("first: " + playerDirection);
  }
  void SecondProjectile()
  {
    GameObject g = Instantiate(SecondTileGameObject, transform.position, Quaternion.identity);
    GameObject gl = Instantiate(SecondTileGameObject, transform.position, Quaternion.identity);
    GameObject gr = Instantiate(SecondTileGameObject, transform.position, Quaternion.identity);
    gl.transform.Rotate(new Vector3(0, 0, 90));
    gr.transform.Rotate(new Vector3(0, 0, -90));
    gl.GetComponent<FlyingByDirection>().direction = Vector2.left;
    gr.GetComponent<FlyingByDirection>().direction = Vector2.right;

  }
  void ThirdProjectile()
  {
    GameObject gl = Instantiate(ThirdTileGameObject, transform.position, Quaternion.identity);
    GameObject gr = Instantiate(ThirdTileGameObject, transform.position, Quaternion.identity);
    gl.GetComponent<ParapolFlying>().direction = new Vector2(8, 16);
    gr.GetComponent<ParapolFlying>().direction = new Vector2(-8, 16);
  }
}
