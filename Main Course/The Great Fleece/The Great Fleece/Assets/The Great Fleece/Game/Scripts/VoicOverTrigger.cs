using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicOverTrigger : MonoBehaviour
{
    private AudioSource _audioSource;
    private AudioClip clip;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        clip = _audioSource.clip;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //_audioSource.Play();
            AudioManager.Instance.PlayVoiceOver(clip);
            Destroy(this.gameObject);
        }
    }
}
