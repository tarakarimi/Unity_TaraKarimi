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
    private bool _coolDown, _isGrounded;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite, _swordArcSprite;
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Movement();
        if (Input.GetMouseButtonDown(0) && isGrounded())
        {
            _playerAnim.Attack();
        }
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        if (move > 0)
        {
            Flip(true);
        }
        else if(move < 0)
        {
            Flip(false);
        }

        _isGrounded = isGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x,_jumpForce);
            _playerAnim.Jump(true);
            StartCoroutine(JumpCoolDown());
        }
        _rigidBody.velocity = new Vector2(move * _speed, _rigidBody.velocity.y);
        _playerAnim.Move(move);
    }

    bool isGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.85f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down * 0.85f, Color.green);
        if (hitInfo.collider != null)
        {
            if (_coolDown == false)
            {
                _playerAnim.Jump(false);
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

    void Flip(bool faceRight)
    {
        if (faceRight)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipY = false;
            _swordArcSprite.flipX = false;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1;
            _swordArcSprite.transform.localPosition = newPos;
        } else 
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipY = true;
            _swordArcSprite.flipX = true;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }
    
}
