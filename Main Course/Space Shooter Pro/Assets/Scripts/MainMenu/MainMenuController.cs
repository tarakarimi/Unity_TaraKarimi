using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadSinglePlayerGame()
    {
        SceneManager.LoadScene("SinglePlayerMode");
    }
    public void LoadMultiPlayerGame()
    {
        SceneManager.LoadScene("MultiPlayerMode");
    }
}
