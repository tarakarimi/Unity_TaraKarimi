using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private float _jumpForce = 5.5f;
    [SerializeField] private float _speed = 2f;
    //[SerializeField] LayerMask _groundLayer;
    private bool _coolDown;
    private PlayerAnimation _anim;
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        Movement();

    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x,_jumpForce);
            StartCoroutine(JumpCoolDown());
        }
        _rigidBody.velocity = new Vector2(move * _speed, _rigidBody.velocity.y);
        _anim.Move(move);
    }

    bool isGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.85f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down * 0.85f, Color.green);
        if (hitInfo.collider != null)
        {
            if (_coolDown == false)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator JumpCoolDown()
    {
        _coolDown = true;
        yield return new WaitForSeconds(0.1f);
        _coolDown = false;
    }
}
