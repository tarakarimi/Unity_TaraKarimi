using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInteractionHandler : MonoBehaviour
{
    private Camera _camera;
    private List<Tile> selectedTiles = new List<Tile>();
    void Start()
    {
        _camera = Camera.main;
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            DetectMouseOverTiles();
        } else if (Input.GetMouseButtonUp(0))
        {
            DeselectAllTiles();
        }
    }
    
    private void DetectMouseOverTiles()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Tile")) {
            Tile tile = hit.collider.GetComponent<Tile>();
            tile.TileSelect();
            selectedTiles.Add(tile);
        }
        
    }
    
    void DeselectAllTiles()
    {
        foreach (Tile tile in selectedTiles)
        {
            tile.TileDeSelect();
        }
        selectedTiles.Clear();
    }
}
