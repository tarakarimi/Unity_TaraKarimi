using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 currentVelocity;

    private GameManagerScript _gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        _gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_gameManagerScript.isGameOver == false)
        {
            if (transform.position.y < target.position.y)
            {
                Vector3 newPos = new Vector3(transform.position.x, target.position.y, transform.position.z);
                transform.position = newPos; //Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, _speed * Time.deltaTime);
            }
        }
        
    }

    public void changeView()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y -10, 0);
    }
}
