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

    private bool _GetScore;

    private float _CheckTime;

    public void Update()
    {

        PhotonCharaView view = GameObject.Find("CharaViewManager(Clone)").
                        GetComponent<PhotonCharaView>();


        _redScoreText.text = view.RedScore.ToString();
        _buleScoreText.text = view.BlueScore.ToString();

        if(_GetScore)
        {
            _CheckTime -= Time.deltaTime;

            if(_CheckTime <= 0.0f)
            {
                _GetScore = false;
            }
        }
    }

    //  �ԃ`�[���X�R�A���Z
    public void AddRedScore()
    {
        if (_GetScore) return;

        PhotonCharaView view = GameObject.Find("CharaViewManager(Clone)").
                       GetComponent<PhotonCharaView>();
        view.RedScore = view.RedScore + 1;
        _GetScore = true;
        _CheckTime = 2.0f;
    }

    //  �`�[���X�R�A���Z
    public void AddBuleScore()
    {
        if (_GetScore) return;

        PhotonCharaView view = GameObject.Find("CharaViewManager(Clone)").
                      GetComponent<PhotonCharaView>();
        view.BlueScore = view.BlueScore + 1;

        _GetScore = true;
        _CheckTime = 2.0f;
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
