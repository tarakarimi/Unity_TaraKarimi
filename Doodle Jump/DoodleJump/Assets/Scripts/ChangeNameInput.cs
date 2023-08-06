using UnityEngine;
using UnityEngine.UI;

public class ChangeNameInput : MonoBehaviour
{
    public Text playerNameText;

    private InputField inputField;

    private void Start()
    {
        inputField = GetComponent<InputField>();
        inputField.onEndEdit.AddListener(ChangePlayerName);
    }

    private void ChangePlayerName(string newName)
    {
        playerNameText.text = newName;
    }
}