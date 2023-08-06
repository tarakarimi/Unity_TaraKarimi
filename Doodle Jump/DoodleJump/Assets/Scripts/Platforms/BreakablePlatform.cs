using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    public GameObject breakEffectPrefab;

    private bool isBroken = false;
    private void Start()
    {
    }

    private void Update()
    {
        float cameraBottomY = Camera.main.transform.position.y - Camera.main.orthographicSize;
        if (transform.position.y < cameraBottomY)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !isBroken && col.relativeVelocity.y <= 0f)
        { 
            BreakPlatform();
        }
    }

    private void BreakPlatform()
    {
        isBroken = true;
        Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
