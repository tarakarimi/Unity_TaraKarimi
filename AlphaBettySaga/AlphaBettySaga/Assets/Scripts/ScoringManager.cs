using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringManager
{
    public static int CalculateScore(List<Tile> tiles)
    {
        int letterScoreSum = 0;
        foreach (var tile in tiles)
        {
            letterScoreSum += tile.GetScore();
        }
        int wordLength = tiles.Count;
        int totalScore = letterScoreSum * wordLength * 10;
        return totalScore;
    }
}