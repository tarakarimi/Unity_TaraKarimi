using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public bool isGameOver = false;
    [SerializeField] private Text scoreText;
    private Player _Player;
    private int _score = 0;
    public GameObject dimmingPanel;
    public bool isPaused = false;
    void Start()
    {
        _Player = GameObject.Find("Doodler").GetComponent<Player>();
        UpdateScoreText();
    }

    void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        // Calculate the _score based on the Doodler's height.
        int newScore = Mathf.FloorToInt(_Player.transform.position.y);

        // Update the _score if it has changed.
        if (newScore > _score) 
        {
            _score = newScore;
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = _score.ToString();
    }
    
    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true; 
            dimmingPanel.SetActive(true);
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
}


