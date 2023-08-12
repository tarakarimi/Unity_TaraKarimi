using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class JetpackPowerup : MonoBehaviour
{
    private float jetpackForce;
    private float jetpackDuration;
    private Player playerController;
    private GameObject attachedJetpack;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private GameObject jetpackVisual;
    [SerializeField] private GameObject FallingBroomPrefab;
    private int childNum;
    private AudioSource _audioSource;
    private GameManagerScript _gameManagerScript;
    private bool isCollected;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = transform.GetComponent<AudioSource>();
        _gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        if (transform.tag == "Helicopter")
        {
            childNum = 3;
            jetpackForce = 30f;
            jetpackDuration = 2f;
        }
        if (transform.tag == "JetpackPowerup")
        {
            childNum = 2;
            jetpackForce = 80f;
            jetpackDuration = 5.5f;
        }
    }

    private void Update()
    {
        if (_gameManagerScript.isGameOver)
        {
            Destroy(this.gameObject);
        }
        
        float cameraBottomY = Camera.main.transform.position.y - Camera.main.orthographicSize;
        if (transform.position.y < cameraBottomY && !isCollected)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isCollected = true;
            jetpackVisual = other.transform.GetChild(childNum).gameObject;
            jetpackVisual.SetActive(true);
            rb = other.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, jetpackForce);
            playerController = other.GetComponent<Player>();
            playerController.Immunity();
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
            if (_audioSource != null)
            {
                _audioSource.Play();
            }
            StartCoroutine(DeActivateJetpack());
        }
    }

    private IEnumerator DeActivateJetpack()
    {
        playerController.ResetImmunity(jetpackDuration);
        yield return new WaitForSeconds(jetpackDuration);
        //jetpackVisual.transform.parent = null;
        Instantiate(FallingBroomPrefab, playerController.transform.position, Quaternion.identity);
        jetpackVisual.SetActive(false);
        Destroy(this.gameObject);
    }
}
