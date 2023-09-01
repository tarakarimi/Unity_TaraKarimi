using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TileInteractionHandler : MonoBehaviour
{
    private Camera _camera;
    private List<Tile> selectedTilesList = new List<Tile>();
    private Tile selectedTile;
    private float distanceThreshold;
    private List<char> tileLetters = new List<char>();
    private WordDatabase db;
    private int gridSize = 5;
    [SerializeField] private GameObject tilePrefab;
    private float tileSize = 1.1f;
    private Vector3 centerOffset;
    public GameObject[,] tileMatrix;
    private GameManager GM;
    void Start()
    {
        _camera = Camera.main;
        distanceThreshold = CalculateMaxDistance();
        db = GetComponent<WordDatabase>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        centerOffset = GM.centerOffset;
        gridSize = GM.gridSize;
        tileSize = GM.tileSize;
        tileMatrix = GM.tileMatrix;
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            DetectMouseOverTiles();
        } else if (Input.GetMouseButtonUp(0) && selectedTilesList.Count > 0)
        {
            WordValidation();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LogNullHouses());
        }
    }
    
    private void DetectMouseOverTiles()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Tile")) {
            Tile tile = hit.collider.GetComponent<Tile>();
            if (!selectedTilesList.Contains(tile) && (selectedTile == null || IsAdjacent(tile, selectedTile)))
            {
                tile.TileSelect();
                selectedTilesList.Add(tile);
                tileLetters.Add(tile.GetLetter());
                selectedTile = tile;
                if (tileLetters.Count <= 10)
                {
                    GM.wordInProgress.text = string.Join("", tileLetters);
                    GM.scoreOfWordInProgress.text = ScoringManager.CalculateScore(selectedTilesList).ToString();
                }
            }
        }
        
    }
    
    void WordValidation()
    {
        // Form the chain of letters
        string chain = string.Join("", tileLetters);
        if (db.IsWordValid(chain))
        {
            CalculateScore();
            GM.SubtractMove();
            Debug.Log("Word: " + chain+ " is Valid");
            
            foreach (var tile in selectedTilesList)
            {
                tileMatrix[tile.row, tile.col] = null;
                var tempRow = tile.row;
                var tempCol = tile.col;
                Destroy(tile.gameObject);
                ShiftTiles(tempRow, tempCol);
            }
        }

        StartCoroutine(LogNullHouses());
        DeselectAllTiles();
    }
    void DeselectAllTiles()
    {
        foreach (Tile tile in selectedTilesList)
        {
            tile.TileDeSelect();
        }
        selectedTilesList.Clear();
        selectedTile = null;
        tileLetters.Clear();
        GM.wordInProgress.text = "";
        GM.scoreOfWordInProgress.text = "0";
    }
    
    bool IsAdjacent(Tile tile1, Tile tile2)
    {
        // Check if tile1 is horizontally, vertically, or diagonally adjacent to tile2
        Vector2 tile1Pos = tile1.transform.position;
        Vector2 tile2Pos = tile2.transform.position;
        return Vector2.Distance(tile1Pos, tile2Pos) <= distanceThreshold;
    }

    private float CalculateMaxDistance()
    {
        GameManager _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        float dist = _gameManager.tileSize;
        distanceThreshold = MathF.Sqrt(2 * MathF.Pow(dist, 2))+0.01f;
        return distanceThreshold;
    }
    
    public void ShiftTiles(int row, int col)
    {
        for(int i = row+1; i < gridSize; i++)
        {
            GameObject tileObject = tileMatrix[i, col];
            if (tileObject != null)
            {
                Tile tileScript = tileMatrix[i,col].GetComponent<Tile>();
                if (tileScript != null)
                {
                    tileScript.shiftDownStep++;
                    tileScript.ShiftDown();
                }
            }
        }
    }

    IEnumerator LogNullHouses()
    {
        yield return new WaitForSeconds(0.3f);

        float delatTime = 0;
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                if (tileMatrix[row, col] == null)
                {
                    //Debug.Log($"House at col {col}, row {row} is null.");
                    Vector3 tilePosition = new Vector3(col * tileSize, row * tileSize + (gridSize * tileSize), 0) - centerOffset;
                    GameObject tempTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                    tempTile.transform.GetComponent<TileFall>().StartTileFall(delatTime, gridSize * tileSize);
                    tempTile.GetComponent<Tile>().SetGridPosition(row,col);
                    tileMatrix[row, col] = tempTile;
                    delatTime += 0.05f;
                }
            }
        }
    }

    private void CalculateScore()
    {
        int score = ScoringManager.CalculateScore(selectedTilesList);
        GM.AddScore(score);
    }

}
