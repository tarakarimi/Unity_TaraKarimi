using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab, breakablePlatformPrefab, moveablePlatformPrefab, SpringPrefab, eyeTrampolinePrefab;
    public List<GameObject> enemyPrefabs;
    [SerializeField] private GameObject firstPlat;
    [SerializeField] private GameObject platformParent;
    private int numberOfPlatforms = 30;
    private Vector3 spawnPosition;
    private bool lastInstanceWasNotJumpable;
    private float xPosition, yPositionRandom;
    private GameObject prefabToSpawn,tempPlat;
    private float nextYPosition ;
    private int _score;
    private float initialMaxY = 1, maxMaxY = 2.9f, currentMaxY = 1, minY = 0.4f, levelWidth = 2.6f;
    private float allFeatureScore = 3000;
    private bool spawnSpring;

    void Start()
    {
        spawnPosition = firstPlat.transform.position;
        tempPlat = firstPlat;
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
            if (lastInstanceWasNotJumpable || Random.Range(0f, 1f) < 0.4f)
            {
                prefabToSpawn = platformPrefab;
                lastInstanceWasNotJumpable = false;
                if (Random.Range(0f,1f) < 0.12f)
                {
                    spawnSpring = true;
                }
                else
                {
                    spawnSpring = false;
                }
            }
            else if (Random.Range(0f, 1f) < 0.5f)
            {
                prefabToSpawn = breakablePlatformPrefab;
                lastInstanceWasNotJumpable = true;
                spawnSpring = false;
            }
            else if ( Random.Range(0f, 1f) < 0.7f && _score > 100)
            {
                prefabToSpawn = moveablePlatformPrefab;
                lastInstanceWasNotJumpable = false;
                if (Random.Range(0f,1f) < 0.1f)
                {
                    spawnSpring = true;
                }
                else
                {
                    spawnSpring = false;
                }
            }
            else if( _score > 500)
            {
                int randomIndex = Random.Range(0, enemyPrefabs.Count);
                prefabToSpawn = enemyPrefabs[randomIndex];
                lastInstanceWasNotJumpable = true;
                spawnSpring = false;
            }
            else
            {
                prefabToSpawn = platformPrefab;
            }

            xPosition = Random.Range(-levelWidth, levelWidth);
            if (nextYPosition == 0)
            {
                if (lastInstanceWasNotJumpable)
                {
                    yPositionRandom = Random.Range(minY, currentMaxY/2f);
                }
                else
                {
                    yPositionRandom = Random.Range(minY, currentMaxY);
                }
            }
            else
            {
                yPositionRandom = nextYPosition;
            }
            spawnPosition = new Vector3(xPosition, spawnPosition.y + yPositionRandom, 0f);
            if (lastInstanceWasNotJumpable)
            {
                nextYPosition = Random.Range(0.5f, tempPlat.transform.position.y + currentMaxY - spawnPosition.y);
            }
            else
            {
                nextYPosition = 0;
            }
            
            bool overlapping = CheckOverlap(prefabToSpawn, spawnPosition);

            if (!overlapping)
            {
                tempPlat = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                tempPlat.transform.parent = platformParent.transform;
                if (spawnSpring)
                {
                    int randomIndex = Random.Range(0, 2);
                    GameObject jumpingPrefab = (randomIndex == 0) ? SpringPrefab : eyeTrampolinePrefab;
                    GameObject tempSpring = Instantiate(jumpingPrefab, spawnPosition + new Vector3(0.25f,0.25f,0), Quaternion.identity);
                    tempSpring.transform.parent = tempPlat.transform;
                }
            } else {
                spawnPosition.y -= yPositionRandom;
            }
        }
    }

    private bool CheckOverlap(GameObject prefab, Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position, prefab.transform.localScale, 0f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Platform") || collider.gameObject.CompareTag("Enemy"))
            {
                return true;
            }
        }

        return false;
    }

    public void SetScore(int score)
    {
        _score = score;
        UpdateDifficulty();
    }
    
    private void UpdateDifficulty()
    {
        float progress = Mathf.Clamp01(_score / allFeatureScore);
        currentMaxY = Mathf.Lerp(initialMaxY, maxMaxY, progress);
        /*if (_score > 500) 
        {
            // Enable additional platforms, enemies, etc.
            
            if (_score > 1500)
            {
                
            }
        }*/
    }
}
