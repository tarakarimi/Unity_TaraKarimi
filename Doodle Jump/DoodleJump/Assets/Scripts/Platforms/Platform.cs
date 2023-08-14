using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    public float jumpForce = 10f;
    private string currentScene;
    [SerializeField] private Sprite colorYellow;
    [SerializeField] private Sprite colorOrange;
    private AudioSource _audioSource;
    private void Start()
    {
        int randomIndex = Random.Range(0, 2);
        transform.GetComponent<SpriteRenderer>().sprite = (randomIndex == 0) ? colorYellow : colorOrange;
        currentScene = SceneManager.GetActiveScene().name;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float cameraBottomY = Camera.main.transform.position.y - Camera.main.orthographicSize;
        if (transform.position.y < cameraBottomY)
        {
            if (currentScene == "GameScene")
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.relativeVelocity.y <= 0f) 
        {
            Rigidbody2D rb = col.transform.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(0, jumpForce);
                if (_audioSource != null)
                {
                    _audioSource.Play();   
                }
            }
        }
    }

}
