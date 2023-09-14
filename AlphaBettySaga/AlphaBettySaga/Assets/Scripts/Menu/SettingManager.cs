using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool musicStatus = true, SFXStatus = true;
    [SerializeField] private Button MusicButtonRef, SFXButtonRef;
    [SerializeField] private Sprite MusicOn, MusicOff, SFXOn, SFXOff;
    [SerializeField] private Text LanguageText;
    private string engStr = "English", farStr = "Farsi";
    private string currentLanguage;
    private AudioManager audioManager;

    void Start()
    {
        currentLanguage = engStr;
        LanguageText.text = currentLanguage;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void MusicToggle()
    {
        if (musicStatus)
        {
            musicStatus = false;
            MusicButtonRef.image.sprite = MusicOff;
        }
        else
        {
            musicStatus = true;
            MusicButtonRef.image.sprite = MusicOn;
        }
    }
    
    public void SFXToggle()
    {
        if (SFXStatus)
        {
            SFXStatus = false;
            SFXButtonRef.image.sprite = SFXOff;
        }
        else
        {
            SFXStatus = true;
            SFXButtonRef.image.sprite = SFXOn;
        }
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
        audioManager.playSfx();
    }
}
