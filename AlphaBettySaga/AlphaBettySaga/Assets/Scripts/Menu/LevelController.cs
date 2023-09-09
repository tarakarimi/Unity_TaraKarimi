using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject levelInfoPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackBtn()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OpenLevelInfo()
    {
        Debug.Log("called");
        levelInfoPrefab.SetActive(true);
        Debug.Log("set true");
    }

    public void GameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
