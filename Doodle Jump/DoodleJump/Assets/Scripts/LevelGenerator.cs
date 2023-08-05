using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    private int numberOfPlatforms = 50;
    [SerializeField] private float levelWidth = 2.6f;
    [SerializeField] private float minY = 0.4f;
    [SerializeField] private float maxY = 1.5f;
    [SerializeField] private GameObject platformParent;
    Vector3 spawnPosition;
    [SerializeField] private GameObject firstPlat;
    [SerializeField] private GameObject breakablePlatformPrefab;
    [SerializeField] private GameObject movablePlatformPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = firstPlat.transform.position;
        SpawnPlatforms();
    }

    // Update is called once per frame
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
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);

            // Check if the current spawn position will obstruct the doodler's jumpable path.
            bool canSpawn = !Physics2D.Raycast(spawnPosition, Vector2.down, maxY * 2);

            if (canSpawn)
            {
                // Determine platform type to spawn.
                GameObject platformToSpawn = platformPrefab;
                float randomValue = Random.value;

                if (randomValue < 0.2f) // 20% chance for breakable platform.
                    platformToSpawn = breakablePlatformPrefab;
                else if (randomValue < 0.4f) // 20% chance for movable platform.
                    platformToSpawn = movablePlatformPrefab;

                // Instantiate the selected platform.
                GameObject tempPlat = Instantiate(platformToSpawn, spawnPosition, Quaternion.identity);
                tempPlat.transform.parent = platformParent.transform;
            }
        }
    }
    /*public void SpawnPlatforms()
    {
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            GameObject tempPlat = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            tempPlat.transform.parent = platformParent.transform;
        }
    }*/
}
