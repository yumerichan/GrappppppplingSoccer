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

    public GameObject _goal;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        //  プレイシーンからスコアの情報をもってくる
        _rb = transform.GetComponent<Rigidbody>();
        _rb.angularVelocity = new Vector3(0, 5, 0);
    }

    // Update is called once per frame
    void Update()
    {

        //  デバッグ用　必ず消す
        if (Input.GetKeyDown(KeyCode.Return))
            _goal.GetComponent<GoalDirecting>().RequestGoalDirecting();

        _rb.velocity = new Vector3(0, 0, 0);
    }
}
