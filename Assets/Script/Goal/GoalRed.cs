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

        if (GameObject.Find("NetWork").GetComponent<NewWorkInfo>().GetNumber() != 0) return;

        if (other.tag == "Ball")
        {
            Canvas canvas = GameObject.FindGameObjectWithTag("GoalCanvas").GetComponent<Canvas>();
            _goalImage = canvas.transform.Find("GoalImage").GetComponent<Image>();

            GoalScore score = GameObject.Find("Score&Timer").GetComponent<GoalScore>();

            PhotonCharaView view = GameObject.Find("CharaViewManager(Clone)").
                         GetComponent<PhotonCharaView>();

            score.AddRedScore();

            //  ゴール演出リクエスト
            canvas.GetComponent<PhotonView>().RPC("RequestRedGoalDirecting", RpcTarget.All);
        }
    }

    private void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Canvas canvas = GameObject.FindGameObjectWithTag("GoalCanvas").GetComponent<Canvas>();
        //    _goalImage = canvas.transform.Find("GoalImage").GetComponent<Image>();

        //    GoalScore score = GameObject.Find("Score&Timer").GetComponent<GoalScore>();

        //    PhotonCharaView view = GameObject.Find("CharaViewManager(Clone)").
        //                 GetComponent<PhotonCharaView>();

        //    score.AddRedScore();

        //    view.RedScore = score.GetRedScore();

        //    //  ゴール演出リクエスト
        //    _goalImage.GetComponent<PhotonView>().RPC("RequestGoalDirecting", RpcTarget.All);
        //}
    }
}
