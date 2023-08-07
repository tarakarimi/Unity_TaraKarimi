using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab, breakablePlatformPrefab,moveablePlatformPrefab;
    [SerializeField] private GameObject firstPlat;
    [SerializeField] private GameObject platformParent;
    private int numberOfPlatforms = 30;
    [SerializeField] private float minY = 0.4f, maxY = 1.5f, levelWidth = 2.6f;
    private Vector3 spawnPosition;
    private bool lastPlatformWasBreakable;
    private float lastNonBreakableXPosition;
    public List<GameObject> enemyPrefabs; 
    void Start()
    {
        spawnPosition = firstPlat.transform.position;
        lastNonBreakableXPosition = spawnPosition.x;
        SpawnPlatforms();
    }
    
    void Update()
    {
        if (Camera.main.transform.position.y > spawnPosition.y - 10)
        {
            SpawnPlatforms();
        }
    }

    public void SpawnPlatforms()
    {
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            GameObject prefabToSpawn;

            if (lastPlatformWasBreakable || Random.Range(0f, 1f) < 0.5f)
            {
                prefabToSpawn = platformPrefab;
                lastPlatformWasBreakable = false;
            }
            else if (Random.Range(0f, 1f) < 0.6f)
            {
                prefabToSpawn = breakablePlatformPrefab;
                lastPlatformWasBreakable = true;
            }
            else if (Random.Range(0f, 1f) < 0.9f)
            {
                prefabToSpawn = moveablePlatformPrefab;
                lastPlatformWasBreakable = false;
            }
            else 
            {
                int randomIndex = Random.Range(0, enemyPrefabs.Count);
                prefabToSpawn = enemyPrefabs[randomIndex];
                lastPlatformWasBreakable = false;
            }
            
            float xPosition;

            if (lastPlatformWasBreakable)
            {
                float minX = Mathf.Max(-levelWidth, lastNonBreakableXPosition - 1.5f);
                float maxX = Mathf.Min(levelWidth, lastNonBreakableXPosition + 1.5f);
                xPosition = Random.Range(minX, maxX);
                lastNonBreakableXPosition = xPosition;
            }
            else
            {
                xPosition = Random.Range(-levelWidth, levelWidth);
            }

            spawnPosition = new Vector3(xPosition, spawnPosition.y + Random.Range(minY, maxY), 0f);
            GameObject tempPlat = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            tempPlat.transform.parent = platformParent.transform;
        }
    }
}
