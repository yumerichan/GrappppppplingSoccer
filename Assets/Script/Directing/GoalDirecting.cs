using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GoalDirecting : MonoBehaviour
{
    [SerializeField]
    private float _dispTime; //表示する時間
    [SerializeField]
    private float _moveSpeed;   //Uiが移動する速さ
    [SerializeField]
    private float _slowTime;    //ゲーム時間がどれだけ遅くなるか
    [SerializeField]
    private Vector3 _initGoalPos;   //移動前の初期位置
    [SerializeField]
    private Vector3 _initTeamPos;  //移動前の初期位置
    [SerializeField]
    private Image _goalImage;        //ゴール画像
    [SerializeField]
    private Image[] _teamImages;        //ゴールしたチーム画像

    private RectTransform _Goalrect;   //Ui移動用座標
    private RectTransform _Teamrect;   //チームUi移動用座標

    private float _dispCount;  //表示時間カウント

    private enum Phase
    { 
        Idle,       //待機
        Move,       //移動
        Display,    //表示
    }
    private Phase _phase;

    private enum GoalTeam
    {
        TeamNone = -1,

        Red,
        Blue,
    }
    private GoalTeam _goalTeam;

    // Start is called before the first frame update
    void Start()
    {
        _Goalrect = _goalImage.GetComponent<RectTransform>();
        _phase = Phase.Idle;
        _dispCount = 0.0f;
        _goalTeam = GoalTeam.TeamNone;
    }

    // Update is called once per frame
    void Update()
    {
        //  フェーズごとの処理分け
        switch (_phase)
        {
            //  待機
            case Phase.Idle:
                {
                    //  特になし
                }
                break;

            //  移動
            case Phase.Move: 
                {
                    //  ゴールUI移動
                    Vector3 pos = _Goalrect.localPosition;
                    pos.x -= _moveSpeed * Time.unscaledDeltaTime * 15;

                    //  チームUI移動
                    Vector3 team_pos = _Teamrect.localPosition;
                    team_pos.x += _moveSpeed * Time.unscaledDeltaTime * 15;


                    //  座標を反映
                    _Goalrect.localPosition = pos;
                    _Teamrect.localPosition = team_pos;

                    //  移動終わり
                    if (pos.x < 0)
                    {
                        //  表示フェーズへ
                        pos.x = 0.0f;
                        _phase = Phase.Display;
                    }

                }
                break;

            //  表示
            case Phase.Display:
                {
                    //   表示時間をカウント
                    _dispCount += Time.unscaledDeltaTime;

                    //  表示時間が過ぎていたら演出終了
                    if (_dispCount > _dispTime)
                        FinGoalDirecting();
                }
                break;
        }
    }

    //  =======ゴールが入った時の演出を呼ぶ==========

    [PunRPC]
   //   赤ゴール
    public void RequestRedGoalDirecting()
    {
        //  ゲームをスローモーションにする
        Time.timeScale = _slowTime;

        //  移動フェーズに移行
        _phase = Phase.Move;

        //  赤チーム
        _goalTeam = GoalTeam.Red;
        _Teamrect = _teamImages[(int)_goalTeam].gameObject.GetComponent<RectTransform>();

        //  リスタートのフラグを折っておく
        GameObject.FindGameObjectWithTag("playManager").GetComponent<PlayScene>()._isGoalDirecting = false;
    }

    [PunRPC]
    //  青ゴール
    public void RequestBlueGoalDirecting()
    {
        //  ゲームをスローモーションにする
        Time.timeScale = _slowTime;

        //  移動フェーズに移行
        _phase = Phase.Move;

        //  青チーム設定
        _goalTeam = GoalTeam.Blue;
        _Teamrect = _teamImages[(int)_goalTeam].gameObject.GetComponent<RectTransform>();

        //  リスタートのフラグを折っておく
        GameObject.FindGameObjectWithTag("playManager").GetComponent<PlayScene>()._isGoalDirecting = false;
    }

    //  ゴール演出終了
    private void FinGoalDirecting()
    {
        //  時間を正常に戻す
        Time.timeScale = 1.0f;

        //  Uiを初期位置に設定
        Vector3 pos = _Goalrect.localPosition;
        pos = _initGoalPos;
        _Goalrect.localPosition = pos;

        Vector3 team_pos = _Teamrect.localPosition;
        team_pos = _initTeamPos;
        _Teamrect.localPosition = team_pos;

        //  表示時間リセット
        _dispCount = 0.0f;

        //  待機フェーズに移行
        _phase = Phase.Idle;

        //  リスタート関数
        GameObject.FindGameObjectWithTag("playManager").GetComponent<PhotonView>().RPC("RequestRestartGame", RpcTarget.MasterClient);
    }
}
