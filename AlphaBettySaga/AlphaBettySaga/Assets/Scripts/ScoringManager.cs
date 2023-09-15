using System.Collections.Generic;
using UnityEngine;

public class ScoringManager
{
    public static int CalculateScore(List<Tile> tiles)
    {
        int letterScoreSum = 0;
        int goldTileCount = 0;
        foreach (var tile in tiles)
        {
            letterScoreSum += tile.GetScore();
            if (tile.CompareTag("GoldTile"))
            {
                goldTileCount++;
            }
        }
        int wordLength = tiles.Count;

        int totalScore = letterScoreSum * wordLength * 10;
        totalScore *= (int)Mathf.Pow(2, goldTileCount);
        return totalScore;
    }
}