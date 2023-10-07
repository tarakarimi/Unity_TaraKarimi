using System;
using UnityEngine;

public class Trail : MonoBehaviour
{
    private Vector3 startTransform, endTransform;
    public float moveDuration = 0.5f;

    private float startTime;
    private bool isMoving;

    private void Update()
    {
        if (isMoving)
        {
            float journeyTime = Time.time - startTime;

            if (journeyTime < moveDuration)
            {
                float distanceCovered = journeyTime / moveDuration;
                transform.position = Vector3.Lerp(startTransform, endTransform, distanceCovered);
            }
            else
            {
                // Ensure the trail reaches the end position
                transform.position = endTransform;
                isMoving = false;
                Destroy(this.gameObject,3f);
            }
        }
    }

    public void StartMovement(Vector3 endPosition)
    {
        endTransform = endPosition;
        startTransform = endPosition + new Vector3(0, 4, 0);
        startTime = Time.time;
        isMoving = true;
    }
}