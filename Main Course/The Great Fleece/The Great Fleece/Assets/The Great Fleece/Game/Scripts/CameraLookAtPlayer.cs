using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAtPlayer : MonoBehaviour
{
    public Transform target, startCamera;
    public float smoothSpeed = 5.0f; // A smoothing factor to control camera movement speed
    private void Start()
    {
        transform.position = startCamera.position;
        transform.rotation = startCamera.rotation;
    }

    void Update()
    {
        if (target != null)
        {
            //Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
            //transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed * Time.fixedDeltaTime);
            transform.LookAt(target);
        }
    }
}
