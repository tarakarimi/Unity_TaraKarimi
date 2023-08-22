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
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _letterGenerator = new LetterGenerator();
        letter = _letterGenerator.GetRandomLetter();
        UpdateTileLetter(letter);
    }

    private void Update()
    {
        
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

}