using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectHolder : MonoBehaviour
{
    private static AspectHolder _instance;
    public static AspectHolder Instance { get { return _instance; } }
    // Start is called before the first frame update
    public int OrthoSize;
    void Start()
    {
        
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Camera.main.orthographicSize = OrthoSize * Screen.height / Screen.width;
    }
}
