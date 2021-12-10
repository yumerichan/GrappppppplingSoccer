using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalScore : MonoBehaviour
{
    //  得点表示用
    public Text _redScoreText;
    public Text _buleScoreText;

    //  得点
    private int _redScore;
    private int _buleScore;

    private bool _GetScore;

    public void Update()
    {
        if (!_GetScore) return;

        PhotonCharaView view = GameObject.Find("CharaViewManager(Clone)").
                        GetComponent<PhotonCharaView>();


        _redScoreText.text = view.RedScore.ToString();
        _buleScoreText.text = view.BlueScore.ToString();

        _GetScore = false;
    }

        //  赤チームスコア加算
    public void AddRedScore()
    {
        _redScore += 1;
        _GetScore = true;
    }

    //  青チームスコア加算
    public void AddBuleScore()
    {
        _buleScore += 1;

        _GetScore = true;
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
