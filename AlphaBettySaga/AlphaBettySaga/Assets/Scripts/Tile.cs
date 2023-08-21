using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite selectedSprite, defaultSprite;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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

    

}