using UnityEngine;
using UnityEngine.UI;

public class ChangeNameInput : MonoBehaviour
{
    public Text playerNameText;
    private InputField inputField;
    private SaveScoreHandler _saveScoreHandler;
    private void Start()
    {
        inputField = GetComponent<InputField>();
        inputField.onEndEdit.AddListener(ChangePlayerName);
        _saveScoreHandler = GameObject.Find("GameOverCanvas").GetComponent<SaveScoreHandler>();
    }

    private void ChangePlayerName(string newName)
    {
        playerNameText.text = newName;
        _saveScoreHandler.SetFinalName();
    }
}