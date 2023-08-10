using System;
using UnityEngine;

public class MoveableEnemy : MonoBehaviour
{
    float moveSpeed = 1.1f; // Speed of movement
    private int direction = 1; // Initial direction
    private GameManagerScript _gameManagerScript;
    float leftBoundary = -1f; // Left movement boundary
    float rightBoundary = 1f; // Right movement boundary


    private void Start()
    {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Destroy the enemy when hit by a bullet
            Destroy(gameObject);
        }
        
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Player>()._immune == false)
            {
                collision.transform.GetChild(1).gameObject.SetActive(false);
                Destroy(collision.collider);
                _gameManagerScript.GameOverActions();
                Destroy(gameObject);
            }
        }
    }

    void Movement()
    {
        // Calculate the new X position based on the current direction and movement speed
        float newX = transform.position.x + moveSpeed * direction * Time.deltaTime;

        // Clamp the new X position within the left and right boundaries
        float clampedX = Mathf.Clamp(newX, leftBoundary, rightBoundary);

        // Update the enemy's position
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Check if the enemy has reached the left or right boundary
        if (transform.position.x >= rightBoundary || transform.position.x <= leftBoundary)
        {
            // Change direction
            direction *= -1;
        }
    }
}