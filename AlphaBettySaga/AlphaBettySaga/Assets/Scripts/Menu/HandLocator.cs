using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLocator : MonoBehaviour
{
    private float yOffset = -255f;
    private float dist = 350;
    private float maxY;
    private int maxLvl = 3;
    void Start()
    {
        maxY = yOffset + dist * (maxLvl-1);
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        float ypos = yOffset + dist * (currentLevel-1);
        if (ypos>maxY)
        {
            ypos = maxY;
        }
        transform.localPosition = new Vector2(transform.position.x,ypos);
    }
}
