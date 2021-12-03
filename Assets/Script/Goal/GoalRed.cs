using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalRed : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            GoalScore score = GameObject.Find("Score&Timer").GetComponent<GoalScore>();

            score.AddRedScore();
        }
    }
}
