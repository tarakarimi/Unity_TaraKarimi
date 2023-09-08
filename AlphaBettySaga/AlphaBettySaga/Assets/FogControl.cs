using System.Collections;
using UnityEngine;

public class FogControl : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private void OnEnable()
    {
        particleSystem = GetComponent<ParticleSystem>();
        StartCoroutine(StopEmission());
    }

    private IEnumerator StopEmission()
    {
        yield return new WaitForSeconds(2f);
        particleSystem.Stop();
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}