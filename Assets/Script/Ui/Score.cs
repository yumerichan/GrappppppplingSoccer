using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int GoalScore; //ì¸ÇÍÇÈÇΩÇﬂÇÃî‘çÜÇê›íu
    public TextMeshProUGUI TextDisplay;

    // Start is called before the first frame update
    void Start()
    {
        GoalScore = 0;
        //string SpriteText = GoalScore.ToString();
        //TextDisplay.GetComponent<TextMeshProUGUI>().text = "";
        //for (int i = 0; i <= SpriteText.Length - 1; i++)
        //{
        //    TextDisplay.GetComponent<TextMeshProUGUI>().text += "<sprite=" + SpriteText[i] + ">";
        //}
    }

    public void ScoreChangedNumber(int point)
    {
        //GoalScore = point;
        //string SpriteText = GoalScore.ToString();
        //TextDisplay.GetComponent<TextMeshProUGUI>().text = "";
        //for (int i = 0; i <= SpriteText.Length - 1; i++)
        //{
        //    TextDisplay.GetComponent<TextMeshProUGUI>().text += "<sprite=" + SpriteText[i] + ">";
        //}
    }

    public int GetScore()
    {
        return GoalScore;
    }
}
