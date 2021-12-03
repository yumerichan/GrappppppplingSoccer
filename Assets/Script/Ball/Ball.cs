using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviour
{
    public float        _iceTime;           //氷結時間
    public GameObject   _iceEffect;         //氷結エフェクト

    public float _blackHoleTime;            //吸い込み総時間
    public float _blackHoleMaxPower;        //吸い込み最大の力
    public float _blackHoleMinPower;        //吸い込み最小の力
    public float _blackHoleRange;           //吸い込み範囲


    private float       _curIceTime;        //現在の氷結時間
    private bool        _isIce;             //氷結しているかフラグ
    private GameObject  _cloneIceEffect;    //プレイ中氷結エフェクト
    

    private Rigidbody   _rigidBody;         //リジッドボディ

    private float _curBlackHoleTime;        //現在の吸い込み時間
    private bool _isBlackHole;              //吸い込んでいるか
    private Vector3 _blackHolePos;          //ブラックホール座標

    private GameObject _player;
    private GameObject _hitPlayer;

    private GoalScore _goalScore;

    // Start is called before the first frame update
    void Start()
    {
        _isIce = _isBlackHole = false;
        _curIceTime = _curBlackHoleTime = 0f;
        _rigidBody = transform.GetComponent<Rigidbody>();
        _cloneIceEffect = null;
        _hitPlayer = null;
        _goalScore = GetComponent<GoalScore>();
    }

    private void Awake()
    {
        _isIce = _isBlackHole = false;
        _curIceTime = _curBlackHoleTime = 0f;
        _rigidBody = transform.GetComponent<Rigidbody>();
        _cloneIceEffect = null;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateIceEffect();
        UpdateBlackHole();
    }

    //氷結スキル発動
    public void StartIceEffect(GameObject player)
    {
        //先にブラックホールが出てたらしゅうりょう
        if (_isBlackHole)
        {
            FinBlackHole();
        }

        if (_cloneIceEffect != null)
        {
            PhotonNetwork.Destroy(_cloneIceEffect.gameObject);
        }

        //初期化
        _curIceTime = 0f;
        _isIce = true;

        //エフェクトを生成し、ボールの子オブジェクトにする
        _cloneIceEffect = PhotonNetwork.Instantiate("IceStopEffect", transform.position, Quaternion.identity);
        _cloneIceEffect.transform.SetParent(transform);

        //重力オフ
        _rigidBody.useGravity = false;
        _rigidBody.velocity = Vector3.zero;

        _player = player;
        
    }

    private void FinIceEffect()
    {
        //子を削除
        PhotonNetwork.Destroy(_cloneIceEffect.gameObject);
        _isIce = false;
        _cloneIceEffect = null;

        //重力オン
        _rigidBody.useGravity = true;
        _player.GetComponent<CharactorBasic>().isSkill_ = false;
    }

    //氷結スキルアップデート
    private void UpdateIceEffect()
    {
        if (!_isIce)
            return;

        _curIceTime += Time.deltaTime;

        //上限処理
        if(_curIceTime >= _iceTime)
        {
            FinIceEffect();
        }
    }

    //ブラックホールスキル発動
    public void StartBlackHole(Vector3 pos)
    {
        //先にアイスが出てたらしゅうりょう
        if(_isIce)
        {
            FinIceEffect();
        }

        //初期化
        _curBlackHoleTime = 0f;
        _isBlackHole = true;
        _blackHolePos = pos;

        //重力オフ
        _rigidBody.useGravity = false;
        _rigidBody.velocity = Vector3.zero;
    }

    //ブラックホール終了
    private void FinBlackHole()
    {
        _isBlackHole = false;
   
        //重力オン
        _rigidBody.useGravity = true;
    }

    //ブラックホールアップデート
    private void UpdateBlackHole()
    {
        if (!_isBlackHole)
            return;

        //ブラックホールの方を向いてそのまま吸い込まれる
        transform.LookAt(_blackHolePos);
        _rigidBody.AddForce(transform.forward* 5);


        _curBlackHoleTime += Time.deltaTime;

        //上限処理
        if (_curBlackHoleTime >= _blackHoleTime)
        {
            FinBlackHole();
        }
    }

    public bool GetIsIce()
    {
        return _isIce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //  プレイヤーと当たったら
        if(collision.gameObject.tag == "Player")
        {
            //更新
            _hitPlayer = collision.gameObject;

            //ボールに当たったスコア加算
            _hitPlayer.transform.GetComponent<CharaScore>().AddBallAtk();


            if (_isIce == true)
                FinIceEffect();
        }
    }

    //  ゴール判定
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RedGoal")
        {
            _hitPlayer.transform.GetComponent<CharaScore>().AddGoal();
        }
        else if(other.tag == "BuleGoal")
        {
            _hitPlayer.transform.GetComponent<CharaScore>().AddGoal();
        }
    }
}
