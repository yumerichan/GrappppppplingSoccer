using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalBlue : MonoBehaviour
{
    [SerializeField]
    private Image _goalImage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            GoalScore score = GameObject.Find("Score&Timer").GetComponent<GoalScore>();

            score.AddBuleScore();

            //  �S�[�����o���N�G�X�g
            _goalImage.GetComponent<GoalDirecting>().RequestGoalDirecting();
        }
    }
}
