using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    private float _lowerBound = -8.5f;
    private float _upperBound = 7f;
    private Player _player;
    private Animator _animator;
        

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform.GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("player is null");
        }
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("animator is null");
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < _lowerBound)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, _upperBound, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit: " + other.transform.name);
        switch (other.transform.tag)
        {
            case "Player":
                Player player = other.transform.GetComponent<Player>();
                if (player != null)
                {
                    player.Damage();
                }
                _animator.SetTrigger("OnEnemyDeath");
                _speed = 0;
                Destroy(this.gameObject,2.5f);
                break;
            
            case "Laser":
                Destroy(this.gameObject,2.5f);
                if (_player != null)
                {
                    _player.AddScore(10);
                }
                _animator.SetTrigger("OnEnemyDeath");
                _speed = 0;
                Destroy(other.gameObject);
                break;
        }
    }
}
