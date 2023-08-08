using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject listItemPrefab; // Assign your prefab here
    public int numberOfItems = 10;    // Number of items you want in the list
    [SerializeField] private GameObject recordsParent;
    [SerializeField] private Sprite _imageType1;
    [SerializeField] private Sprite _imageType2;

    void Start()
    {
        PopulateList();
    }

    void PopulateList()
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            GameObject listItem = Instantiate(listItemPrefab, recordsParent.transform);
            listItem.transform.position -= new Vector3(0,i * 70,0);
            listItem.transform.parent = recordsParent.transform;
            Text textComponent = listItem.GetComponentInChildren<Text>();
            textComponent.text = "Item " + (i + 1);
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
}
