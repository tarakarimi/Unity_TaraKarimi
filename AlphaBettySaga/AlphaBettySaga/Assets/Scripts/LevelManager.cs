using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public LevelData[] levels; 
    private int currentLevel = 0; // track the current level
    private GameManager GM;
    [SerializeField] private GameObject levelInfoPrefab;
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
        Debug.Log("level = "+ levelNumber);
        PlayerPrefs.SetInt("CurrentLevel", levelNumber);
        currentLevel = levelNumber;
        LevelData levelData = levels[currentLevel-1];
        int scoreGoal = levelData.scoreGoal;
        
        levelInfoPrefab.SetActive(true);
        levelInfoPrefab.transform.GetChild(2).GetComponent<Text>().text = "Level " + currentLevel;
        levelInfoPrefab.transform.GetChild(3).GetComponent<Text>().text = "Goal: Score " + scoreGoal + " points";
        animator = levelInfoPrefab.GetComponent<Animator>();
        animator.Play("LevelInfoDlgBox_anim", 0, 0f);
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
        List<string> word = levelData.wordList;
        GM = GetComponent<GameManager>();
        GM.SetLevelParameters(moves, scoreGoal, gridSize, word);
    }
    
    public void PlayClickSound()
    {
        audioManager.playSfx();
    }
}