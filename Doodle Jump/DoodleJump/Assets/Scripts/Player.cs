using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private float horizontalMovement = 0f;
    private float movementSpeed = 5f;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;
    private SpriteRenderer spriteRenderer;
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
}
