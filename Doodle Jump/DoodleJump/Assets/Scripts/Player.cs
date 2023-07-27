using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private float horizontalMovement = 0f;
    private float movementSpeed = 5f;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;
    private SpriteRenderer spriteRenderer;

    private float horizontalBound = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = rightSprite;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed;
        FlipCharacterHandler();
        ReSpawnOnTheOtherSide();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMovement, rb.velocity.y) ;
    }

    void FlipCharacterHandler()
    {
        if (horizontalMovement < 0)
        {
            spriteRenderer.sprite = leftSprite;
        }
        else if (horizontalMovement > 0)
        {
            spriteRenderer.sprite = rightSprite;
        }
    }

    void ReSpawnOnTheOtherSide()
    {
        if (transform.position.x > horizontalBound)
        {
            transform.position = new Vector2(-horizontalBound,transform.position.y);
        } else if (transform.position.x < -horizontalBound)
        {
            transform.position = new Vector2(horizontalBound, transform.position.y);
        }
    }
}
