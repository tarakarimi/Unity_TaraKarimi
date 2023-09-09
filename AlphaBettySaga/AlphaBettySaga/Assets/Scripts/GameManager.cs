using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private void Start()
    {
        tileMatrix = new GameObject[gridSize, gridSize];
        movesText.text = numberOfMoves.ToString();
        goalText.text = "/ " + goalScore;
        CreateGrid();

        if (levelNumber == 1)
        {
            StartCoroutine(SetMidRowLetters());
        }
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
            if (numberOfMoves == 0)
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

    private IEnumerator SetMidRowLetters()
    {
        yield return null;
        tileMatrix[2, 0].gameObject.GetComponent<Tile>().letter = 'a';
        tileMatrix[2, 1].gameObject.GetComponent<Tile>().letter = 'l';
        tileMatrix[2, 2].gameObject.GetComponent<Tile>().letter = 'p';
        tileMatrix[2, 3].gameObject.GetComponent<Tile>().letter = 'h';
        tileMatrix[2, 4].gameObject.GetComponent<Tile>().letter = 'a';

        for (int col = 0; col < gridSize; col++)
        {
            tileMatrix[2, col].gameObject.GetComponent<Tile>().SetLetterProperties();
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

}