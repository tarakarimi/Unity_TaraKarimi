using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class JetpackPowerup : MonoBehaviour
{
    private float jetpackForce = 80f;
    private float jetpackDuration = 5f;
    private Player playerController;
    private GameObject attachedJetpack;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private GameObject jetpackVisual;
    [SerializeField] private GameObject FallingBroomPrefab;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jetpackVisual = other.transform.GetChild(2).gameObject;
            jetpackVisual.SetActive(true);
            rb = other.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, jetpackForce);
            playerController = other.GetComponent<Player>();
            playerController.Immunity();
            spriteRenderer.enabled = false;
            StartCoroutine(DeActivateJetpack());
        }
    }

    private IEnumerator DeActivateJetpack()
    {
        playerController.ResetImmunity(5);
        yield return new WaitForSeconds(jetpackDuration);
        //jetpackVisual.transform.parent = null;
        Instantiate(FallingBroomPrefab, playerController.transform.position, Quaternion.identity);
        jetpackVisual.SetActive(false);
        Destroy(this.gameObject);
    }
}
