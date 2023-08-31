using System.Collections.Generic;
using UnityEngine;

public class LetterScoreMap
{
    private static LetterScoreMap instance;
    private Dictionary<char, int> letterScores = new Dictionary<char, int>();

    private LetterScoreMap()
    {
        InitializeLetterScores();
    }

    public static LetterScoreMap Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LetterScoreMap();
                Debug.Log("created");
            }
            return instance;
        }
    }

    private void InitializeLetterScores()
    {
        letterScores['a'] = 1;
        letterScores['b'] = 3;
        letterScores['c'] = 2;
        letterScores['d'] = 2;
        letterScores['e'] = 1;
        letterScores['f'] = 3;
        letterScores['g'] = 3;
        letterScores['h'] = 2;
        letterScores['i'] = 1;
        letterScores['j'] = 4;
        letterScores['k'] = 4;
        letterScores['l'] = 2;
        letterScores['m'] = 3;
        letterScores['n'] = 3;
        letterScores['o'] = 1;
        letterScores['p'] = 3;
        letterScores['q'] = 4;
        letterScores['r'] = 3;
        letterScores['s'] = 3;
        letterScores['t'] = 1;
        letterScores['u'] = 2;
        letterScores['v'] = 3;
        letterScores['w'] = 3;
        letterScores['x'] = 4;
        letterScores['y'] = 3;
        letterScores['z'] = 4;
    }

    public int GetScoreForLetter(char letter)
    {
        int score;
        
        if (letterScores.TryGetValue(letter, out score))
        {
            return score;
        }
        else
        {
            return 0;
        }
    }
}