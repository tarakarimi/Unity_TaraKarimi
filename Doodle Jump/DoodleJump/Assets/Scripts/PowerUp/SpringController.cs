using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringController : MonoBehaviour
{
    [SerializeField] private Sprite _spriteSprung;
    [SerializeField] private float jumpForce = 15f; // Adjust the force strength
    private SpriteRenderer spriteRenderer;
    private bool hasSprung;
    private Sprite _spriteIdle;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteIdle = spriteRenderer.sprite;
        _audioSource = GetComponent<AudioSource>();
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
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasSprung && collision.gameObject.CompareTag("Player") && collision.relativeVelocity.y <= 0f)
        {
            Player _player = collision.gameObject.GetComponent<Player>();
            
            hasSprung = true;
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = _spriteSprung;
            }
            
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                if (transform.CompareTag("Eye"))
                {
                    _player.Immunity();
                    _player.TakeAFullTurn();
                    _player.ResetImmunity(1.7f);
                }
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                _audioSource.Play();   
            }

            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.2f);
        hasSprung = false;
        spriteRenderer.sprite = _spriteIdle;
    }
}
