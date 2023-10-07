using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText, bestScoreText;
    public int bestScore;

    [SerializeField] private Sprite[] _liveSprites;

    [SerializeField] private Image _livesDisplayImg;

    [SerializeField] private Text _gameOverText;

    [SerializeField] private Text _restartText;
    private GameManager _gameManager;
        
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + 0;
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best: " + bestScore;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("game manager script not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreText(int playerScore)
    {
        scoreText.text = "score: " + playerScore.ToString();
    }

    public void CheckForBestScore(int currentScore)
    {
        if (currentScore>bestScore)
        {
            bestScore = currentScore;
            bestScoreText.text = "score: " + bestScore.ToString();
            PlayerPrefs.SetInt("BestScore",bestScore);
        }
    }

    public void UpdateLives(int currentLives)
    {
        if (currentLives <= 0)
        {
            currentLives = 0;
            
        }
        _livesDisplayImg.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }
    
    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(true);
        }
        
    }

    public void ResumePlay()
    {
        _gameManager.ResumeGame();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
