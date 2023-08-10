using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public bool isGameOver = false;
    [SerializeField] private Text scoreText;
    private Player _Player;
    private int _score = 0;
    public GameObject dimmingPanel;
    public GameObject gameOverPanel;
    public bool isPaused = false;
    private SaveScoreHandler _saveScoreHandler;
    private LevelGenerator _lvlGen;
    
    void Start()
    {
        _Player = GameObject.Find("Doodler").GetComponent<Player>();
        _lvlGen = transform.GetComponent<LevelGenerator>();
        UpdateScoreText();
    }

    void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        // Calculate the _score based on the Doodler's height.
        int newScore = Mathf.FloorToInt(_Player.transform.position.y * 100)/10;

        // Update the _score if it has changed.
        if (newScore > _score) 
        {
            _score = newScore;
            _lvlGen.SetScore(_score);
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = _score.ToString();
    }
    
    public void PauseGame()
    {
        if (isGameOver == false)
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                isPaused = true; 
                dimmingPanel.SetActive(true);
            }
        }   
    }
    
    public void ResumeGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            dimmingPanel.SetActive(false);
        }
    }

    public void PlayAgain()
    {
        gameOverPanel.transform.GetComponent<SaveScoreHandler>().SaveRecord();
        SceneManager.LoadScene("GameScene");
    }

    public void GameOverActions()
    {
        isGameOver = true;
        Camera.main.GetComponent<CameraFollow>().changeView();
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.GetChild(1).GetComponent<Text>().text = "your score: " + _score;
        gameOverPanel.GetComponent<SaveScoreHandler>().SetFinalScore(_score);
    }

    public void GoToMenu()
    {
        gameOverPanel.transform.GetComponent<SaveScoreHandler>().SaveRecord();
        SceneManager.LoadScene("MainMenu");
    }
}


