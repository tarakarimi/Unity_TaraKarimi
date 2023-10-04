using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private float _jumpForce = 5f;
    private bool _isGrounded, coolDown;
    [SerializeField] LayerMask _groundLayer;
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");
        _rigidBody.velocity = new Vector2(move, _rigidBody.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && !coolDown)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
            _isGrounded = false;
            coolDown = true;
            StartCoroutine(CoolDownCoroutine());
        }

        if (!coolDown)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.85f, _groundLayer.value); //1<<8
            Debug.DrawRay(transform.position,Vector2.down* 0.85f,Color.green);
        
            if (hitInfo.collider != null)
            {
                Debug.Log("Hit: "+hitInfo.collider.name);
                _isGrounded = true;
            }
        }
        
    }


    IEnumerator CoolDownCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        coolDown = false;
    }
}
