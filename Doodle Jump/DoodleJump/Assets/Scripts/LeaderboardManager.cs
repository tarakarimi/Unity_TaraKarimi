using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
public class LeaderboardManager : MonoBehaviour
{
    public Transform scoreEntryParent;
    public GameObject scoreEntryPrefab;

    void Start()
    {
        DisplayLeaderboard();
    }

    void DisplayLeaderboard()
    {
        // Clear existing entries
        foreach (Transform child in scoreEntryParent)
        {
            Destroy(child.gameObject);
        }

        // Load and display top scores
        List<ScoreEntry> topScores = LoadTopScores();
        int rank = 1;

        foreach (ScoreEntry scoreEntry in topScores)
        {
            GameObject entry = Instantiate(scoreEntryPrefab, scoreEntryParent);
            entry.GetComponent<ScoreEntryUI>().Setup(rank, scoreEntry.name, scoreEntry.score);
            rank++;
        }
    }

    List<ScoreEntry> LoadTopScores()
    {
        List<ScoreEntry> topScores = new List<ScoreEntry>();

        for (int i = 1; i <= 10; i++)
        {
            string name = PlayerPrefs.GetString("ScoreName" + i, "");
            int score = PlayerPrefs.GetInt("ScoreValue" + i, 0);

            if (!string.IsNullOrEmpty(name) && score > 0)
            {
                topScores.Add(new ScoreEntry(name, score));
            }
        }

        return topScores;
    }
}*/