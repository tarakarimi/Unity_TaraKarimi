using System.Collections;
using System.Collections.Generic;
using RTLTMPro;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    private bool musicStatus = true, SFXStatus = true;
    [SerializeField] private Button MusicButtonRef, SFXButtonRef;
    [SerializeField] private Sprite MusicOn, MusicOff, SFXOn, SFXOff;
    [SerializeField] private RTLTextMeshPro LanguageText;
    private string engStr = "English", farStr = "Farsi";
    private string currentLanguage;
    private AudioManager audioManager;

    private string musicStatusKey = "MusicStatus";
    private string SFXStatusKey = "SFXStatus";
    private string languagePrefKey = "LanguagePreference";

    void Start()
    {
        currentLanguage = engStr;
        LanguageText.text = currentLanguage;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        
        if (PlayerPrefs.HasKey(musicStatusKey))
        {
            musicStatus = PlayerPrefs.GetInt(musicStatusKey) == 1;
            UpdateMusicUI();
        }

        if (PlayerPrefs.HasKey(SFXStatusKey))
        {
            SFXStatus = PlayerPrefs.GetInt(SFXStatusKey) == 1;
            UpdateSFXUI();
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void MusicToggle()
    {
        musicStatus = !musicStatus;
        UpdateMusicUI();
        audioManager.SetMusicStatus(musicStatus);
        PlayerPrefs.SetInt(musicStatusKey, musicStatus ? 1 : 0);
        PlayerPrefs.Save();
        

    }

    public void SFXToggle()
    {
        SFXStatus = !SFXStatus;
        UpdateSFXUI();
        if(SFXStatus){
            AudioListener.pause = false;
        } else{
            AudioListener.pause = true;
        }
        PlayerPrefs.SetInt(SFXStatusKey, SFXStatus ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LanguageToggle()
    {
        if (currentLanguage == engStr)
        {
            LanguageText.text = "فارسی";
            currentLanguage = farStr;
        }
        else
        {
            LanguageText.text = engStr;
            currentLanguage = engStr;
        }
        PlayerPrefs.SetString(languagePrefKey, currentLanguage);
        PlayerPrefs.Save();
    }


    public void PlayClickSound()
    {
        audioManager.PlayClickSound();
    }

    private void UpdateMusicUI()
    {
        MusicButtonRef.image.sprite = musicStatus ? MusicOn : MusicOff;
    }

    private void UpdateSFXUI()
    {
        SFXButtonRef.image.sprite = SFXStatus ? SFXOn : SFXOff;
    }
}
