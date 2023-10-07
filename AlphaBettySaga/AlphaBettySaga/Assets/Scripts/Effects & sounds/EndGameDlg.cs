using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameDlg : MonoBehaviour
{
    [SerializeField] private TileInteractionHandler TIH;
    private void Start()
    {
        TIH.isGameOver = true;
    }

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