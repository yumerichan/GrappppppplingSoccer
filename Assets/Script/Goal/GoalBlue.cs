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
        if (other.tag == "Ball")
        {
            Canvas canvas = GameObject.FindGameObjectWithTag("GoalCanvas").GetComponent<Canvas>();
            _goalImage = canvas.transform.Find("GoalImage").GetComponent<Image>();

            GoalScore score = GameObject.Find("Score&Timer").GetComponent<GoalScore>();

            PhotonCharaView view = GameObject.Find("CharaViewManager(Clone)").
                        GetComponent<PhotonCharaView>();

            score.AddBuleScore();

            view.RedScore = score.GetRedScore();

            //  ゴール演出リクエスト
            _goalImage.GetComponent<PhotonView>().RPC("RequestGoalDirecting", RpcTarget.All);
        }
    }
}
