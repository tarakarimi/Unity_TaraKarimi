using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LetterGenerator : MonoBehaviour
{
    private static LetterGenerator instance;
    private bool isFarsiLanguage;
    private string languagePreference;
    public static LetterGenerator Instance
    {
        get
        {
            if (instance == null)
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(LetterGenerator).Name;
                    instance = obj.AddComponent<LetterGenerator>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    public char GetRandomLetter()
    {
        languagePreference = PlayerPrefs.GetString("LanguagePreference", "English");
        isFarsiLanguage = (languagePreference == "Farsi");
        if (isFarsiLanguage)
        {
            float tmpRnd = Random.Range(0.0684f, 1.0f);

            if (tmpRnd >= 0.8600f) return 'ا';
            if (tmpRnd >= 0.7700f) return 'ی';
            if (tmpRnd >= 0.6900f) return 'ن';
            if (tmpRnd >= 0.6140f) return 'ر';
            if (tmpRnd >= 0.5390f) return 'د';
            if (tmpRnd >= 0.4740f) return 'م';
            if (tmpRnd >= 0.4130f) return 'ه';
            if (tmpRnd >= 0.3550f) return 'و';
            if (tmpRnd >= 0.3130f) return 'ت';
            if (tmpRnd >= 0.2720f) return 'ب';
            if (tmpRnd >= 0.2500f) return 'س';
            if (tmpRnd >= 0.2290f) return 'ش';
            if (tmpRnd >= 0.2090f) return 'ک';
            if (tmpRnd >= 0.1890f) return 'ز';
            if (tmpRnd >= 0.1690f) return 'ل';
            if (tmpRnd >= 0.1570f) return 'گ';
            if (tmpRnd >= 0.1460f) return 'ق';
            if (tmpRnd >= 0.1350f) return 'ف';
            if (tmpRnd >= 0.1245f) return 'خ';
            if (tmpRnd >= 0.1145f) return 'ع';
            if (tmpRnd >= 0.1045f) return 'ح';
            if (tmpRnd >= 0.0951f) return 'ج';
            if (tmpRnd >= 0.0901f) return 'پ';
            if (tmpRnd >= 0.0861f) return 'چ';
            if (tmpRnd >= 0.0831f) return 'ض';
            if (tmpRnd >= 0.0791f) return 'ط';
            if (tmpRnd >= 0.0751f) return 'ص';
            if (tmpRnd >= 0.0731f) return 'غ';
            if (tmpRnd >= 0.0711f) return 'ظ';
            if (tmpRnd >= 0.0701f) return 'ث';
            if (tmpRnd >= 0.0691f) return 'ذ';
            else return 'ژ';
        }
        else
        {
            float tmpRnd = Random.Range(0f, 1f);
            //Setting the ranges: 'e': 0 - 0.12702 , 't': 0.12702-0.21758 , ...
            if (tmpRnd <= 0.12702f) return 'e';
            if (tmpRnd <= 0.21758f) return 't';
            if (tmpRnd <= 0.29925f) return 'a';
            if (tmpRnd <= 0.37432f) return 'o';
            if (tmpRnd <= 0.44398f) return 'i';
            if (tmpRnd <= 0.51147f) return 'n';
            if (tmpRnd <= 0.57474f) return 's';
            if (tmpRnd <= 0.63568f) return 'h';
            if (tmpRnd <= 0.69555f) return 'r';
            if (tmpRnd <= 0.73808f) return 'd';
            if (tmpRnd <= 0.77833f) return 'l';
            if (tmpRnd <= 0.80615f) return 'c';
            if (tmpRnd <= 0.83373f) return 'u';
            if (tmpRnd <= 0.85779f) return 'm';
            if (tmpRnd <= 0.88139f) return 'w';
            if (tmpRnd <= 0.90367f) return 'f';
            if (tmpRnd <= 0.92382f) return 'g';
            if (tmpRnd <= 0.94356f) return 'y';
            if (tmpRnd <= 0.96285f) return 'p';
            if (tmpRnd <= 0.97777f) return 'b';
            if (tmpRnd <= 0.98755f) return 'v';
            if (tmpRnd <= 0.99527f) return 'k';
            if (tmpRnd <= 0.9968f) return 'j';
            if (tmpRnd <= 0.9983f) return 'x';
            if (tmpRnd <= 0.99925f) return 'q';
            return 'z';
        }
    }
}