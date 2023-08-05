using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    private bool goRight;
    private float boundLimit = 2.6f;
    private float _speed = 1f;
    private void Start()
    {
        goRight = (Random.value > 0.5f); //0: left   1: right
    }

    private void Update()
    {
        if (goRight)
        {
            if (transform.position.x < boundLimit)
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            }
            else
            {
                goRight = false;
            }
        }
        else
        {
            if (transform.position.x > -boundLimit)
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }
            else
            {
                goRight = true;
            }
        }
        
        
    }
}