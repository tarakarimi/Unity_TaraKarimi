using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    private Vector3 startPosition;
    private bool isFalling;

    private float fallSpeed = 3f;

    public void Start()
    {
        isFalling = true;
    }

    void Update()
    {
        if (isFalling)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
            fallSpeed *= 1.01f;
            float cameraBottomY = Camera.main.transform.position.y - Camera.main.orthographicSize;
            if (transform.position.y < cameraBottomY)
            {
                Destroy(gameObject);
            }
        }
    }
}