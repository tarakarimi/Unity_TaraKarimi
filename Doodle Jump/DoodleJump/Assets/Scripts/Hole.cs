using System;
using System.Collections;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private GameManagerScript _gameManagerScript;
    private bool isAbsorbing = false;
    private float shrinkSpeed = 0.05f;
    private float moveSpeed = 5f; // Adjust the movement speed as needed
    private AudioSource audioSource;

    private void Start()
    {
        _gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float cameraBottomY = Camera.main.transform.position.y - Camera.main.orthographicSize;
        if (transform.position.y + 0.8f < cameraBottomY)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isAbsorbing)
        {
            audioSource.Play();
            isAbsorbing = true;
            other.transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(ShrinkAndAbsorbPlayer(other.gameObject));
        }
    }

    IEnumerator ShrinkAndAbsorbPlayer(GameObject player)
    {
        Vector3 originalScale = player.transform.localScale;

        // Disable player movement and control
        Player playerController = player.GetComponent<Player>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Disable player gravity by disabling the Rigidbody2D component
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            playerRigidbody.gravityScale = 0f;
        }
        Vector3 startPosition = player.transform.position;
        Vector3 endPosition = transform.position;
        float startTime = Time.time;
        while (player.transform.localScale.x > 0)
        {
            Vector3 newScale = player.transform.localScale - new Vector3(shrinkSpeed, shrinkSpeed, 0f);
            player.transform.localScale = newScale;
            float t = (Time.time - startTime) * moveSpeed;
            player.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return new WaitForFixedUpdate();
        }

        // Set the scale to 0 in case of any slight overshooting
        player.transform.localScale = Vector3.zero;

        // Ensure the player is precisely positioned at the hole's position
        player.transform.position = endPosition;

        if (_gameManagerScript.isGameOver == false)
        {
            _gameManagerScript.GameOverActions();
        }

        // Enable player movement, control, and gravity after absorption
        if (playerController != null)
        {
            playerController.enabled = true;
        }
        if (playerRigidbody != null)
        {
            playerRigidbody.gravityScale = 1f;
        }
    }
}
