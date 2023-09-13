using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class WordDatabase : MonoBehaviour
{
    private List<string> validWords = new List<string>();
    private List<string> threeLetterWords = new List<string>();
    private List<string> fourLetterWords = new List<string>();
    private List<string> fiveLetterWords = new List<string>();
    private List<string> otherWords = new List<string>();

    [SerializeField] private TextAsset textAsset;

    private void Start()
    {
        StartCoroutine(LoadWordsCoroutine());
    }

    private IEnumerator LoadWordsCoroutine()
    {
        if (textAsset != null)
        {
            string[] lines = textAsset.text.Split('\n');
            int batchSize = 4500;
            int currentBatch = 0;

            while (currentBatch < lines.Length)
            {
                for (int i = currentBatch; i < currentBatch + batchSize && i < lines.Length; i++)
                {
                    string word = lines[i].Trim();
                    if (!string.IsNullOrEmpty(word) && word.Length > 2)
                    {
                        validWords.Add(word);
                        
                        if (word.Length == 3)
                        {
                            threeLetterWords.Add(word);
                        }
                        else if (word.Length == 4)
                        {
                            fourLetterWords.Add(word);
                        }
                        else if (word.Length == 5)
                        {
                            fiveLetterWords.Add(word);
                        }
                        else
                        {
                            otherWords.Add(word);
                        }
                    }
                }

                currentBatch += batchSize;
                yield return null;
            }
        }

        Debug.Log("Word loading complete. Total words: " + validWords.Count + "time: " + Time.time);
    }

    public bool IsWordValid(string word, int wordLength)
    {
        switch (wordLength)
        {
            case 3:
                return threeLetterWords.Contains(word);
            case 4:
                return fourLetterWords.Contains(word);
            case 5:
                return fiveLetterWords.Contains(word);
            default:
                return otherWords.Contains(word);
        }

    }
}
