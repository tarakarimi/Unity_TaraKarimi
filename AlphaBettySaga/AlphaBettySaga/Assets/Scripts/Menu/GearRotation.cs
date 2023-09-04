using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotation : MonoBehaviour
{
    private float angle;
    [SerializeField] private float speed = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle += speed;
        transform.rotation = Quaternion.Euler(0,0,angle);
    }
}
