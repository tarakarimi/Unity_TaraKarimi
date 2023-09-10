using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData[] levels; 
    private int currentLevel = 0; // track the current level
    private GameManager GM;

    private void Start()
    {
        
    }

    public void StartGame()
    {
        if (currentLevel < levels.Length)
        {
            LevelData levelData = levels[currentLevel];
            int moves = levelData.movesNumber;
            int scoreGoal = levelData.scoreGoal;
            GM = GetComponent<GameManager>();
            GM.SetLevelParameters(moves, scoreGoal);
        }
    }

}