using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject breakablePlatformPrefab;
    public GameObject moveablePlatformPrefab;
    private int numberOfPlatforms = 50;
    [SerializeField] private float levelWidth = 2.6f;
    [SerializeField] private float minY = 0.4f;
    [SerializeField] private float maxY = 1.5f;
    [SerializeField] private GameObject platformParent;
    private Vector3 spawnPosition;
    [SerializeField] private GameObject firstPlat;

    private bool lastPlatformWasBreakable = false;
    private float lastNonBreakableXPosition = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = firstPlat.transform.position;
        lastNonBreakableXPosition = spawnPosition.x;
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
            GameObject prefabToSpawn;

            if (lastPlatformWasBreakable || Random.Range(0f, 1f) > 0.5f)
            {
                prefabToSpawn = platformPrefab;
                lastPlatformWasBreakable = false;
            }
            else if (Random.Range(0f, 1f) > 0.5f)
            {
                prefabToSpawn = breakablePlatformPrefab;
                lastPlatformWasBreakable = true;
            }
            else
            {
                prefabToSpawn = moveablePlatformPrefab;
                lastPlatformWasBreakable = false;
            }

            float platformScale = prefabToSpawn.transform.localScale.x;
            float platformWidth = platformScale;

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
