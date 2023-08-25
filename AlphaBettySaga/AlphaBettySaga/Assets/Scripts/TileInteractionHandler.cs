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
    void Start()
    {
        _camera = Camera.main;
        distanceThreshold = CalculateMaxDistance();
        db = GetComponent<WordDatabase>();
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            DetectMouseOverTiles();
        } else if (Input.GetMouseButtonUp(0))
        {
            WordValidation();
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
            }
        }
        
    }
    
    void WordValidation()
    {
        // Form the chain of letters
        string chain = string.Join("", tileLetters);
        Debug.Log("Chain: " + chain);
        if (db.IsWordValid(chain))
        {
            //get points & stuff
            Debug.Log("Valid");
            foreach (var tile in selectedTilesList)
            {
                Destroy(tile.gameObject);
            }
        }
        else
        {
            Debug.Log("Not valid");
            DeselectAllTiles();
        }
    }
    void DeselectAllTiles()
    {
        foreach (Tile tile in selectedTilesList)
        {
            tile.TileDeSelect();
        }
        selectedTilesList.Clear();
        selectedTile = null;
        
        // Form the chain of letters
        string chain = string.Join("", tileLetters);
        Debug.Log("Chain: " + chain);
        tileLetters.Clear();
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

}
