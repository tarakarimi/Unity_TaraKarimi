using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _rotSpeed = 19f;

    [SerializeField] private GameObject _explosionPrefab;

    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Laser"))
        {
            GameObject tempExplosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(col.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject,0.25f);
        }
    }
}
