using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TwoHPEnemy : MonoBehaviour
{
    public Sprite damagedSprite; // Sprite to be shown after first shot
    private int health = 2; // Initial HP
    private GameManagerScript _gameManagerScript;
    private SpriteRenderer spriteRenderer;
    float moveSpeed = 1.1f;
    private int direction = 2;
    float leftBoundary = -0.2f;
    float rightBoundary = 0.2f;
    [SerializeField] private Collider2D platformChildCollider;
    public LayerMask playerLayer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    private void Update()
    {
        Movement();
        float cameraBottomY = Camera.main.transform.position.y - Camera.main.orthographicSize;
        if (transform.position.y < cameraBottomY)
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;

            // Show damaged sprite after the first shot
            if (health == 1)
            {
                spriteRenderer.sprite = damagedSprite;
            }
            // Destroy the enemy after the second shot
            else if (health == 0)
            {
                Destroy(gameObject);
            }

            // Destroy the bullet
            Destroy(collision.gameObject);
        }

        if (platformChildCollider != null)
        {
            if (!platformChildCollider.IsTouchingLayers(playerLayer))
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    if (collision.gameObject.GetComponent<Player>()._immune == false)
                    {
                        collision.transform.GetChild(1).gameObject.SetActive(false);
                        Destroy(collision.GetComponent<Collider>());
                        _gameManagerScript.GameOverActions();
                        collision.gameObject.transform.GetChild(4).gameObject.SetActive(true);
                        collision.gameObject.GetComponent<AudioSource>().Play();
                        Destroy(gameObject);
                    }
                }
            } else if (platformChildCollider.IsTouchingLayers(playerLayer))
            {
                Destroy(gameObject);
            }
            
        }
    }
    void Movement()
    {
        float newX = transform.position.x + moveSpeed * direction * Time.deltaTime;
        float clampedX = Mathf.Clamp(newX, leftBoundary, rightBoundary);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        if (transform.position.x >= rightBoundary || transform.position.x <= leftBoundary)
        {
            direction *= -1;
        }
    }
}