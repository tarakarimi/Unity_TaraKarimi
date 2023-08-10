using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringController : MonoBehaviour
{
    [SerializeField] private Sprite _spriteSprung;
    private float jumpForce = 15f; // Adjust the force strength
    private SpriteRenderer spriteRenderer;
    private bool hasSprung = false;

    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            collision.gameObject.GetComponent<Player>().JumpImmunity();
            hasSprung = true;
            spriteRenderer.sprite = _spriteSprung;

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                _audioSource.Play();   
            }
        }
    }
}
