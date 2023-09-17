using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite selectedSprite, defaultSprite;
    private SpriteRenderer _spriteRenderer;
    public char letter;
    public int score;
    private LetterGenerator _letterGenerator;
    [SerializeField] private Text tileLetter, tileScore;
    public int row, col;
    public int shiftDownStep;
    private float size;
    private float elapsedTime = 0f;
    private float duration = 0.2f;
    private bool isShifting;
    private TileInteractionHandler _tileInteractionHandler;
    private LetterScoreMap letterScoreMap;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip createdSFX, clickSFX;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject GM = GameObject.Find("GameManager");
        size = GM.GetComponent<GameManager>().tileSize;
        _tileInteractionHandler = GM.GetComponent<TileInteractionHandler>();
        if (CompareTag("WildTile"))
        {
            letter = '*';
        }
        else
        {
            _letterGenerator = LetterGenerator.Instance;
            letter = _letterGenerator.GetRandomLetter();
        }
        SetLetterProperties();
    }

    public void SetLetterProperties()
    {
        tileLetter.text = letter.ToString();
        letterScoreMap = LetterScoreMap.Instance;
        if (CompareTag("Tile") || CompareTag("GoldTile") || CompareTag("Bomb") )
        {
            score = letterScoreMap.GetScoreForLetter(letter);
        }
        else if (CompareTag("SilverTile"))
        {
            score = letterScoreMap.GetScoreForLetter(letter) * 2;
        }
        tileScore.text = score.ToString().ToUpper();
    }

    public void TileSelect()
    {
        _audioSource.clip = clickSFX;
        _audioSource.Play();   
        _spriteRenderer.sprite = selectedSprite;
    }

    public void TileDeSelect()
    {
        _spriteRenderer.sprite = defaultSprite;
    }

    public char GetLetter()
    {
        return letter;
    }
    public int GetScore()
    {
        return score;
    }

    public void SetGridPosition(int rowToBe, int colToBe)
    {
        row = rowToBe;
        col= colToBe;
    }

    public void ShiftDown()
    {
        StartCoroutine(MovingDownCoroutine(0.01f));
    }

    IEnumerator MovingDownCoroutine(float time)
    {
        if (!isShifting)
        {
            isShifting = true;

            yield return new WaitForSeconds(time);
            _tileInteractionHandler.tileMatrix[row, col] = null;
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = startPosition + new Vector3(0, -shiftDownStep * size, 0);
            transform.position = targetPosition;
            
            while (elapsedTime <= duration)
            {
                float t = Mathf.Clamp01(elapsedTime / duration);
                transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
            row -= shiftDownStep;
            //Debug.Log("tile: " + letter + "moves "+shiftDownStep+" steps and new row & col is: "+row+", " + col);
            _tileInteractionHandler.tileMatrix[row, col] = this.gameObject;
            shiftDownStep = 0;
            elapsedTime = 0f;
            isShifting = false;
        }
    }

    public void PlayeCreatedSFX()
    {
        _audioSource.clip = createdSFX;
        _audioSource.Play();
    }
    
}