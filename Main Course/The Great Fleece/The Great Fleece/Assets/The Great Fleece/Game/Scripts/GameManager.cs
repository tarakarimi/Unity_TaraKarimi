using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public PlayableDirector introCutscene;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is missing!");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public bool HasCard
    {
        get;
        set;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            introCutscene.time = 60.0f;
        }
    }
}
