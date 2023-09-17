using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public LevelData[] levels; 
    private int currentLevel;
    [SerializeField] private GameManager GM;
    [SerializeField] private LevelInfoDialog _levelInfoDialog;
    private Animator animator;
    private AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenInfoDlg(int levelNumber)
    {
        PlayerPrefs.SetInt("CurrentLevel", levelNumber);
        currentLevel = levelNumber;
        LevelData levelData = levels[currentLevel-1];
        int scoreGoal = levelData.scoreGoal;
        _levelInfoDialog.SetProperties(currentLevel, scoreGoal);
    }
    
    public void BackBtn()
    {
        SceneManager.LoadScene("Menu");
    }

    public void setLevelInfo()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        LevelData levelData = levels[currentLevel-1];
        int moves = levelData.movesNumber;
        int scoreGoal = levelData.scoreGoal;
        int gridSize = levelData.gridSize;
        string languagePreference = PlayerPrefs.GetString("LanguagePreference", "English");
        List<string> word;
        if (languagePreference == "Farsi")
        {
            word = levelData.wordListFarsi;
        }
        else
        {
            word = levelData.wordList;
        }
        GM.SetLevelParameters(moves, scoreGoal, gridSize, word);
    }
    
    public void PlayClickSound()
    {
        audioManager.PlayClickSound();
    }
}