using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float moveDistance = 2.0f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private bool movingToTarget = true;

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + new Vector3(moveDistance, 0f, 0f);
    }

    private void Update()
    {
        float step = moveSpeed * Time.deltaTime;

        if (movingToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            if (transform.position.x >= targetPosition.x)
            {
                movingToTarget = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, step);
            if (transform.position.x <= initialPosition.x)
            {
                movingToTarget = true;
            }
        }
    }
}