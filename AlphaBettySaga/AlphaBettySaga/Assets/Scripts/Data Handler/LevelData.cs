using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    public int movesNumber;
    public int scoreGoal;
    public int gridSize;
    public List<string> wordList;
    public List<string> wordListFarsi;
}