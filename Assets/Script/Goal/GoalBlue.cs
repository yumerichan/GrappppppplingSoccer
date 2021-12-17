using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GoalBlue : MonoBehaviour
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

            score.AddBuleScore();
            //  ゴール演出リクエスト
            _goalImage.GetComponent<PhotonView>().RPC("RequestGoalDirecting", RpcTarget.All);
        }
    }
}
