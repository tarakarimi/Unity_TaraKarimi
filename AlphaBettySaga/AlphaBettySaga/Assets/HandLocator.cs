using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLocator : MonoBehaviour
{
    private float yOffset = -255f;
    private float dist = 300;
    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        float ypos = yOffset + dist * (currentLevel-1);
        transform.localPosition = new Vector2(transform.position.x,ypos);
    }
}
