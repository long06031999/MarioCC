using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarioController : MonoBehaviour
{
  private const float maxSpeedWhenHoldKey = 12;
  private const float checkTimeHoldKey = 0.02f;

  private int health = 100;
  public int MaxHealth = 100;

  Image image;
  float originalImageSize;

  public int Health
  {
    get { return health; }
    set
    {
      if (value <= 0) Die();
      else
      {
        if (value < MaxHealth) health = MaxHealth;
        if (value > 0 && value <= MaxHealth)
        {
          health = value;
          if (health <= 100 && level != 0)
          {
            level = 0;
            isChangeMario = true;
            MaxHealth = 100;
          }
          else if (health > 100 && health <= 200 && level != 1)
          {
            level = 1;
            isChangeMario = true;
            MaxHealth = 200;
          }
          else if (health > 200 && health <= 300 && level != 2)
          {
            level = 2;
            isChangeMario = true;
            MaxHealth = 300;
          }
        }
        float percent = (float)health / MaxHealth;
        Debug.Log("Percent: " + percent);
        GetComponentInChildren<Image>().rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalImageSize * percent);
      }
    }
  }

  public void HandleHealthPlayerWhenEatItem()
  {
    if (level < 2)
    {
      MaxHealth += 100;
      Health = MaxHealth;
    }
    else
    {
      Health += 100;
    }
  }


  //default value setting
  private float velocityWhenPress = 7;

  private float velocityJump = 500;
  private float velocityFall = 5;
  private float smallJump = 5;

  private float timeHoldKey = 0;

  private float velocity = 0;
  private bool isOnGround = true;
  private bool isNavigation = false;
  private bool isRight = true;
  private int angle = 90;

  //component
  private Animator animator;
  private Rigidbody2D r2d;

  //show level mario
  public int level = 0;
  public bool isChangeMario = false;

  //bullet
  private bool isSpawnBullet = false;
  public GameObject bullet;

  //move to pipe
  public bool isOnPipe = false;
  public GameObject pipe;

  //mario die
  public GameObject marioDie;
  private Vector2 positionDie;

  //
  private AudioSource audioSource;

  private void Awake()
  {
    image = GetComponentInChildren<Image>();
    originalImageSize = image.rectTransform.rect.width;
    Debug.Log(image);
  }
  // Start is called before the first frame update
  void Start()
  {
    animator = GetComponent<Animator>();
    r2d = GetComponent<Rigidbody2D>();
    audioSource = GetComponent<AudioSource>();
  }

  void Die()
  {
    DestroyMario();
  }
  // Update is called once per frame
  void Update()
  {
    animator.SetFloat("velocity", velocity);
    animator.SetBool("isOnGround", isOnGround);
    animator.SetBool("isNavigation", isNavigation);
    OnJump();
    ShootAndSpeed();
    OnMoveToPipe();
    OnChangeMario();
    CheckMarioDie();
  }

  private void CheckMarioDie()
  {
    if (transform.localPosition.y <= -10f)
    {
      DestroyMario();
    }
  }

  private void OnChangeMario()
  {
    if (this.isChangeMario)
    {
      switch (level)
      {
        case 0:
          {
            StartCoroutine(ChangeSmallMario());
            this.isChangeMario = false;
            break;
          }
        case 1:
          {
            StartCoroutine(ChangeHighMario());
            this.isChangeMario = false;
            break;
          }
        case 2:
          {
            StartCoroutine(ChangeHighMarioWithGun());
            this.isChangeMario = false;
            break;
          }
        default:
          {
            this.isChangeMario = false;
            break;
          }

      }
    }
  }

  private void FixedUpdate()
  {
    OnMove();
  }
  private void OnMove()
  {
    float velocityKeyInput = Input.GetAxis("Horizontal");
    r2d.velocity = new Vector2(velocityWhenPress * velocityKeyInput, r2d.velocity.y);
    velocity = Mathf.Abs(velocityWhenPress * velocityKeyInput);
    if (velocityKeyInput > 0 && !isRight) OnDirection();
    if (velocityKeyInput < 0 && isRight) OnDirection();
  }

  private void OnDirection()
  {
    isRight = !isRight;
    gameObject.GetComponent<SpriteRenderer>().flipX = !isRight;
    if (velocityWhenPress > 1f) StartCoroutine(OnNavigation());
  }
  private void OnJump()
  {
    if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
    {
      // //Test
      //Health -= 10;
      // //Test
      r2d.AddForce((Vector2.up) * velocityJump);
      isOnGround = false;
      if (level == 0) CreateAudio("smb_jump-small");
      else CreateAudio("smb_jump-super");
    }
    if (r2d.velocity.y < 0)
    {
      r2d.velocity += Vector2.up * Physics2D.gravity.y * (velocityFall - 1) * Time.deltaTime;
    }
    else if (r2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
    {
      r2d.velocity += Vector2.up * Physics2D.gravity.y * (smallJump - 1) * Time.deltaTime;
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Ground")
    {
      isOnGround = true;
    }
  }

  IEnumerator OnNavigation()
  {
    isNavigation = true;
    yield return new WaitForSeconds(0.1f);
    isNavigation = false;
  }


  //bắn đạn và chạy nhanh.
  private void ShootAndSpeed()
  {
    // shoot
    if (Input.GetKeyDown(KeyCode.Z))
    {
      timeHoldKey += Time.deltaTime;
      if (level == 2 && timeHoldKey < checkTimeHoldKey)
      {
        if (!isSpawnBullet)
        {
          isSpawnBullet = true;
          Vector2 positionOfBullet;
          if (!gameObject.GetComponent<SpriteRenderer>().flipX)
            positionOfBullet = new Vector2(transform.position.x + 1f, transform.position.y);
          else
            positionOfBullet = new Vector2(transform.position.x - 1f, transform.position.y);
          GameObject g = Instantiate(bullet, positionOfBullet, Quaternion.identity);

          // because mario's default face direction is always right and default flipX = false,
          // so ! for exact direction
          if (!gameObject.GetComponent<SpriteRenderer>().flipX)
            g.GetComponent<BulletController>().direction = Vector2.right;
          else
            g.GetComponent<BulletController>().direction = Vector2.left;
          CreateAudio("smb_fireball");
        }
      }
    }
    // hold key z to move faster
    else if (Input.GetKey(KeyCode.Z))
    {
      timeHoldKey += Time.deltaTime;
      if (timeHoldKey < checkTimeHoldKey)
      {

      }
      else
      {
        velocityWhenPress *= 1.01f;
        if (velocityWhenPress >= maxSpeedWhenHoldKey)
        {
          velocityWhenPress = maxSpeedWhenHoldKey;
        }
      }
    }
    //reset value when press finsish
    if (Input.GetKeyUp(KeyCode.Z))
    {
      velocityWhenPress = 7f;
      timeHoldKey = 0f;
      isSpawnBullet = false;
    }
  }

  private void OnMoveToPipe()
  {
    if (Input.GetKeyDown(KeyCode.DownArrow))
    {
      if (isOnPipe)
      {
        Debug.Log(pipe.transform.GetChild(0).transform.position);
        pipe.GetComponent<PipeScript>().Action();

      }
      else
      {
        Debug.Log("Ground");
      }

    }
  }


  //thay đổi độ lớn của mario
  IEnumerator ChangeHighMario()
  {
    float delay = 0.1f;
    animator.SetLayerWeight(animator.GetLayerIndex("SmallMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMario"), 1);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMarioWithGun"), 0);
    yield return new WaitForSeconds(delay);

    animator.SetLayerWeight(animator.GetLayerIndex("SmallMario"), 1);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMarioWithGun"), 0);
    yield return new WaitForSeconds(delay);

    animator.SetLayerWeight(animator.GetLayerIndex("SmallMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMario"), 1);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMarioWithGun"), 0);
    yield return new WaitForSeconds(delay);

    animator.SetLayerWeight(animator.GetLayerIndex("SmallMario"), 1);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMarioWithGun"), 0);
    yield return new WaitForSeconds(delay);

    animator.SetLayerWeight(animator.GetLayerIndex("SmallMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMario"), 1);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMarioWithGun"), 0);
    yield return new WaitForSeconds(delay);
  }

  IEnumerator ChangeHighMarioWithGun()
  {
    float delay = 0.1f;
    animator.SetLayerWeight(animator.GetLayerIndex("SmallMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMarioWithGun"), 1);
    yield return new WaitForSeconds(delay);

    animator.SetLayerWeight(animator.GetLayerIndex("SmallMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMario"), 1);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMarioWithGun"), 0);
    yield return new WaitForSeconds(delay);

    animator.SetLayerWeight(animator.GetLayerIndex("SmallMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMarioWithGun"), 1);
    yield return new WaitForSeconds(delay);

    animator.SetLayerWeight(animator.GetLayerIndex("SmallMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMario"), 1);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMarioWithGun"), 0);
    yield return new WaitForSeconds(delay);

    animator.SetLayerWeight(animator.GetLayerIndex("SmallMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMarioWithGun"), 1);
    yield return new WaitForSeconds(delay);
  }

  IEnumerator ChangeSmallMario()
  {
    float delay = 0.1f;
    animator.SetLayerWeight(animator.GetLayerIndex("SmallMario"), 1);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMarioWithGun"), 0);
    yield return new WaitForSeconds(delay);

    animator.SetLayerWeight(animator.GetLayerIndex("SmallMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMario"), 1);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMarioWithGun"), 0);
    yield return new WaitForSeconds(delay);

    animator.SetLayerWeight(animator.GetLayerIndex("SmallMario"), 1);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMarioWithGun"), 0);
    yield return new WaitForSeconds(delay);

    animator.SetLayerWeight(animator.GetLayerIndex("SmallMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMario"), 1);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMarioWithGun"), 0);
    yield return new WaitForSeconds(delay);

    animator.SetLayerWeight(animator.GetLayerIndex("SmallMario"), 1);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMario"), 0);
    animator.SetLayerWeight(animator.GetLayerIndex("HighMarioWithGun"), 0);
    yield return new WaitForSeconds(delay);
  }


  public void DestroyMario()
  {
    positionDie = transform.localPosition;
    Instantiate(marioDie, positionDie, Quaternion.identity);
    Destroy(gameObject);
  }

  public void CreateAudio(String fileName)
  {
    audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/" + fileName));
  }
}

