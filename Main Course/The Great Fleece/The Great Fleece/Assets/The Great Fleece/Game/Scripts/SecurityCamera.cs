using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public GameObject gameOverCutscene;
    private MeshRenderer renderer;
    private Color _redColor;
    public Animator anim;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        _redColor = new Color(0.83f,0.18f,0.30f,0.03f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(AlertRoutine());
            anim.enabled = false;
            renderer.material.SetColor("_TintColor",_redColor);
        }
    }

    IEnumerator AlertRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        gameOverCutscene.SetActive(true);
    }
}
