using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    private bool goRight;
    private float boundLimit = 2.6f;
    private float _speed = 1f;
    public float jumpForce = 10f;
    private AudioSource _audioSource;
    private void Start()
    {
        goRight = (Random.value > 0.5f); //0: left   1: right
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (goRight)
        {
            if (transform.position.x < boundLimit)
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            }
            else
            {
                goRight = false;
            }
        }
        else
        {
            if (transform.position.x > -boundLimit)
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }
            else
            {
                goRight = true;
            }
        }
        float cameraBottomY = Camera.main.transform.position.y - Camera.main.orthographicSize;
        if (transform.position.y < cameraBottomY)
        {
            Destroy(this.gameObject);
        }
        
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.relativeVelocity.y <= 0f) 
        {
            Rigidbody2D rb = col.transform.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                _audioSource.Play();
            }
        }
    }
}