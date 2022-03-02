using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreKepeer : MonoBehaviour
{
    public static int score = 0;
    Text myText;
    PlayerController PlayerControl;
    private void Start()
    {
        PlayerControl = GameObject.FindObjectOfType<PlayerController>();
        myText = GetComponent<Text>();
        reset();
    }
    public void ScorePoint (int point)
    {
        score += point;
        PlayerControl.CheckForLevelUp(score);
        myText.text = score.ToString();
    }
    public static void reset()
    {
        score = 0;
    }
}
