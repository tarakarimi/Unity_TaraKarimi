using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Sprite selectedSprite;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                if (hit.collider != null)
                {
                    Tile tile = hit.collider.GetComponent<Tile>();
                    if (tile != null) {
                        tile.ChangeToSelectedSprite(); // Change the sprite only for the specific tile
                    }
                }
            }
        }
    
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null) {
                Tile tile = hit.collider.GetComponent<Tile>();
                if (tile != null) {
                    tile.ChangeToSelectedSprite(); // Change the sprite only for the specific tile
                }
            }
        }
    }


    public void ChangeToSelectedSprite()
    {
        _spriteRenderer.sprite = selectedSprite;
    }
    

}