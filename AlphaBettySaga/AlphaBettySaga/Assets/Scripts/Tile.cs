using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite selectedSprite, defaultSprite;
    private SpriteRenderer _spriteRenderer;
    private char letter;
    private LetterGenerator _letterGenerator;
    [SerializeField] private Text tileLetter;
    public int row, col;
    public int shiftDownStep;
    private float size;
    private float elapsedTime = 0f;
    private float duration = 0.25f;
    private bool isShifting;
    private TileInteractionHandler _tileInteractionHandler;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _letterGenerator = new LetterGenerator();
        letter = _letterGenerator.GetRandomLetter();
        UpdateTileLetter(letter);
        GameObject GM = GameObject.Find("GameManager");
        size = GM.GetComponent<GameManager>().tileSize;
        _tileInteractionHandler = GM.GetComponent<TileInteractionHandler>();
    }

    public void TileSelect()
    {
        _spriteRenderer.sprite = selectedSprite;
    }

    public void TileDeSelect()
    {
        _spriteRenderer.sprite = defaultSprite;
    }

    private void UpdateTileLetter(char letter)
    {
        tileLetter.text = letter.ToString();
    }

    public char GetLetter()
    {
        return letter;
    }

    public void SetGridPosition(int x, int y)
    {
        row = y;
        col = x;
    }

    public void ShiftDown()
    {
        StartCoroutine(MovingDownCoroutine(0.1f));
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
            _tileInteractionHandler.tileMatrix[row, col] = this.gameObject;
            shiftDownStep = 0;
            elapsedTime = 0f;
            isShifting = false;
        }
    }
}