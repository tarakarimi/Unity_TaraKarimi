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
    private float movementSpeed = 5f;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private Sprite shootSprite;
    private SpriteRenderer spriteRenderer;
    public GameObject bulletPrefab;
    private float shootCoolDownTime = 0;
    private bool shootMode = false;
    [SerializeField] private GameObject weapon;
    private float bullSpawnY = 2f;

    private float horizontalBound = 3.5f;

    private Camera camera;

    private GameManagerScript _gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = rightSprite;
        _gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed;
        
        FlipCharacterHandler();
        
        ReSpawnOnTheOtherSide();

        if (Input.GetKeyDown(KeyCode.Space) & Time.time > shootCoolDownTime)
        {
            ShootUpward();
        }
        
        if (Input.GetMouseButtonDown(0) & Time.time > shootCoolDownTime)
        {
            ShootInDirection();
        }

        GameOverFallCheck();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMovement, rb.velocity.y) ;
    }

    void FlipCharacterHandler()
    {
        if (shootMode == false)
        {
             if (horizontalMovement < 0)
             {
                 spriteRenderer.sprite = leftSprite;
             }
             else if (horizontalMovement > 0)
             {
                 spriteRenderer.sprite = rightSprite;
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
        shootMode = true;
        weapon.SetActive(true);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Debug.Log("angle: "+angle);
        
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

        weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        // Convert the angle back to a normalized direction vector
        direction = Quaternion.Euler(0, 0, angle) * Vector3.right;

        GameObject bullet = Instantiate(bulletPrefab, transform.position+ new Vector3(0,bullSpawnY,0), Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
        shootCoolDownTime = Time.time + 0.5f;
        StartCoroutine(ReturnSpriteToIdle());
    }

    void ShootUpward()
    {
        shootMode = true;
        weapon.SetActive(true);
        GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0,bullSpawnY,0), quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(Vector3.up);
        weapon.transform.rotation = Quaternion.Euler(0, 0, 90);
        shootCoolDownTime = Time.time + 0.5f;
        StartCoroutine(ReturnSpriteToIdle());
    }

    IEnumerator ReturnSpriteToIdle()
    {
        yield return new WaitForSeconds(0.5f);
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
            //Debug.Log("Game Over!");
            _gameManagerScript.isGameOver = true;
            //Destroy(this.gameObject);
        }
    }
    
    
}
