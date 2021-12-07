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
    private Vector3 _initPos;   //移動前の初期位置

    private RectTransform _rect;   //Ui移動用座標

    private float _dispCount;  //表示時間カウント

    private enum Phase
    { 
        Idle,       //待機
        Move,       //移動
        Display,    //表示
    }
    private Phase _phase;

    // Start is called before the first frame update
    void Start()
    {
        _rect = GetComponent<RectTransform>();
        _phase = Phase.Idle;
        _dispCount = 0.0f;
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
                    //  Uiの座標を参照
                    Vector3 pos = _rect.localPosition;
                    pos.x -= _moveSpeed * Time.unscaledDeltaTime * 15;

                    //  移動終わり
                    if(pos.x < 0)
                    {
                        //  表示フェーズへ
                        pos.x = 0.0f;
                        _phase = Phase.Display;
                    }

                    //  座標を反映
                    _rect.localPosition = pos;
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

    [PunRPC]
    //  ゴールが入った時の演出を呼ぶ
    public void RequestGoalDirecting()
    {
        //  ゲームをスローモーションにする
        Time.timeScale = _slowTime;

        //  移動フェーズに移行
        _phase = Phase.Move;
    }

    //  ゴール演出終了
    private void FinGoalDirecting()
    {
        //  時間を正常に戻す
        Time.timeScale = 1.0f;

        //  Uiを初期位置に設定
        Vector3 pos = _rect.localPosition;
        pos = _initPos;
        _rect.localPosition = pos;

        //  表示時間リセット
        _dispCount = 0.0f;

        //  待機フェーズに移行
        _phase = Phase.Idle;

        //  リスタート関数
        GameObject.FindGameObjectWithTag("playManager").GetComponent<PlayScene>().RequestRestartGame();
    }
}
