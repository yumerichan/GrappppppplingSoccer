using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalScore : MonoBehaviour
{
    //  ���_�\���p
    public Text _redScoreText;
    public Text _buleScoreText;

    //  ���_
    private int _redScore;
    private int _buleScore;

    public void Update()
    {
        if (GameObject.Find("CharaViewManager(Clone)").
                         GetComponent<PhotonCharaView>() == null)return;

        PhotonCharaView view = GameObject.Find("CharaViewManager(Clone)").
                        GetComponent<PhotonCharaView>();


        _redScoreText.text = view.RedScore.ToString();
        _buleScoreText.text = view.BlueScore.ToString();
    }

        //  �ԃ`�[���X�R�A���Z
    public void AddRedScore()
    {
        _redScore += 1;   
    }

    //  �`�[���X�R�A���Z
    public void AddBuleScore()
    {
        _buleScore += 1; 
    }

    public int GetRedScore()
    {
        return _redScore;
    }

    public int GetBlueScore()
    {
        return _buleScore;
    }
}
