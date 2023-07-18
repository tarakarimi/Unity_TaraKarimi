using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _laserPrefab;
    private float horizontalBound = 11.3f;
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _fireRate = 0.2f;
    private float _canFire = -1;
    void Start()
    {
        //player starting position 
        transform.position = new Vector3(0, 0, 0);
    }
    
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        //user inputs WASD
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        //player movement
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        
        //player bounds
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.8f, 0), 0);

        if (transform.position.x >= horizontalBound)
        {
            transform.position = new Vector3(-horizontalBound, transform.position.y, 0);
        } else if (transform.position.x < -horizontalBound)
        {
            transform.position = new Vector3(horizontalBound, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
    }
}