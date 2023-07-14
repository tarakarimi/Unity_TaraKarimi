using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        //player starting position 
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //user inputs WASD
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        //player movement - optimized
        //transform.Translate(Vector3.right * _speed * horizontalInput  * Time.deltaTime);   //transform.Translate(new Vector3(1,0,0));
        //transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        
        //player bounds
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        } else if (transform.position.y <= -4.8f)
        {
            transform.position = new Vector3(transform.position.x, -4.8f, 0);
        }

        if (transform.position.x >= 17)
        {
            transform.position = new Vector3(-17, transform.position.y, 0);
        } else if (transform.position.x < -17)
        {
            transform.position = new Vector3(17, transform.position.y, 0);
        }
    }
}
