using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpringController : MonoBehaviour
{
    [SerializeField] private Sprite _spriteSprung;
    [SerializeField] private float jumpForce = 15f; // Adjust the force strength
    private SpriteRenderer spriteRenderer;
    private bool hasSprung;
    private Sprite _spriteIdle;
    private AudioSource _audioSource;

    private AudioClip _audioClip;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteIdle = spriteRenderer.sprite;
        _audioSource = GetComponent<AudioSource>();
        _audioClip = _audioSource.clip;
    }

    // Update is called once per frame
    void Update()
    {
        float cameraBottomY = Camera.main.transform.position.y - Camera.main.orthographicSize;
        if (transform.position.y < cameraBottomY)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!hasSprung && other.gameObject.CompareTag("Player"))
        {
            Player _player = other.gameObject.GetComponent<Player>();
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null && rb.velocity.y <= 0f)
            {
                hasSprung = true;
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = _spriteSprung;
                }

                if (rb != null)
                {
                    rb.velocity = new Vector2(0, jumpForce);
                    AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position);
                    
                    if (transform.CompareTag("Eye"))
                    {
                        _player.Immunity();
                        _player.TakeAFullTurn();
                        _player.ResetImmunity(1.7f);
                    }

                }

                StartCoroutine(Reset());
            }

        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.2f);
        hasSprung = false;
        spriteRenderer.sprite = _spriteIdle;
    }
}
