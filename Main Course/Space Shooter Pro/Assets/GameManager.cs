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
            if (isMultiPlayerMode)
            {
                SceneManager.LoadScene(2); //game scene
            }
            else
            {
                SceneManager.LoadScene(1); //game scene   
            }
        }
        
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Application.Quit();
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
