using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalRed : MonoBehaviour
{
    [SerializeField]
    private Image _goalImage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            GoalScore score = GameObject.Find("Score&Timer").GetComponent<GoalScore>();

            score.AddRedScore();

            //  ゴール演出リクエスト
            _goalImage.GetComponent<GoalDirecting>().RequestGoalDirecting();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            GoalScore score = GameObject.Find("Score&Timer").GetComponent<GoalScore>();

            score.AddRedScore();

            //  ゴール演出リクエスト
            _goalImage.GetComponent<GoalDirecting>().RequestGoalDirecting();
        }
    }
}
