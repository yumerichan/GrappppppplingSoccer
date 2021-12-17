using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class StartDirecting : MonoBehaviour
{
    //  リスタート演出フラグ
    //  これが立っていたらゲームは進行しない
    public bool _isRestartDirection { get; set; }

    [SerializeField]
    private Image _image;       //画像
    [SerializeField]
    private Text _text;         //カウントダウン用テキスト
    [SerializeField]
    private float _countDownTime;   //カウントダウン秒数
    private int count;
    private float _curCountDown;   //カウントダウン

    [SerializeField]
    private float _imageDispTime;       //スタート画像表示時間
    private float _curImageDispTime;    //現在の画像表示時間

    private enum Phase
    {
        Count,
        Display,
    }

    Phase _phase;

    // Start is called before the first frame update
    void Start()
    {
        //  初期化
        _curCountDown = _countDownTime;
        count = (int)_curCountDown;
        _curImageDispTime = 0.0f;
        _phase = Phase.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isRestartDirection)
        {
            switch (_phase)
            {
                case Phase.Count:
                    {
                        _curCountDown -= Time.deltaTime;
                        count = (int)_curCountDown;

                        _text.text = (1 + count).ToString();

                        //  残り0.1秒になったらスタート画像表示
                        if (_curCountDown < 0.1)
                        {
                            _image.gameObject.SetActive(true);

                            //  カウントダウン非表示
                            _text.gameObject.SetActive(false);

                            //  フェーズ以降
                            _phase = Phase.Display;
                        }
                    }
                    break;

                case Phase.Display:
                    {
                        _curImageDispTime += Time.deltaTime;

                        //  表示時間が過ぎていたら演出終了
                        if (_curImageDispTime > _imageDispTime)
                            FinRestartDirection();
                    }
                    break;
            }
           
        }
    }

    //  スタート関数リクエスト
    [PunRPC]
    public void requestRestartDirection()
    {
        _isRestartDirection = true;
        _text.gameObject.SetActive(true);

    }

    //  スタート演出終了
    private void FinRestartDirection()
    {
        _isRestartDirection = false;
        _image.gameObject.SetActive(false);
        _text.gameObject.SetActive(false);
        _curCountDown = _countDownTime;
        _phase = Phase.Count;
        _curImageDispTime = 0.0f;
    }
}
