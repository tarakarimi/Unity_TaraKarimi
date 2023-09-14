using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioSource audioSourceMusic;
    [SerializeField] private AudioSource audioSourceSFX;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSourceMusic.clip = menuMusic;
            audioSourceMusic.Play();
        }
        else
        {
            Debug.Log("destroy");
            Destroy(gameObject);
            return;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            audioSourceMusic.clip = gameMusic;
            audioSourceMusic.Play();
        }
        else
        {
            if (!audioSourceMusic.isPlaying || audioSourceMusic.clip != menuMusic)
            {
                audioSourceMusic.clip = menuMusic;
                audioSourceMusic.Play();
            }
            
        }
    }

    public void playSfx()
    {
        audioSourceSFX.Play();
    }

}
