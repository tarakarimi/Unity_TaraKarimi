using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private GameObject _enemyContainer;

    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        //StopCoroutine("SpawnRoutine");
    }

    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9f, 9f), 7f, 0);
            GameObject newEnemy =  Instantiate(enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
