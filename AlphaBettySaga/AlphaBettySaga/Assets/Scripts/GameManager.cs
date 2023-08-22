using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform tileParent;
    [SerializeField] int gridSize = 5;
    public float tileSize = 1.1f;
    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        Vector3 centerOffset = new Vector3((gridSize - 1) * tileSize / 2, (gridSize - 1) * tileSize / 2, 0);
        float delay_time = 0.5f;
        float delay_speed = 0.1f;
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x< gridSize; x++)
            {
                Vector3 tilePosition = new Vector3(x * tileSize, y * tileSize + 10, 0) - centerOffset;
                GameObject tempTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
                tempTile.transform.SetParent(tileParent);
                tempTile.transform.GetComponent<TileFall>().StartTileFall(delay_time);
                delay_time += delay_speed;
            }
        }
    }
}