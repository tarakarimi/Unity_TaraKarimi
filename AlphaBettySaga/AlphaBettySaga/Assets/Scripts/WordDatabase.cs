using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class WordDatabase : MonoBehaviour
{
    private List<string> validWords = new List<string>();
    [SerializeField] private TextAsset textAsset; 
    void Start()
    {
        
    }

    public void LoadWordsFromFile()
    {
        //TextAsset textAsset = Resources.Load<TextAsset>(fileName);
        if (textAsset != null)
        {
            string[] lines = textAsset.text.Split('\n');
            foreach (string line in lines)
            {
                string word = line.Trim();
                if (!string.IsNullOrEmpty(word) && word.Length > 2)
                {
                    validWords.Add(word);
                    Debug.Log("count: "+ validWords.Count + "time: " + Time.time);
                }
            }
        }
        else
        {
            Debug.LogError("no data");
        }
    }

    public bool IsWordValid(string word)
    {
        return validWords.Contains(word);
    }
}