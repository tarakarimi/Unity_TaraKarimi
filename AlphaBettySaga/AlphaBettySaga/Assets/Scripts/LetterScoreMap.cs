
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

        
        letterScores['آ'] = 1;
        letterScores['ا'] = 1; 
        letterScores['ب'] = 3; 
        letterScores['پ'] = 2; 
        letterScores['ت'] = 1; 
        letterScores['ث'] = 2;
        letterScores['ج'] = 3;
        letterScores['چ'] = 4; 
        letterScores['ح'] = 2; 
        letterScores['خ'] = 4; 
        letterScores['د'] = 2;
        letterScores['ذ'] = 3; 
        letterScores['ر'] = 3; 
        letterScores['ز'] = 1;
        letterScores['ژ'] = 4;
        letterScores['س'] = 3;
        letterScores['ش'] = 2;
        letterScores['ص'] = 2;
        letterScores['ض'] = 3;
        letterScores['ط'] = 1; 
        letterScores['ظ'] = 4; 
        letterScores['ع'] = 3; 
        letterScores['غ'] = 3;
        letterScores['ف'] = 2;
        letterScores['ق'] = 4; 
        letterScores['ک'] = 2; 
        letterScores['گ'] = 3;
        letterScores['ل'] = 1; 
        letterScores['م'] = 2;
        letterScores['ن'] = 3;
        letterScores['و'] = 1;
        letterScores['ه'] = 2; 
        letterScores['ی'] = 1; 
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
