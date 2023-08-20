using UnityEngine;

public class LetterGenerator
{
    public char GetRandomLetter()
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