using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScoreHandler : MonoBehaviour
{
    private string _currentName = "Doodler";
    private int _currentScore = 0;
    [SerializeField] private Text _displayingName;

    public void SetFinalScore(int score)
    {
        _currentScore = score;
    }
    public void SetFinalName()
    {
        _currentName = _displayingName.text;
    }

    public void SaveRecord()
    {
        // Load the existing top scores
        ScoreEntry[] topScores = LoadTopScores();

        // Find if the current name already exists in the top scores
        bool foundName = false;

        for (int i = 0; i < topScores.Length; i++)
        {
            if (_currentName == topScores[i].name)
            {
                foundName = true;

                // If the current score is higher, update the score
                if (_currentScore > topScores[i].score)
                {
                    topScores[i] = new ScoreEntry(_currentName, _currentScore);
                    SaveTopScores(topScores);
                    break;
                }
            }
        }

        // If the current name was not found in top scores, and the current score is eligible,
        // add the name and score as a new entry
        if (!foundName && _currentScore > 0)
        {
            for (int i = 0; i < topScores.Length; i++)
            {
                if (_currentScore > topScores[i].score)
                {
                    // Shift down the existing entries to make room for the new entry
                    for (int j = topScores.Length - 1; j > i; j--)
                    {
                        topScores[j] = topScores[j - 1];
                    }

                    // Add the new entry
                    topScores[i] = new ScoreEntry(_currentName, _currentScore);
                    SaveTopScores(topScores);
                    break;
                }
            }
        }
    }

    

    // Load the top scores from PlayerPrefs
    private ScoreEntry[] LoadTopScores()
    {
        ScoreEntry[] topScores = new ScoreEntry[10];

        for (int i = 1; i <= 10; i++)
        {
            string name = PlayerPrefs.GetString("ScoreName" + i, "Doodler");
            int score = PlayerPrefs.GetInt("ScoreValue" + i, 0);

            //if (!string.IsNullOrEmpty(name) && score > 0)
            //{
                topScores[i - 1] = new ScoreEntry(name, score);
                //}
        }
        return topScores;
    }

    // Save the top scores using PlayerPrefs
    private void SaveTopScores(ScoreEntry[] scores)
    {
        for (int i = 0; i < scores.Length; i++)
        {
            PlayerPrefs.SetString("ScoreName" + (i + 1), scores[i].name);
            PlayerPrefs.SetInt("ScoreValue" + (i + 1), scores[i].score);
        }
    }

    public class ScoreEntry
    {
        public string name;
        public int score;

        public ScoreEntry(string playerName, int playerScore)
        {
            name = playerName;
            score = playerScore;
        }
    }

}
