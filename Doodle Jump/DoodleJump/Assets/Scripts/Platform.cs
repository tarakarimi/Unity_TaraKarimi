using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float jumpForce = 10f;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.relativeVelocity.y <= 0f) 
        {
            Rigidbody2D rb = col.transform.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
}
