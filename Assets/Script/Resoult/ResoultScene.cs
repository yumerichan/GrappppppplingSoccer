using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResoultScene : MonoBehaviour
{
    //  =======スコア倍率=============

    //  得点      　：800
    //  アシスト    ：300
    //  スキル発動  ：100

    //================================

    private int _goalScore = 800;
    private int _assistScore = 300;
    private int _skillScore = 100;

    private int _myScore;           //自分のチームのスコア
    private int _opponentScore;     //相手のスコア

    private int[] _goalNum;         //チームのゴールの数
    private int[] _assistNum;       //チームのアシストの数
    private int[] _skillNum;        //チームのスキルアシストの数
    private int[] _sumScore;        //スコアの合計

    // Start is called before the first frame update
    void Start()
    {
        //  プレイシーンからスコアの情報をもってくる
    }

    // Update is called once per frame
    void Update()
    {

    }
}
