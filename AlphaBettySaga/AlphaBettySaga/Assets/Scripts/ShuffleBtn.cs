using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ShuffleBtn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite disabledSprite, enabledSprite;
    private Image img;
    [SerializeField] private TileInteractionHandler tileInteraction;
    private bool shuffled = false;
    private Button btn;
    void Start()
    {
        img = GetComponent<Image>();
        btn = GetComponent<Button>();
    }
    public void Clicked()
    {
        if (tileInteraction.isTouchActive && tileInteraction.isGameOver == false)
        {
            if (!shuffled)
            {
                img.sprite = disabledSprite;
                tileInteraction.ShuffleTiles();
                shuffled = true;
                btn.interactable = false;
                StartCoroutine(EnableShuffleBtn());
            }
        }
    }

    IEnumerator EnableShuffleBtn()
    {
        yield return new WaitForSeconds(30f);
        btn.interactable = true;
        shuffled = false;
        img.sprite = enabledSprite;
    }
}
