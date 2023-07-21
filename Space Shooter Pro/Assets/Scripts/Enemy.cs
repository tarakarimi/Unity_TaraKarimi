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
                Destroy(this.gameObject);
                break;
            
            case "Laser":
                Destroy(this.gameObject);
                Destroy(other.gameObject);
                break;
        }
    }
}
