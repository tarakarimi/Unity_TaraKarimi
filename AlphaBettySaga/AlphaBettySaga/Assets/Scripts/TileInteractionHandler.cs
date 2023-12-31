using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
public class TileInteractionHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip destroySfx;
    [SerializeField] private GameObject silverTilePrefab, goldenTilePrefab, wildTilePrefab, bombTilePrefab,Fog,trailPrefab,tilePrefab, arrowPrefab, LightImg;
    [SerializeField] private WordDatabase db;
    [SerializeField] private GameManager GM;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask tileLayerMask;
    public GameObject[,] tileMatrix;
    private List<char> tileLetters = new List<char>();
    private List<Tile> selectedTilesList = new List<Tile>();
    private List<GameObject> arrows = new List<GameObject>();
    private Tile selectedTile;
    private Vector3 centerOffset;
    private float distanceThreshold, tileSize,maxDelayTimeForFall;
    private int gridSize = 5,extraPoints;
    private bool createFromSilver, createFromGold, createFromWild, createFromBomb,wordIsValid,isFarsiLanguage;
    public bool isTouchActive, isGameOver;
    private string languagePreference;
    private char startLetter, finalLetter;
    
    void Start()
    {
        centerOffset = GM.centerOffset;
        gridSize = GM.gridSize;
        tileSize = GM.tileSize;
        tileMatrix = GM.tileMatrix;
        distanceThreshold = CalculateMaxDistance();
        LanguageSetUp();
    }
    
    void Update()
    {
        if (isTouchActive && !isGameOver)
        {
            if (Input.GetMouseButton(0))
            {
                DetectMouseOverTiles();
            } else if (Input.GetMouseButtonUp(0) && selectedTilesList.Count > 0)
            {
                WordValidation();
            }
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                StartCoroutine(ChangeTileToBooster(bombTilePrefab,1));
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(ChangeTileToBooster(silverTilePrefab,1));
            }
        }
        
    }
    
    private void DetectMouseOverTiles()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("TileLayer")) {
            Tile tile = hit.collider.GetComponent<Tile>();
            int wordCount = tileLetters.Count;
            if (selectedTile != tile && !selectedTilesList.Contains(tile) && (selectedTile == null || IsAdjacent(tile, selectedTile)))
            {
                if (wordCount <= 10)
                {
                    tile.TileSelect();
                    selectedTilesList.Add(tile);
                    tileLetters.Add(tile.GetLetter());
                    wordCount++;
                    string word = string.Join("", tileLetters);
                    selectedTile = tile;
                    GM.wordInProgress.text = word;
                    GM.scoreOfWordInProgress.text = ScoringManager.CalculateScore(selectedTilesList).ToString();
                    if (wordCount > 1)
                    {
                        Transform previousTile = selectedTilesList[Index.FromEnd(2)].transform;
                        Transform currentTile = selectedTile.transform;
                        CreateArrow(previousTile, currentTile);
                        LightCheck();
                    }
                }
            } else if (wordCount > 1 && tile == selectedTilesList[Index.FromEnd(2)])
            {
                DeselectLastTile();
            }
        }
        
    }

    void LightCheck()
    {
        LightImg.SetActive(false);
        wordIsValid = false;
        string chain = string.Join("", tileLetters);
        int chainLength = chain.Length;
        if (chain.Contains("*"))
        {
            for (char letter = startLetter; letter <= finalLetter; letter++)
            {
                string wordToCheck = chain.Replace('*', letter);
                if (db.IsWordValid(wordToCheck, chainLength))
                {
                    wordIsValid = true;
                    break;
                }
            }
        }
        else if (db.IsWordValid(chain, chainLength))
        {
            wordIsValid = true;
        }
        LightImg.SetActive(wordIsValid);
    }

    void WordValidation()
    {
        if (wordIsValid)
        {
            GM.SubtractMove();
            CalculateScore();

            int wordLength = tileLetters.Count;
            foreach (var tile in selectedTilesList)
            {
                var tempRow = tile.row;
                var tempCol = tile.col;
                tileMatrix[tempRow, tempCol] = null;
                Destroy(tile.gameObject);
                _audioSource.PlayOneShot(destroySfx);
                ShiftTiles(tempRow, tempCol, tile.transform.tag);
            }
            if (wordLength >= 4)
            {
                switch (wordLength)
                {
                    case 4:
                        StartCoroutine(ChangeTileToBooster(silverTilePrefab,1));
                        break;
                    case 5:
                        StartCoroutine(ChangeTileToBooster(goldenTilePrefab,1));
                        break;
                    default:
                        if (Random.Range(0, 2) == 0)
                        {
                            StartCoroutine(ChangeTileToBooster(wildTilePrefab,1));
                        } else {
                            StartCoroutine(ChangeTileToBooster(bombTilePrefab,1));
                        } 
                        break;
                }
            }
        }
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
        distanceThreshold = MathF.Sqrt(2 * MathF.Pow(tileSize, 2))+0.01f;
        return distanceThreshold;
    }
    
    public void ShiftTiles(int row, int col, string tileTag)
    {
        StartCoroutine(TouchState(false, 0));
        if (tileTag == "Bomb")
        {
            for(int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    GameObject tileObject = tileMatrix[i, j];
                    if (tileObject != null)
                    {
                        if (i==row && j == col) continue;
                        
                        Tile tileScript = tileMatrix[i,j].GetComponent<Tile>();
                        if (tileScript.row == row || tileScript.col == col)
                        {
                            if (!selectedTilesList.Contains(tileScript))
                            {
                                extraPoints += 10;
                                string keptTag = tileObject.tag;
                                Destroy(tileObject.gameObject);
                                tileMatrix[i, j] = null;
                                ShiftTiles(i, j, keptTag);
                            }
                        }
                    }
                }
            }
            tileMatrix[row, col] = null;
            Destroy(tileMatrix[row,col]);
        }
        else
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
        StartCoroutine(LogNullHouses());
    }

    IEnumerator LogNullHouses()
    {
        yield return new WaitForSeconds(0.6f);
    
    GM.AddScore(extraPoints);
    maxDelayTimeForFall = 0;
    extraPoints = 0;
    float delayTime = 0;
    
    for (int row = 0; row < gridSize; row++)
    {
        for (int col = 0; col < gridSize; col++)
        {
            if (tileMatrix[row, col] == null)
            {
                Vector3 tilePosition = new Vector3(col * tileSize, row * tileSize + (gridSize * tileSize) + 5, 0) - centerOffset;
                GameObject tempTile;
                if (createFromGold)
                {
                    tempTile = Instantiate(goldenTilePrefab, tilePosition, Quaternion.identity);
                    createFromGold = false;
                }
                else if (createFromSilver)
                {
                    tempTile = Instantiate(silverTilePrefab, tilePosition, Quaternion.identity);
                    createFromSilver = false;
                }
                else if (createFromWild)
                {
                    tempTile = Instantiate(wildTilePrefab, tilePosition, Quaternion.identity);
                    createFromWild = false;
                }
                else if (createFromBomb)
                {
                    tempTile = Instantiate(bombTilePrefab, tilePosition, Quaternion.identity);
                    createFromBomb = false;
                }
                else
                {
                    tempTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                }
                tempTile.GetComponent<TileFall>().StartTileFall(delayTime, gridSize * tileSize + 5);
                tempTile.GetComponent<Tile>().SetGridPosition(row, col);
                tempTile.transform.SetParent(GM.tileParent);
                tileMatrix[row, col] = tempTile;
                delayTime += 0.08f;
            }
        }
    }
    
    maxDelayTimeForFall = delayTime + 0.4f;
    StartCoroutine(TouchState(true, maxDelayTimeForFall));
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
                tileObject.transform.position += new Vector3(0,15f,0);
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
                tile.GetComponent<TileFall>().StartTileFall(delay_time, 15f);
                tile.GetComponent<Tile>().SetLetterProperties();
                letterIndex++;
                delay_time += delay_speed;
            }
        }
        Fog.SetActive(true);
    }
    
    IEnumerator ChangeTileToBooster(GameObject prefab, int num)
    {
        int randomRow = Random.Range(0, gridSize);
        int randomCol = Random.Range(0, gridSize);
        GameObject tile = tileMatrix[randomRow, randomCol];
        if (tile != null && !tile.CompareTag("Bomb") && !tile.CompareTag("WildTile"))
        {
            char letterKept = tile.GetComponent<Tile>().GetLetter();
            Vector3 tilePosition = tile.transform.position;
            int shiftStep = tile.GetComponent<Tile>().shiftDownStep;
            Destroy(tile.gameObject);
            GameObject magicTile = Instantiate(prefab, tilePosition, Quaternion.identity);
            GameObject trail = Instantiate(trailPrefab,new Vector3(0,10,0), quaternion.identity);
            Vector3 end = magicTile.transform.position;
            trail.GetComponent<Trail>().StartMovement(end);
            Tile tileScript = magicTile.GetComponent<Tile>();
            
            if (prefab != wildTilePrefab)
            {
                yield return new WaitForSeconds(0.001f);
                tileScript.letter = letterKept;
                tileScript.SetLetterProperties();
            }
            else
            {
                yield return new WaitForSeconds(0.02f);
                tileScript.letter = '*';
                tileScript.SetLetterProperties();
            }
            tileScript.SetGridPosition(randomRow, randomCol);
            tileScript.shiftDownStep = shiftStep;
            tileScript.ShiftDown();
            tileMatrix[randomRow, randomCol] = magicTile;
        }
        else
        {
            if (prefab == silverTilePrefab) { createFromSilver = true; }
            else if (prefab == goldenTilePrefab) {createFromGold= true; }
            else if (prefab == wildTilePrefab) { createFromWild = true; } 
            else { createFromBomb = true; }
        }
    }

    
    public void MovesToBomb(int num)
    {
        num++;
        createFromBomb = createFromGold = createFromSilver = createFromWild = false;
        isGameOver = true;
        StartCoroutine(ChangeMovesToBombs(num));
    }

    
    IEnumerator ChangeMovesToBombs(int num)
    {
        Debug.Log("Come to wait at: " +Time.time);
        yield return new WaitForSeconds(4f);
        Debug.Log("waited for 5 sec at: " +Time.time);
        for (int i = 0; i < num; i++)
        {
            if (GM.numberOfMoves > 0)
            {
                GM.numberOfMoves--;
                GM.movesText.text = GM.numberOfMoves.ToString();
                yield return new WaitForSeconds(0.4f);
                yield return ChangeTileToBooster(bombTilePrefab, 1);
            }
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(DestroyBombTiles());
    }
    

    IEnumerator DestroyBombTiles()
    {
        GameObject[] bombObjects = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (GameObject bombObject in bombObjects)
        {
            if (bombObject != null)
            {
                Tile tempBombScript = bombObject.GetComponent<Tile>();
                int row = tempBombScript.row;
                int col = tempBombScript.col;
                _audioSource.PlayOneShot(destroySfx);
                Destroy(bombObject.gameObject);
                ShiftTiles(row,col,"Bomb");
            }
        }

        createFromBomb = false;
        yield return new WaitForSeconds(3f);
        GM.winPage.SetActive(true);
    }

    void LanguageSetUp()
    {
        languagePreference = PlayerPrefs.GetString("LanguagePreference", "English");
        isFarsiLanguage = (languagePreference == "Farsi");
        if (isFarsiLanguage)
        {
            startLetter = 'ا';
            finalLetter = 'ی';
        }
        else
        {
            startLetter = 'a';
            finalLetter = 'z';
        }
    }

    public IEnumerator TouchState(bool toggle, float time)
    {
        yield return new WaitForSeconds(time);
        isTouchActive = toggle;
    }
}
