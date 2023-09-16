using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameDlg : MonoBehaviour
{
    private bool isReady = false;

    // Start is called before the first frame update
    public void GoBack()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        if (gameObject.CompareTag("Win"))
        {
            currentLevel++;
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        }

        SceneManager.LoadScene("LevelsScene");
    }
    public void RePlay()
    {
        SceneManager.LoadScene("LevelsScene");
    }
}