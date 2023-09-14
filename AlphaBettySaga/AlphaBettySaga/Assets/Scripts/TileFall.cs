using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileFall : MonoBehaviour
{
    private float fallDuration = 0.4f;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator FallingCoroutine(float delayTime, float ypos)
    {
        yield return new WaitForSeconds(delayTime);
        
        float startTime = Time.time;

        while (Time.time - startTime < fallDuration)
        {
            float progress = (Time.time - startTime) / fallDuration;
            Vector3 currentPos = new Vector3(startPos.x, Mathf.Lerp(startPos.y, startPos.y - ypos, progress), startPos.z);
            transform.position = currentPos;
            yield return null;
        }
        transform.position = new Vector3(startPos.x, startPos.y - ypos, startPos.z);
        GetComponent<Tile>().PlayeCreatedSFX();
    }

    public void StartTileFall(float delayTime, float ypos)
    {
        startPos = transform.position;
        StartCoroutine(FallingCoroutine(delayTime,ypos));
    }
}
