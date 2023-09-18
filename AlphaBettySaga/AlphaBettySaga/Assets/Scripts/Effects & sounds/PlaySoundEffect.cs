using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEffect : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private TileInteractionHandler TIH;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateTouch()
    {
        if (TIH != null)
        {
            TIH.isTouchActive = true;
        }
        
    }
    public void PlaySFX()
    {
        _audioSource.Play();
    }
}
