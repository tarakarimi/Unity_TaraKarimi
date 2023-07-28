using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;

    public bool isMultiPlayerMode = false;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "MultiPlayerMode")
        {
            isMultiPlayerMode = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(1); //game scene
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
