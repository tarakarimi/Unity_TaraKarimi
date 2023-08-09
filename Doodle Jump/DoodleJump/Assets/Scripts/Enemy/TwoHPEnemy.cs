using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TwoHPEnemy : MonoBehaviour
{
    public Sprite damagedSprite; // Sprite to be shown after first shot
    private int health = 2; // Initial HP
    private GameManagerScript _gameManagerScript;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    private void Update()
    {
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
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.GetComponent<Collider>());
            _gameManagerScript.GameOverActions();
            Destroy(gameObject);
        }
    }
}