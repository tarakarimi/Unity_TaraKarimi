using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] Image onButtonImage_shooting; // Reference to the Image component of the ON button
    [SerializeField] Image offButtonImage_shooting; // Reference to the Image component of the OFF button
    [SerializeField] Sprite onSprite_green; // Reference to the ON sprite image
    [SerializeField] Sprite onSprite_gray;
    [SerializeField] Sprite offSprite_green; // Reference to the OFF sprite image
    [SerializeField] Sprite offSprite_gray;
    
    [SerializeField] Image onButtonImage_sound;
    [SerializeField] Image offButtonImage_sound;

    private const string ShootingModeKey = "ShootingMode"; // Key for PlayerPrefs
    private const string SoundModeKey = "SoundMode";
    private void Start()
    {
        // Load the shooting mode setting from PlayerPrefs and set the UI accordingly
        bool isShootingModeOn = PlayerPrefs.GetInt(ShootingModeKey, 1) == 1; // Default to ON
        UpdateUIShooting(isShootingModeOn);
        bool isSoundModeOn = PlayerPrefs.GetInt(SoundModeKey, 1) == 1; // Default to ON
        UpdateSoundUI(isSoundModeOn);

        // Add click event listeners to the buttons
        onButtonImage_shooting.GetComponent<Button>().onClick.AddListener(OnShootingButtonClick);
        offButtonImage_shooting.GetComponent<Button>().onClick.AddListener(OffShootingButtonClick);
        
        onButtonImage_sound.GetComponent<Button>().onClick.AddListener(OnSoundButtonClick);
        offButtonImage_sound.GetComponent<Button>().onClick.AddListener(OffSoundButtonClick);
    }
    
    //shootings
    private void OnShootingButtonClick()
    {
        UpdateUIShooting(true);
    }

    private void OffShootingButtonClick()
    {
        UpdateUIShooting(false);
    }

    private void UpdateUIShooting(bool isShootingModeOn)
    {
        if (isShootingModeOn)
        {
            onButtonImage_shooting.sprite = onSprite_green;
            offButtonImage_shooting.sprite = offSprite_gray;
        }
        else
        {
            onButtonImage_shooting.sprite = onSprite_gray;
            offButtonImage_shooting.sprite = offSprite_green;
        }

        PlayerPrefs.SetInt(ShootingModeKey, isShootingModeOn ? 1 : 0);
        PlayerPrefs.Save();
    }
    
    //Sounds
    private void OnSoundButtonClick()
    {
        UpdateSoundUI(true);
    }

    private void OffSoundButtonClick()
    {
        UpdateSoundUI(false);
    }

    private void UpdateSoundUI(bool isSoundOn)
    {
        if (isSoundOn)
        {
            onButtonImage_sound.sprite = onSprite_green;
            offButtonImage_sound.sprite = offSprite_gray;
        }
        else
        {
            onButtonImage_sound.sprite = onSprite_gray;
            offButtonImage_sound.sprite = offSprite_green;
        }

        PlayerPrefs.SetInt(SoundModeKey, isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
    }
    
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}