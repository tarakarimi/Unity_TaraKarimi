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
        //load in a list
        ScoreEntry[] topScores = LoadTopScores();
        
        //compare
        //bool isHighScore = false;

        for (int i = 0; i < topScores.Length; i++)
        {
            if (_currentScore > topScores[i].score)
            {
                //isHighScore = true;
                topScores[i] = new ScoreEntry(_currentName, _currentScore);
                SaveTopScores(topScores);
                break;
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
                Debug.Log(topScores);
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
