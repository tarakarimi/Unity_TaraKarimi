using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 15f;
    private float distCameraToEdge = 5.28f;
    private Camera _camera;
    private Vector3 _direction;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * speed * Time.deltaTime;
        
        if (transform.position.y > _camera.transform.position.y + distCameraToEdge)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetDirection(Vector3 dir)
    {
        _direction = dir.normalized;
    }
}
