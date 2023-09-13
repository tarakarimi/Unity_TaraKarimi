using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab, winPage, gameoverPage;
    [SerializeField] public Transform tileParent;
    [SerializeField] public int gridSize = 5;
    public float tileSize = 1.1f;
    public GameObject[,] tileMatrix;
    [SerializeField] private Text scoreText, movesText, goalText;
    public Text wordInProgress, scoreOfWordInProgress;
    public Vector3 centerOffset;
    [SerializeField] private int numberOfMoves, goalScore, levelNumber;
    [SerializeField] private int currentScore = 1000, targetScore;
    [SerializeField] private Text ObjectivePreviewText;
    private List<string> wordSuggest;

    private void Start()
    {
        LevelManager LM = GetComponent<LevelManager>();
        LM.setLevelInfo();
        
        tileMatrix = new GameObject[gridSize, gridSize];
        movesText.text = numberOfMoves.ToString();
        goalText.text = "/ " + goalScore;
        CreateGrid();
    }

    private void CreateGrid()
    {
        centerOffset = new Vector3((gridSize - 1) * tileSize / 2, (gridSize - 1) * tileSize / 2 + 0.5f, 0);
        float delay_time = 0.5f;
        float delay_speed = 0.1f;
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x< gridSize; x++)
            {
                Vector3 tilePosition = new Vector3(x * tileSize, y * tileSize + 10, 0) - centerOffset;
                GameObject tempTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
                tempTile.transform.SetParent(tileParent);
                tempTile.transform.GetComponent<TileFall>().StartTileFall(delay_time, 10f);
                tempTile.GetComponent<Tile>().SetGridPosition(y,x);
                tileMatrix[y, x] = tempTile;
                delay_time += delay_speed;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("LevelsScene");
        }
    }

    public void AddScore(int addingScore)
    {
        targetScore = currentScore + addingScore;
        if (targetScore > goalScore)
        {
            targetScore = goalScore;
        }
        StartCoroutine(IncrementScoreCoroutine(targetScore));
    }

    public void SubtractMove()
    {
        if (numberOfMoves>0)
        {
            numberOfMoves--;
            if (targetScore == goalScore)
            {
                winPage.SetActive(true);
            }
            else if (numberOfMoves == 0)
            {
                gameoverPage.SetActive(true);
            }
            movesText.text = numberOfMoves.ToString();
        }
    }

    private IEnumerator IncrementScoreCoroutine(int targetScore)
    {
        while (currentScore < targetScore)
        {
            currentScore += 20;
            if (currentScore == goalScore)
            {
                winPage.SetActive(true);
            }

            scoreText.text = currentScore.ToString();
            yield return null;
        }
    }

    private IEnumerator SetMidRowLetters(List<string> word, int numberOfWords)
    {
        yield return null;
        if (numberOfWords == 1)
        {
            int midRow = 2;
            int startCol = (gridSize - word[0].Length) / 2;
            for (int col = 0; col < word[0].Length; col++)
            {
                tileMatrix[midRow, startCol + col].gameObject.GetComponent<Tile>().letter = word[0][col];
                tileMatrix[midRow, startCol + col].gameObject.GetComponent<Tile>().SetLetterProperties();
            }
        }
        else
        {
            for (int c_word = 0; c_word < numberOfWords; c_word++)
            {
                bool writeLeftToRight = (Random.Range(0, 2) == 0);

                string currentWord = word[c_word];
                if (!writeLeftToRight)
                {
                    char[] charArray = currentWord.ToCharArray();
                    Array.Reverse(charArray);
                    currentWord = new string(charArray);
                }

                for (int col = 0; col < currentWord.Length; col++)
                {
                    tileMatrix[c_word, col].gameObject.GetComponent<Tile>().letter = currentWord[col];
                    tileMatrix[c_word, col].gameObject.GetComponent<Tile>().SetLetterProperties();
                }
            }

        }

    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void RePlay()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void SetLevelParameters(int moves, int goal, int size, List<string> word)
    {
        numberOfMoves = moves;
        goalScore = goal;
        ObjectivePreviewText.text = "score " + goal + " points";
        gridSize = size;
        wordSuggest = word;
        int numberOfWord = wordSuggest.Count;
        if (gridSize > 5)
        {
            tileSize = 0.9f;
        }
        StartCoroutine(SetMidRowLetters(wordSuggest,numberOfWord));
    }
    
    

}