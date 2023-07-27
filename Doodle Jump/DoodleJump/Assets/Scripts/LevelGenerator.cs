using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    [SerializeField] private int numberOfPlatforms = 200;
    [SerializeField] private float levelWidth = 2.6f;
    [SerializeField] private float minY = 0.4f;
    [SerializeField] private float maxY = 1.5f;
    [SerializeField] private GameObject platformParent;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = transform.position;
        
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            GameObject tempPlat = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            tempPlat.transform.parent = platformParent.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
