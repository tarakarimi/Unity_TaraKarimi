using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStateActivation : MonoBehaviour
{
    public GameObject winCutscene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.HasCard)
            {
                winCutscene.SetActive(true);
            }
            else
            {
                print("You must get the card first");
            }
        }
    }
}
