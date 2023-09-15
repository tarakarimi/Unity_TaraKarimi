using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    private bool musicStatus = true, SFXStatus = true;
    [SerializeField] private Button MusicButtonRef, SFXButtonRef;
    [SerializeField] private Sprite MusicOn, MusicOff, SFXOn, SFXOff;
    [SerializeField] private Text LanguageText;
    private string engStr = "English", farStr = "Farsi";
    private string currentLanguage;
    private AudioManager audioManager;

    // Define PlayerPrefs keys for saving settings
    private string musicStatusKey = "MusicStatus";
    private string SFXStatusKey = "SFXStatus";

    void Start()
    {
        currentLanguage = engStr;
        LanguageText.text = currentLanguage;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // Load saved settings (if available)
        if (PlayerPrefs.HasKey(musicStatusKey))
        {
            musicStatus = PlayerPrefs.GetInt(musicStatusKey) == 1; // 1 for true, 0 for false
            UpdateMusicUI();
        }

        if (PlayerPrefs.HasKey(SFXStatusKey))
        {
            SFXStatus = PlayerPrefs.GetInt(SFXStatusKey) == 1; // 1 for true, 0 for false
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

        // Update AudioManager's music status
        audioManager.SetMusicStatus(musicStatus);

        // Save the music status in PlayerPrefs
        PlayerPrefs.SetInt(musicStatusKey, musicStatus ? 1 : 0);
        PlayerPrefs.Save();
        
        Debug.Log("MusicEnabled: " + PlayerPrefs.GetInt("MusicEnabled"));

    }

    public void SFXToggle()
    {
        SFXStatus = !SFXStatus;
        UpdateSFXUI();
        if(SFXStatus == true){
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
            LanguageText.text = farStr;
            currentLanguage = farStr;
        }
        else
        {
            LanguageText.text = engStr;
            currentLanguage = engStr;
        }
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
