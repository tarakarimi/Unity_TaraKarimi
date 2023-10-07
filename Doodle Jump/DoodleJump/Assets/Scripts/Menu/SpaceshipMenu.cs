using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class SpaceshipMenu : MonoBehaviour
{
    float loopRadius = .8f; 
    float rotationSpeed = 0.7f; 
    Vector3 startingPosition; 

    private float elapsedTime = 0.0f;
    [SerializeField] private GameObject _light;

    private void Start()
    {
        startingPosition = transform.position;
        StartCoroutine(SpaceshipLight());
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        
        float x = Mathf.Cos(elapsedTime * rotationSpeed) * loopRadius;
        float y = Mathf.Sin(elapsedTime * rotationSpeed * 2) * (loopRadius * 0.5f); 

        transform.position = startingPosition + new Vector3(x, y, 0.0f);
    }

    IEnumerator SpaceshipLight()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.4f);
            _light.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            _light.SetActive(true);
        }
    }

}