using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject tilePrefab, winPage, gameoverPage;
    [SerializeField] public Transform tileParent;
    [SerializeField] public int gridSize = 5;
    public float tileSize = 1.1f;
    public GameObject[,] tileMatrix;
    [SerializeField] public Text scoreText, movesText, goalText, ObjectivePreviewText;
    public Text wordInProgress, scoreOfWordInProgress;
    public Vector3 centerOffset;
    [SerializeField] public int numberOfMoves, goalScore;
    [SerializeField] private int currentScore, targetScore;
    private List<string> wordSuggest;
    [SerializeField] private TileInteractionHandler TIH;
    private bool isGameOver;
    [SerializeField] private LevelManager LM;

    private void Start()
    {
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
                Vector3 tilePosition = new Vector3(x * tileSize, y * tileSize + 15, 0) - centerOffset;
                GameObject tempTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
                tempTile.transform.SetParent(tileParent);
                tempTile.transform.GetComponent<TileFall>().StartTileFall(delay_time, 15f);
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
        StartCoroutine(IncrementScoreCoroutine(targetScore));
    }

    public void SubtractMove()
    {
        if (numberOfMoves>0)
        {
            numberOfMoves--;
            movesText.text = numberOfMoves.ToString();
            if (numberOfMoves == 0)
            {
                if (targetScore >= goalScore)
                {
                    winPage.SetActive(true);
                }
                else
                {
                    gameoverPage.SetActive(true);
                }
            }
        }
    }

    private IEnumerator IncrementScoreCoroutine(int targetScore)
    {
        while (currentScore < targetScore)
        {
            currentScore += 20;
            scoreText.text = currentScore.ToString();
            if (targetScore >= goalScore && numberOfMoves > 0)
            {
                if (!isGameOver)
                {
                    TIH.MovesToBomb(numberOfMoves);
                    isGameOver = true;
                }
            }
            yield return null;
        }
    }

    private IEnumerator SetCustomWord(List<string> word, int numberOfWords)
    {
        yield return null;
        for (int row = 0; row < numberOfWords; row++)
        {
            bool leftToRight = (Random.Range(0, 2) == 0);
            string currentWord = word[row];
            if (!leftToRight)
            {
                char[] charArray = currentWord.ToCharArray();
                Array.Reverse(charArray);
                currentWord = new string(charArray);
            }
            for (int col = 0; col < currentWord.Length; col++)
            {
                if (numberOfWords == 1)
                { row = 2;}
                tileMatrix[row, col].gameObject.GetComponent<Tile>().letter = currentWord[col];
                tileMatrix[row, col].gameObject.GetComponent<Tile>().SetLetterProperties();
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
            tileSize = 0.7f;
        }
        StartCoroutine(SetCustomWord(wordSuggest,numberOfWord));
    }
}