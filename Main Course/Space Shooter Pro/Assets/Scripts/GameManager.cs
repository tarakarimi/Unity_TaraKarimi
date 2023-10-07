using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;

    public bool isMultiPlayerMode = false;

    [SerializeField] private GameObject _pauseMenuPanel;
    Animator _pausePanelAnimator; 
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "MultiPlayerMode")
        {
            isMultiPlayerMode = true;
        }

        _pausePanelAnimator = GameObject.Find("Pause_Menu").GetComponent<Animator>();
        if (_pauseMenuPanel == null)
        {
            Debug.Log("Pause panel animator not found");
        }
        _pausePanelAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
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
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenuPanel.SetActive(true);
            _pausePanelAnimator.SetBool("isPaused",true);
            Time.timeScale = 0;
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
