using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GoalRed : MonoBehaviour
{
    [SerializeField]
    private Image _goalImage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            Canvas canvas = GameObject.FindGameObjectWithTag("GoalCanvas").GetComponent<Canvas>();
            _goalImage = canvas.transform.Find("GoalImage").GetComponent<Image>();

            GoalScore score = GameObject.Find("Score&Timer").GetComponent<GoalScore>();

            score.AddRedScore();

            //  ゴール演出リクエスト
            //_goalImage.GetComponent<GoalDirecting>().RequestGoalDirecting();
            _goalImage.GetComponent<PhotonView>().RPC("RequestGoalDirecting", RpcTarget.All);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Canvas canvas = GameObject.FindGameObjectWithTag("GoalCanvas").GetComponent<Canvas>();
            _goalImage = canvas.transform.Find("GoalImage").GetComponent<Image>();

            GoalScore score = GameObject.Find("Score&Timer").GetComponent<GoalScore>();

            score.AddRedScore();

            //  ゴール演出リクエスト
            //_goalImage.GetComponent<GoalDirecting>().RequestGoalDirecting();
            _goalImage.GetComponent<PhotonView>().RPC("RequestGoalDirecting", RpcTarget.All);
        }
    }
}
