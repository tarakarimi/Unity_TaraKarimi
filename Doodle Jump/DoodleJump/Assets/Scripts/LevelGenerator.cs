using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab, breakablePlatformPrefab, moveablePlatformPrefab;
    public List<GameObject> enemyPrefabs; // Attach your enemy prefabs to this list in the Inspector
    [SerializeField] private GameObject firstPlat;
    [SerializeField] private GameObject platformParent;
    private int numberOfPlatforms = 30;
    private float minY = 0.4f, maxY = 2.9f, levelWidth = 2.6f;
    private Vector3 spawnPosition;
    private bool lastInstanceWasNotJumpable;
    private float xPosition;
    private float yPositionRandom;
    private GameObject prefabToSpawn;
    private GameObject tempPlat;
    private float nextYPosition = 0;

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
            if (lastInstanceWasNotJumpable || Random.Range(0f, 1f) < 0.5f)
            {
                prefabToSpawn = platformPrefab;
                lastInstanceWasNotJumpable = false;
            }
            else if (Random.Range(0f, 1f) < 0.6f)
            {
                prefabToSpawn = breakablePlatformPrefab;
                lastInstanceWasNotJumpable = true;
            }
            else if ( Random.Range(0f, 1f) < 0.9f)
            {
                prefabToSpawn = moveablePlatformPrefab;
                lastInstanceWasNotJumpable = false;
            }
            else
            {
                int randomIndex = Random.Range(0, enemyPrefabs.Count);
                prefabToSpawn = enemyPrefabs[randomIndex];
                lastInstanceWasNotJumpable = true;
            }

            xPosition = Random.Range(-levelWidth, levelWidth);
            if (nextYPosition == 0)
            {
                if (lastInstanceWasNotJumpable)
                {
                    yPositionRandom = Random.Range(minY, maxY/2f);
                }
                else
                {
                    yPositionRandom = Random.Range(minY, maxY);
                }
                
            }
            else
            {
                yPositionRandom = nextYPosition;
            }
            spawnPosition = new Vector3(xPosition, spawnPosition.y + yPositionRandom, 0f);
            if (lastInstanceWasNotJumpable)
            {
                //Debug.Log("possible: "+ (tempPlat.transform.position.y + maxY - spawnPosition.y));
                nextYPosition = Random.Range(0.5f, tempPlat.transform.position.y + maxY - spawnPosition.y);
            }
            else
            {
                nextYPosition = 0;
            }
            tempPlat = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            tempPlat.transform.parent = platformParent.transform;

            
            /*
            // Ensure the spawned object doesn't overlap with other objects
            bool overlapping = CheckOverlap(prefabToSpawn, spawnPosition);

            if (!overlapping)
            {
                tempPlat = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                tempPlat.transform.parent = platformParent.transform;
            } else {
                spawnPosition.y -= yPositionRandom;
            }*/
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
}
