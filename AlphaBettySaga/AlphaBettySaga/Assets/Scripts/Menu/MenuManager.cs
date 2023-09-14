using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.DeleteAll();
        } 
    }

    public void GameScene()
    {
        SceneManager.LoadScene("LevelsScene");
    }
    public void Setting()
    {
        SceneManager.LoadScene("Setting");
    }

    public void ExitApp()
    {
        Application.Quit();
    }
    
    public void PlayClickSound()
    {
        audioManager.PlayClickSound();
    }
}
