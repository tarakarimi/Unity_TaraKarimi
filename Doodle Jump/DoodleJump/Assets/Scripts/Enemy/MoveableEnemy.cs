using System;
using UnityEngine;

public class MoveableEnemy : MonoBehaviour
{
    float moveSpeed = 1.1f;
    private int direction = 1;
    private GameManagerScript _gameManagerScript;
    float leftBoundary = -1f;
    float rightBoundary = 1f;
    [SerializeField] private Collider2D platformChildCollider;
    public LayerMask playerLayer;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private void Start()
    {
        _gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        _audioSource = GetComponent<AudioSource>();
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

        if (platformChildCollider != null && !platformChildCollider.IsTouchingLayers(playerLayer))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.GetComponent<Player>()._immune == false)
                {
                    collision.transform.GetChild(1).gameObject.SetActive(false);
                    Destroy(collision.collider);
                    _gameManagerScript.GameOverActions();
                    collision.gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    collision.gameObject.GetComponent<AudioSource>().Play();
                    Destroy(gameObject);
                }
            }
        } else if (platformChildCollider != null && platformChildCollider.IsTouchingLayers(playerLayer))
        {
            Debug.Log("getting destroyed");
            collision.gameObject.GetComponent<AudioSource>().Play();
            //AudioSource.PlayClipAtPoint(_audioClip, transform.position, 2);
            Destroy(gameObject);
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