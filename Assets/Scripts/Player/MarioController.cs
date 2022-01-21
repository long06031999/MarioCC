﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MarioController : MonoBehaviour
{
  [Header("Bullet")]
  public Text BulletTextUI;
  public Image ProgressImage;
  public int MaxBulletNumber = 5;
  public int bulletNumber = 5;
  public float countDown = 10f;
  private float countDownTimer = 10f;

  //==================================HEALTH=============================
  [Header("Health")]
  public Image UndeadStar;
  public Image CurrentHealthImage;
  public Text HealthTextDetail;
  private bool isUndead = false;
  [SerializeField]
  public bool IsUndead
  {
    set
    {
      isUndead = value;
      NotifyDataChanged();
    }
    get
    {
      return isUndead;
    }
  }
  private int health = 100;
  public int MaxHealth = 100;

  public int Health
  {
    get { return health; }
    set
    {
      if (value < health && IsUndead)
      {

      }
      else
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
          // float percent = (float)health / MaxHealth;
          // Debug.Log("Percent: " + percent);
          // CurrentHealthImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalImageSize * percent);
          // HealthTextDetail.text = Health + " / " + MaxHealth;
          NotifyDataChanged();
        }
      }

    }
  }

  //====================================ANDROID UI==================================

  [Header("Android UI")]
  public GameObject fireButton;
  public GameObject downButton;

  public GameObject pauseMenu;
  [Header("Time")]
  public Text timeTextUI;

  public double money;

  public float TotalTime;
  PlayerInputAction playerInputAction;

  public ParticleSystem particleSystem;
  int moveDirection = 0;
  public bool IsSlowDown
  {
    get
    {
      if (marioStatus.GetType() == typeof(SlowDownMarioSpeed))
      {
        return true;
      }
      else { return false; }
    }
    set
    {
      if (value == true)
      {
        marioStatus = new SlowDownMarioSpeed();
      }
      else
      {
        marioStatus = new NormalMarioSpeed();
      }
    }
  }
  private const float maxSpeedWhenHoldKey = 12;
  private const float checkTimeHoldKey = 0.02f;

  Image image;
  // float originalImageSize;


  public void HandleHealthPlayerWhenEatItem()
  {
    if (level < 2)
    {
      MaxHealth += 100;
    }
    Health += 100;

  }


  //default value setting
  // private float velocityWhenPress = 7;

  // private float velocityJump = 500;
  public MarioStatus marioStatus = new NormalMarioSpeed();
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
    // originalImageSize = image.rectTransform.rect.width;
    Debug.Log(image);

    animator = GetComponent<Animator>();
    r2d = GetComponent<Rigidbody2D>();
    audioSource = GetComponent<AudioSource>();

    // Init Player Input Action
    if (playerInputAction == null)
    {
      playerInputAction = new PlayerInputAction();
      playerInputAction.Enable();
    }

    playerInputAction.PlayerInputActions.MoveLeft.performed += MoveLeftPerformed;
    playerInputAction.PlayerInputActions.MoveLeft.canceled += MoveLeftCanceled;
    playerInputAction.PlayerInputActions.MoveRight.performed += MoveRightPerformed;
    playerInputAction.PlayerInputActions.MoveRight.canceled += MoveRightCanceled;
    playerInputAction.PlayerInputActions.Jump.performed += JumpPerformed;
    playerInputAction.PlayerInputActions.Jump.canceled += JumpCanceled;
    playerInputAction.PlayerInputActions.Teleport.performed += TeleportPerformed;
    playerInputAction.PlayerInputActions.Teleport.canceled += TeleportCanceled;
    playerInputAction.PlayerInputActions.Fire.performed += FirePerformed;
    playerInputAction.PlayerInputActions.Fire.canceled += FireCanceled;
    playerInputAction.PlayerInputActions.OpenPauseMenu.performed += OpenPauseMenuPerformed;

    if (GameManager.Instance.ReloadGame)
    {
      ReloadData();
    }
  }

  void SetTotalTime()
  {
    TotalTime += Time.deltaTime;
    timeTextUI.text = Mathf.Floor(TotalTime) + "s";
  }
  // Start is called before the first frame update
  void Start()
  {
    NotifyDataChanged();
  }

  public void MoveLeftPerformed(InputAction.CallbackContext context)
  {
    moveDirection = -1;
  }
  public void MoveLeftCanceled(InputAction.CallbackContext context)
  {
    moveDirection = 0;
  }

  public void MoveRightPerformed(InputAction.CallbackContext context)
  {
    moveDirection = 1;
  }
  public void MoveRightCanceled(InputAction.CallbackContext context)
  {
    moveDirection = 0;
  }
  public void JumpPerformed(InputAction.CallbackContext context)
  {
    OnJump();
  }

  public void SetUndeadDuration(float second)
  {
    StartCoroutine(UndeadToNormal(second));
  }

  IEnumerator UndeadToNormal(float duration)
  {
    while (IsUndead)
    {
      yield return null;
    }
    if (!IsUndead)
    {
      Debug.Log("Undead");
      IsUndead = true;

      float timer = duration;
      while (timer > 0)
      {
        timer -= Time.fixedDeltaTime;
        UndeadStar.fillAmount = timer / duration;
        yield return null;
      }
      Debug.Log("Expired");
      IsUndead = false;
    }


  }
  public void JumpCanceled(InputAction.CallbackContext context)
  {

  }
  public void FirePerformed(InputAction.CallbackContext context)
  {
    // timeHoldKey += Time.deltaTime;
    // if (level == 2 && timeHoldKey < checkTimeHoldKey)
    if (this && level == 2)
    {

      if (bulletNumber > 0)
      {
        bulletNumber--;
        NotifyDataChanged();

        if (!isSpawnBullet && bullet && this)
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
  }
  public void FireCanceled(InputAction.CallbackContext context)
  {
    marioStatus.velocityWhenPress = 7f;
    timeHoldKey = 0f;
    isSpawnBullet = false;
  }
  public void TeleportPerformed(InputAction.CallbackContext context)
  {
    if (this)
    {
      OnMoveToPipe();
    }
  }
  public void TeleportCanceled(InputAction.CallbackContext context)
  {

  }

  public void OpenPauseMenuPerformed(InputAction.CallbackContext context)
  {
    if (pauseMenu)
    {
      pauseMenu.SetActive(true);
      GameManager.Instance.PauseGame();
    }
  }
  void Die()
  {
    DestroyMario();
  }
  // Update is called once per frame
  void Update()
  {
    SetTotalTime();
    animator.SetFloat("velocity", velocity);
    animator.SetBool("isOnGround", isOnGround);
    animator.SetBool("isNavigation", isNavigation);
    // OnJump();
    ShootAndSpeed();
    // OnMoveToPipe();
    OnChangeMario();
    CheckMarioDie();
  }

  private void CheckMarioDie()
  {
    /*if (transform.localPosition.y <= -10f)
    {
      DestroyMario();
    }*/
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
            // fireButton.SetActive(false);
            // BulletTextUI.gameObject.SetActive(false);
            break;
          }
        case 1:
          {
            StartCoroutine(ChangeHighMario());
            this.isChangeMario = false;
            // fireButton.SetActive(false);
            // BulletTextUI.gameObject.SetActive(false);
            break;
          }
        case 2:
          {
            StartCoroutine(ChangeHighMarioWithGun());
            this.isChangeMario = false;
            // fireButton.SetActive(true);
            // BulletTextUI.gameObject.SetActive(true);
            break;
          }
        default:
          {
            this.isChangeMario = false;
            // fireButton.SetActive(false);
            break;
          }

      }
    }
  }

  private void FixedUpdate()
  {
    OnMove();
    ReloadBulletNumber();

    // CurrentHealthImage.color = Color.yellow;
  }

  void ReloadBulletNumber()
  {
    if (bulletNumber < MaxBulletNumber)
    {
      if (countDownTimer <= 0)
      {
        countDownTimer = countDown;

        bulletNumber += 1;
        NotifyDataChanged();
      }
      else
      {
        countDownTimer -= Time.fixedDeltaTime;
        ProgressImage.fillAmount = countDownTimer / countDown;
      }
    }
  }
  private void OnMove()
  {
    // float velocityKeyInput = Input.GetAxis("Horizontal");
    r2d.velocity = new Vector2(marioStatus.velocityWhenPress * moveDirection, r2d.velocity.y);
    velocity = Mathf.Abs(marioStatus.velocityWhenPress * moveDirection);
    if (moveDirection > 0 && !isRight) OnDirection();
    if (moveDirection < 0 && isRight) OnDirection();
  }

  private void OnDirection()
  {
    isRight = !isRight;
    gameObject.GetComponent<SpriteRenderer>().flipX = !isRight;
    if (marioStatus.velocityWhenPress > 1f) StartCoroutine(OnNavigation());
  }
  private void OnJump()
  {
    // //Test
    //Health -= 10;
    // //Test
    if (isOnGround)
    {
      if (r2d)
      {
        r2d.AddForce((Vector2.up) * marioStatus.velocityJump);
        isOnGround = false;
        if (level == 0) CreateAudio("smb_jump-small");
        else CreateAudio("smb_jump-super");

        if (r2d.velocity.y < 0)
        {
          r2d.velocity += Vector2.up * Physics2D.gravity.y * (velocityFall - 1) * Time.deltaTime;
        }
        else if (r2d.velocity.y > 0)
        {
          r2d.velocity += Vector2.up * Physics2D.gravity.y * (smallJump - 1) * Time.deltaTime;
        }
      }

    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Ground")
    {
      // Debug.Log("collision " + collision.contacts[0].normal.y);
      if (collision.contacts[0].normal.y >= 0.5)
        isOnGround = true;
    }
  }

  // private void OnCollisionStay2D(Collision2D other)
  // {
  //   if (other.gameObject.tag == "Ground" && isOnGround == false)
  //   {
  //     isOnGround = true;
  //   }
  // }

  // private void OnCollisionExit2D(Collision2D other)
  // {
  //   if (other.gameObject.tag == "Ground" && isOnGround == true)
  //   {
  //     isOnGround = false;
  //   }
  // }

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
    // if (Input.GetKeyDown(KeyCode.Z))
    // {
    //   // timeHoldKey += Time.deltaTime;
    //   // if (level == 2 && timeHoldKey < checkTimeHoldKey)
    //   // {
    //   //   if (!isSpawnBullet)
    //   //   {
    //   //     isSpawnBullet = true;
    //   //     Vector2 positionOfBullet;
    //   //     if (!gameObject.GetComponent<SpriteRenderer>().flipX)
    //   //       positionOfBullet = new Vector2(transform.position.x + 1f, transform.position.y);
    //   //     else
    //   //       positionOfBullet = new Vector2(transform.position.x - 1f, transform.position.y);
    //   //     GameObject g = Instantiate(bullet, positionOfBullet, Quaternion.identity);

    //   //     // because mario's default face direction is always right and default flipX = false,
    //   //     // so ! for exact direction
    //   //     if (!gameObject.GetComponent<SpriteRenderer>().flipX)
    //   //       g.GetComponent<BulletController>().direction = Vector2.right;
    //   //     else
    //   //       g.GetComponent<BulletController>().direction = Vector2.left;
    //   //     CreateAudio("smb_fireball");
    //   //   }
    //   // }
    // }
    // hold key z to move faster
    // if (Input.GetKey(KeyCode.Z))
    // {
    //   timeHoldKey += Time.deltaTime;
    //   if (timeHoldKey < checkTimeHoldKey)
    //   {

    //   }
    //   else
    //   {
    //     marioStatus.velocityWhenPress *= 1.01f;
    //     if (marioStatus.velocityWhenPress >= maxSpeedWhenHoldKey)
    //     {
    //       marioStatus.velocityWhenPress = maxSpeedWhenHoldKey;
    //     }
    //   }
    // }
    //reset value when press finsish
    // if (Input.GetKeyUp(KeyCode.Z))
    // {
    //   marioStatus.velocityWhenPress = 7f;
    //   timeHoldKey = 0f;
    //   isSpawnBullet = false;
    // }
  }

  private void OnMoveToPipe()
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

  public void ReloadData()
  {
    // GameManager.Instance.LoadSavedGame(this);
    // GameManager.Instance.ReloadGame = false;
  }

  public void NotifyDataChanged()
  {
    //====Set Undead========
    if (IsUndead)
    {
      CurrentHealthImage.color = Color.yellow;
      UndeadStar.gameObject.SetActive(true);
      particleSystem.Play();
    }
    else
    {
      CurrentHealthImage.color = Color.red;
      UndeadStar.gameObject.SetActive(false);
      particleSystem.Clear();
      particleSystem.Stop();

    }

    if (this && level < 2)
    {
      BulletTextUI.gameObject.SetActive(false);
      fireButton.SetActive(false);
      ProgressImage.gameObject.SetActive(false);
    }
    else
    {
      BulletTextUI.gameObject.SetActive(true);
      fireButton.SetActive(true);
      ProgressImage.gameObject.SetActive(true);
    }
    // Update Health
    float percent = (float)health / MaxHealth;
    // Debug.Log("Percent: " + percent);
    CurrentHealthImage.fillAmount = percent;
    HealthTextDetail.text = Health + " / " + MaxHealth;

    // Update Bullet
    BulletTextUI.text = bulletNumber.ToString();
  }

  public override bool Equals(object obj)
  {
    return obj is MarioController controller &&
           base.Equals(obj) &&
           EqualityComparer<ParticleSystem>.Default.Equals(particleSystem, controller.particleSystem);
  }
}

public abstract class MarioStatus
{
  public float velocityWhenPress;
  public float velocityJump;

  protected MarioStatus() { }

  protected MarioStatus(float moveSpeed, float jumpHeight)
  {
    velocityWhenPress = moveSpeed;
    velocityJump = jumpHeight;
  }
}

public class NormalMarioSpeed : MarioStatus
{
  public NormalMarioSpeed() : base(7, 500) { }
}

public class SlowDownMarioSpeed : MarioStatus
{
  public SlowDownMarioSpeed() : base(3, 400) { }
}
