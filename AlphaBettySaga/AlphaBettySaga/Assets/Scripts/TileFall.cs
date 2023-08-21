using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileFall : MonoBehaviour
{
    private float fallDuration = 0.4f;
    private Vector3 startPos;
    private char letter;

    private LetterGenerator _letterGenerator;

    [SerializeField] private Text tileText;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        _letterGenerator = new LetterGenerator();
        letter = _letterGenerator.GetRandomLetter();
        UpdateTileLetter(letter);
    }

    IEnumerator FallingCoroutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        
        float startTime = Time.time;

        while (Time.time - startTime < fallDuration)
        {
            float progress = (Time.time - startTime) / fallDuration;
            Vector3 currentPos = new Vector3(startPos.x, Mathf.Lerp(startPos.y, startPos.y - 10.0f, progress), startPos.z);
            transform.position = currentPos;
            yield return null;
        }
        transform.position = new Vector3(startPos.x, startPos.y - 10.0f, startPos.z);
    }

    public void StartTileFall(float delayTime)
    {
        
        StartCoroutine(FallingCoroutine(delayTime));
    }

    private void UpdateTileLetter(char letter)
    {
        tileText.text = letter.ToString();
    }
}
