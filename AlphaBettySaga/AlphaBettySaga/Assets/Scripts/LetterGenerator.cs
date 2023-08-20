using UnityEngine;

public class LetterGenerator
{
    public char GetRandomLetter()
    {
        float tmpRnd = Random.Range(0f, 1f);

        if (tmpRnd <= 0.12702f) return 'e';      //0-0.12702 will be 'e'
        else if (tmpRnd <= 0.21758f) return 't'; //0.12702-0.21758 will be 't'
        else if (tmpRnd <= 0.29925f) return 'a';
        else if (tmpRnd <= 0.37432f) return 'o';
        else if (tmpRnd <= 0.44398f) return 'i';
        else if (tmpRnd <= 0.51147f) return 'n';
        else if (tmpRnd <= 0.57474f) return 's';
        else if (tmpRnd <= 0.63568f) return 'h';
        else if (tmpRnd <= 0.69555f) return 'r';
        else if (tmpRnd <= 0.73808f) return 'd';
        else if (tmpRnd <= 0.77833f) return 'l';
        else if (tmpRnd <= 0.80615f) return 'c';
        else if (tmpRnd <= 0.83373f) return 'u';
        else if (tmpRnd <= 0.85779f) return 'm';
        else if (tmpRnd <= 0.88139f) return 'w';
        else if (tmpRnd <= 0.90367f) return 'f';
        else if (tmpRnd <= 0.92382f) return 'g';
        else if (tmpRnd <= 0.94356f) return 'y';
        else if (tmpRnd <= 0.96285f) return 'p';
        else if (tmpRnd <= 0.97777f) return 'b';
        else if (tmpRnd <= 0.98755f) return 'v';
        else if (tmpRnd <= 0.99527f) return 'k';
        else if (tmpRnd <= 0.9968f) return 'j';
        else if (tmpRnd <= 0.9983f) return 'x';
        else if (tmpRnd <= 0.99925f) return 'q';
        else return 'z';

    }
}