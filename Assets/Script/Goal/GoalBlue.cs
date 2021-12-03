using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBlue : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        GoalScore score = GameObject.Find("Score&Timer").GetComponent<GoalScore>();

        score.AddBuleScore();
    }
}
