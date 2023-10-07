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
    [SerializeField] private AudioClip _audioClip;
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
        
        if (platformChildCollider.IsTouchingLayers(playerLayer))
        {
            //_doodlerAudioSource.enabled = true;
            //_doodlerAudioSource.gameObject.GetComponent<AudioSource>().Play();
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
                Debug.Log("collide with player");
                if (collision.gameObject.GetComponent<Player>()._immune == false)
                {
                    collision.transform.GetChild(1).gameObject.SetActive(false);
                    Destroy(collision);
                    _gameManagerScript.GameOverActions();
                    collision.gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    collision.gameObject.GetComponent<AudioSource>().Play();
                    Destroy(this.gameObject);
                }
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