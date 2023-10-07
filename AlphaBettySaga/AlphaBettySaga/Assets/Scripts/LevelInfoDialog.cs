using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoDialog : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    public void SetProperties(int level, int goal)
    {
        gameObject.SetActive(true);
        transform.GetChild(2).GetComponent<Text>().text = "Level " + level;
        transform.GetChild(3).GetComponent<Text>().text = "Goal: Score " + goal + " points";
        _animator.Play("LevelInfoDlgBox_anim", 0, 0f);
    }
}
