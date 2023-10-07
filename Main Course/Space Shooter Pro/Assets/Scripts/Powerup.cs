using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int powerupID; //0:tripleshot  //1:speed  //2:shield
    [SerializeField] private AudioClip _collectPowerupSFX;
    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldPowerActive();
                        break;
                    default:
                        Debug.Log("Default value");
                        break;
                }
            }
            AudioSource.PlayClipAtPoint(_collectPowerupSFX, transform.position);
            Destroy(this.gameObject);
        }
    }
}
