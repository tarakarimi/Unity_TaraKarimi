using System.Collections;
using UnityEngine;

public class FogControl : MonoBehaviour
{
    private ParticleSystem ps;

    private void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        StartCoroutine(StopEmission());
    }

    private IEnumerator StopEmission()
    {
        yield return new WaitForSeconds(2f);
        ps.Stop();
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}