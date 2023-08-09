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
    [SerializeField] private float minY = 0.4f, maxY = 3f, levelWidth = 2.6f;
    private float maxYdefault = 2.8f;
    private Vector3 spawnPosition;
    private bool lastPlatformWasBreakable;
    private float xPosition;
    private float yPositionRandom;

    void Start()
    {
        spawnPosition = firstPlat.transform.position;
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
            lastPlatformWasBreakable = false;
            if (lastPlatformWasBreakable || Random.Range(0f, 1f) < 0.5f)
            {
                prefabToSpawn = platformPrefab;
            }
            else if (Random.Range(0f, 1f) < 0.6f)
            {
                prefabToSpawn = breakablePlatformPrefab;
                lastPlatformWasBreakable = true;
            }
            else if (Random.Range(0f, 1f) < 0.9f)
            {
                prefabToSpawn = moveablePlatformPrefab;
            }
            else
            {
                int randomIndex = Random.Range(0, enemyPrefabs.Count);
                prefabToSpawn = enemyPrefabs[randomIndex];
            }

            xPosition = Random.Range(-levelWidth, levelWidth);
            yPositionRandom = Random.Range(minY, maxY);
            spawnPosition = new Vector3(xPosition, spawnPosition.y + yPositionRandom, 0f);

            // Ensure the spawned object doesn't overlap with other objects
            bool overlapping = CheckOverlap(prefabToSpawn, spawnPosition);

            if (!overlapping)
            {
                GameObject tempPlat = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                tempPlat.transform.parent = platformParent.transform;
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
}
