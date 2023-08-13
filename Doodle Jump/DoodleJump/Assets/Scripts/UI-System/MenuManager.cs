using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int soundMode = PlayerPrefs.GetInt("SoundMode", 1);
        SetSoundMode(soundMode == 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
    
    public void OpenOptions()
    {
        SceneManager.LoadScene("OptionScene");
    }
    
    public void SetSoundMode(bool isSoundOn)
    {
        // Mute or unmute all sounds in the game
        AudioListener.volume = isSoundOn ? 1 : 0;
    }
}
