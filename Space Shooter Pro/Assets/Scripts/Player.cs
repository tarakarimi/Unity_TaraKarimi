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
        //player movement
        transform.Translate(Vector3.right * _speed * Time.deltaTime);   //transform.Translate(new Vector3(1,0,0));
    }
}
