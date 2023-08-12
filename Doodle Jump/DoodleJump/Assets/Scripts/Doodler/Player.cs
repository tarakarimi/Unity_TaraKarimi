using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private float horizontalMovement = 0f;
    private float movementSpeed = 7f;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private Sprite shootSprite;
    private SpriteRenderer spriteRenderer;
    public GameObject bulletPrefab;
    private float shootCoolDownTime = 0;
    private bool shootMode = false;
    [SerializeField] private GameObject weapon, circle;
    private float horizontalBound = 3.5f;
    private Camera _camera;
    private GameManagerScript _gameManagerScript;
    public bool _immune = false;
    private BoxCollider2D playerCollider;
    private AudioSource _audioSource;
    private float jumpImmuneTimer;
    private float rotationSpeed = 1f;
    private bool useGyroscope; // Set this to false for touch controls
    private Vector3 initialGyroRotation;
    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = transform.GetComponent<Rigidbody2D>();
        if (Application.platform == RuntimePlatform.Android)
        {
            useGyroscope = true;
        }
        if (useGyroscope)
        {
            // Enable the gyroscope
            if (SystemInfo.supportsGyroscope)
            {
                Input.gyro.enabled = true;
                initialGyroRotation = Input.gyro.attitude.eulerAngles;
            }
            else
            {
                useGyroscope = false;
            }
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = rightSprite;
        _gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        _audioSource = GetComponent<AudioSource>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 70);
        if (_gameManagerScript.isGameOver == false)
        {
            MovementHandler();
            GameOverFallCheck();
        }

        if (_gameManagerScript.isGameOver)
        {
            if (playerCollider != null)
            {
                playerCollider.enabled = false;
            }
        }

    }

    private void MovementHandler()
    {
        if (useGyroscope)
        {
            horizontalMovement = 0;
            //horizontalMovement = -Input.gyro.rotationRateUnbiased.z * movementSpeed * 2;
            horizontalMovement = Input.acceleration.x * movementSpeed * 3f;
        }
        else
        {
            horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed;
        }
        
        FlipCharacterHandler();
        
        ReSpawnOnTheOtherSide();

        if (Input.GetKeyDown(KeyCode.Space) & Time.time > shootCoolDownTime)
        {
            if (!transform.GetChild(3).gameObject.activeSelf)
            {
                ShootUpward();
            }
            
        }
        
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && Time.time > shootCoolDownTime)
        {
            if (!transform.GetChild(3).gameObject.activeSelf)
            {
                ShootInDirection();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMovement, rb.velocity.y) ;
    }

    void FlipCharacterHandler()
    {
        if (shootMode == false)
        {
            spriteRenderer.sprite = rightSprite;
             if (horizontalMovement < 0)
             {
                 //spriteRenderer.sprite = leftSprite;
                 Vector3 newScale = transform.localScale;
                 newScale.x = -1f;
                 transform.localScale = newScale;
             }
             else if (horizontalMovement > 0)
             {
                 
                 Vector3 newScale = transform.localScale;
                 newScale.x = 1f;
                 transform.localScale = newScale;
             }
        }
        else
        {
            spriteRenderer.sprite = shootSprite;
        }
    }

    void ReSpawnOnTheOtherSide()
    {
        if (transform.position.x > horizontalBound)
        {
            transform.position = new Vector2(-horizontalBound,transform.position.y);
        } else if (transform.position.x < -horizontalBound)
        {
            transform.position = new Vector2(horizontalBound, transform.position.y);
        }
    }

    void ShootInDirection()
    {
        if (_gameManagerScript.isGameOver == false)
        {
            shootMode = true;
            weapon.SetActive(true);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (mousePos - transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (!(angle > 45f && angle < 135f))
            {
                if ((angle > 0 & angle < 45)||(angle < 0 & angle>-90))
                {
                    angle = 45;
                } else
                {
                    angle = 135;
                }
            }

            if (transform.localScale.x == 1)
            {
                weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                weapon.transform.rotation = Quaternion.Euler(0, 0, angle+180);
            }

            
            // Convert the angle back to a normalized direction vector
            direction = Quaternion.Euler(0, 0, angle) * Vector3.right;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetDirection(direction);
            shootCoolDownTime = Time.time + 0.4f;
            _audioSource.Play();
            StartCoroutine(ReturnSpriteToIdle());
        }
    }

    void ShootUpward()
    {
        if (_gameManagerScript.isGameOver == false)
        {
            shootMode = true;
            weapon.SetActive(true);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, quaternion.identity);
            bullet.GetComponent<Bullet>().SetDirection(Vector3.up);
            weapon.transform.rotation = Quaternion.Euler(0, 0, 90);
            shootCoolDownTime = Time.time + 0.4f;
            _audioSource.Play();
            StartCoroutine(ReturnSpriteToIdle());
        }
    }

    IEnumerator ReturnSpriteToIdle()
    {
        yield return new WaitForSeconds(0.4f);
        shootMode = false;
        weapon.SetActive(false);
        if (horizontalMovement == 0)
        {
            spriteRenderer.sprite = rightSprite;
        }
        else
        {
            FlipCharacterHandler();
        }
    }

    public void GameOverFallCheck()
    {
        float cameraBottomY = Camera.main.transform.position.y - Camera.main.orthographicSize;
        if (transform.position.y < cameraBottomY)
        {
            circle.SetActive(false);
            if (_gameManagerScript.isGameOver == false)
            {
                _gameManagerScript.GameOverActions();
            }
        }
    }


    public void ResetImmunity(float duration)
    {
        StartCoroutine(StartResetingImmunity(duration));
    }


    IEnumerator StartResetingImmunity(float duration)
    {
        jumpImmuneTimer = 0f;
        while (jumpImmuneTimer < duration)
        {
            jumpImmuneTimer += Time.deltaTime;
            yield return null;
        }
    
        jumpImmuneTimer = 0f;
        _immune = false;
        if (!_gameManagerScript.isGameOver)
        {
            playerCollider.enabled = true;
        }
    }
    public void Immunity()
    {
        _immune = true;
        playerCollider.enabled = false;
    }

    public void TakeAFullTurn()
    {
        StartCoroutine(RotatePlayer());
    }

    private IEnumerator RotatePlayer()
    {
        float startRotation = transform.rotation.eulerAngles.z;
        float targetRotation = transform.eulerAngles.z + 360f;
        float elapsedTime = 0f;
    
        while (elapsedTime < 1f)
        {
            float currentRotation = Mathf.Lerp(startRotation, targetRotation, elapsedTime);
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
        
            elapsedTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, targetRotation);
        //jumpImmuneDuration = 1f;
    }

}
