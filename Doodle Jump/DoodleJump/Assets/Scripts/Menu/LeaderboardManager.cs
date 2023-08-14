using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject listItemPrefab;
    public int numberOfItems = 10;
    [SerializeField] private GameObject recordsParent;
    [SerializeField] private Sprite _imageType1;
    [SerializeField] private Sprite _imageType2;


    void Start()
    {
        PopulateList();
    }

    void PopulateList()
    {
        ScoreEntry[] topScores = LoadTopScores();

        for (int i = 0; i < numberOfItems; i++)
        {
            GameObject listItem = Instantiate(listItemPrefab, recordsParent.transform);
            listItem.transform.position -= new Vector3(0, i * 70, 0);
            listItem.transform.parent = recordsParent.transform;
        
            // Find the child Text components for ranking, name, and score
            Text rankText = listItem.transform.Find("RankText").GetComponent<Text>();
            Text nameText = listItem.transform.Find("NameText").GetComponent<Text>();
            Text scoreText = listItem.transform.Find("ScoreText").GetComponent<Text>();

            // Calculate the ranking (1-based index)
            int ranking = i + 1;

            // Check if there is data for this index
            if (i < topScores.Length && topScores[i] != null)
            {
                rankText.text = ranking.ToString() + ". ";
                nameText.text = topScores[i].name;
                scoreText.text = topScores[i].score.ToString();
            }
            else
            {
                rankText.text = ranking.ToString();
                nameText.text = "No Data";
                scoreText.text = "";
            }

            if (i % 2 == 0)
            {
                listItem.GetComponent<Image>().sprite = _imageType2;
            }
        }
    }



    
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    private ScoreEntry[] LoadTopScores()
    {
        ScoreEntry[] topScores = new ScoreEntry[10];

        for (int i = 1; i <= 10; i++)
        {
            string name = PlayerPrefs.GetString("ScoreName" + i, "Doodler");
            int score = PlayerPrefs.GetInt("ScoreValue" + i, 0);
            
            topScores[i - 1] = new ScoreEntry(name, score);
            Debug.Log(topScores);
        }
        return topScores;
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
