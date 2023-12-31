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
    private bool isSFXEnabled = true;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayMenuMusic();
            int soundStatus = PlayerPrefs.GetInt("SFXStatus",1);
            if(soundStatus == 1){
                AudioListener.pause = false;
            } else{
                AudioListener.pause = true;
            }

        }
        else
        {
            Destroy(gameObject);
            return;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            PlayGameMusic();
        }
        else
        {
            if (!audioSourceMusic.isPlaying || audioSourceMusic.clip != menuMusic)
            {
                PlayMenuMusic();
            }
        }
    }

    public void PlayClickSound()
    {
        if (isSFXEnabled)
        {
            audioSourceSFX.Play();
        }
    }

    public void SetMusicStatus(bool status)
    {
        PlayerPrefs.SetInt("MusicEnabled", status ? 1 : 0); // Store user preference
        if (status)
        {
            PlayMenuMusic();
        }
        else
        {
            audioSourceMusic.Stop();
        }
    }
    

    private void PlayMenuMusic()
    {
        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
        {
            audioSourceMusic.clip = menuMusic;
            audioSourceMusic.Play();
        }
    }

    private void PlayGameMusic()
    {
        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1) 
        {
            audioSourceMusic.clip = gameMusic;
            audioSourceMusic.Play();
        }
    }
}
