using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class TileInteractionHandler : MonoBehaviour
{
    private Camera _camera;
    private List<Tile> selectedTilesList = new List<Tile>();
    private Tile selectedTile;
    private float distanceThreshold;
    private List<char> tileLetters = new List<char>();
    private WordDatabase db;
    private int gridSize = 5;
    [SerializeField] private GameObject tilePrefab, arrowPrefab, LightImg;
    private float tileSize = 1.1f;
    private Vector3 centerOffset;
    public GameObject[,] tileMatrix;
    private GameManager GM;
    private List<GameObject> arrows = new List<GameObject>();
    private bool wordIsValid = false;
    [SerializeField] private GameObject Fog;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip destroySfx, createSFX;
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

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LogNullHouses());
        }*/
    }
    
    private void DetectMouseOverTiles()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Tile")) {
            Tile tile = hit.collider.GetComponent<Tile>();
            if (selectedTile != tile && !selectedTilesList.Contains(tile) && (selectedTile == null || IsAdjacent(tile, selectedTile)))
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

                if (selectedTilesList.Count>1)
                {
                    Transform previousTile = selectedTilesList[selectedTilesList.Count - 2].transform;
                    Transform currentTile = selectedTile.transform;
                    CreateArrow(previousTile, currentTile);
                    LightCheck();
                }
            } else if (selectedTilesList.Count > 1 && tile == selectedTilesList[selectedTilesList.Count - 2])
            {
                DeselectLastTile();
            }
        }
        
    }

    void LightCheck()
    {
        string chain = string.Join("", tileLetters);
        int chainLength = chain.Length;
        if (db.IsWordValid(chain, chainLength))
        {
            LightImg.SetActive(true);
            wordIsValid = true;
        }
        else
        {
            LightImg.SetActive(false);
            wordIsValid = false;
        }
    }
    void WordValidation()
    {
        // Form the chain of letters
        //string chain = string.Join("", tileLetters);
        if (wordIsValid)
        {
            CalculateScore();
            GM.SubtractMove();
            //Debug.Log("Word: " + chain+ " is Valid");
            
            foreach (var tile in selectedTilesList)
            {
                tileMatrix[tile.row, tile.col] = null;
                var tempRow = tile.row;
                var tempCol = tile.col;
                Destroy(tile.gameObject);
                _audioSource.PlayOneShot(destroySfx);
                ShiftTiles(tempRow, tempCol);
            }
        }

        StartCoroutine(LogNullHouses());
        DeselectAllTiles();
    }
    void DeselectAllTiles()
    {
        LightImg.SetActive(false);
        wordIsValid = false;
        foreach (Tile tile in selectedTilesList)
        {
            tile.TileDeSelect();
        }
        selectedTilesList.Clear();
        selectedTile = null;
        tileLetters.Clear();
        GM.wordInProgress.text = "";
        GM.scoreOfWordInProgress.text = "0";
        ClearArrows();
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

        float delayTime = 0;
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                if (tileMatrix[row, col] == null)
                {
                    //Debug.Log($"House at col {col}, row {row} is null.");
                    Vector3 tilePosition = new Vector3(col * tileSize, row * tileSize + (gridSize * tileSize), 0) - centerOffset;
                    GameObject tempTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                    //_audioSource.clip = createSFX;
                    //_audioSource.PlayDelayed(delayTime);
                    tempTile.transform.GetComponent<TileFall>().StartTileFall(delayTime, gridSize * tileSize);
                    tempTile.GetComponent<Tile>().SetGridPosition(row,col);
                    tempTile.transform.SetParent(GM.tileParent);
                    tileMatrix[row, col] = tempTile;
                    delayTime += 0.08f;
                }
            }
        }
    }

    private void CalculateScore()
    {
        int score = ScoringManager.CalculateScore(selectedTilesList);
        GM.AddScore(score);
    }

    private void CreateArrow(Transform startTile, Transform endTile)
    {
        var arrowPos= (startTile.position + endTile.position) / 2;
        Vector3 direction = endTile.position - startTile.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion arrowRotation = Quaternion.Euler(0, 0, angle);
        GameObject arrow = Instantiate(arrowPrefab, arrowPos, arrowRotation);
        arrows.Add(arrow);
    }
    
    private void ClearArrows()
    {
        foreach (var arrow in arrows)
        {
            Destroy(arrow);
        }
        arrows.Clear();
    }
    
    private void DeselectLastTile()
    {
        // remove last tile
        var lastInd = selectedTilesList.Count - 1;
        selectedTile = selectedTilesList[lastInd-1];
        Tile lastSelectedTile = selectedTilesList[lastInd];
        lastSelectedTile.TileDeSelect();
        selectedTilesList.RemoveAt(lastInd);
        tileLetters.RemoveAt(lastInd);
        
        // remove last arrow
        Destroy(arrows[arrows.Count - 1]);
        arrows.RemoveAt(arrows.Count - 1);
        GM.wordInProgress.text = string.Join("", tileLetters);
        GM.scoreOfWordInProgress.text = ScoringManager.CalculateScore(selectedTilesList).ToString();
        // Check the light again
        LightCheck();
    }

    public void ShuffleTiles()
    {
        List<char> extractedLetters = new List<char>();
        // Move up and extract letters
        foreach (GameObject tileObject in tileMatrix)
        {
            if (tileObject != null)
            {
                tileObject.transform.position += new Vector3(0,10f,0);
                Tile tileScript = tileObject.GetComponent<Tile>();
                char letter = tileScript.letter;
                extractedLetters.Add(letter);
            }
        }
        extractedLetters.Shuffle();

        // ReAssign letters
        int letterIndex = 0;
        float delay_time = 0.5f;
        float delay_speed = 0.1f;
        foreach (GameObject tileObject in tileMatrix)
        {
            if (tileObject != null)
            {
                GameObject tile = tileObject;
                tile.GetComponent<Tile>().letter = extractedLetters[letterIndex];
                tile.GetComponent<TileFall>().StartTileFall(delay_time, 10f);
                //tileObject.transform.position -= new Vector3(0,10f,0);
                tile.GetComponent<Tile>().SetLetterProperties();
                letterIndex++;
                delay_time += delay_speed;
            }
        }
        Fog.SetActive(true);
    }
}
