using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviourPunCallbacks
{
    public float _iceTime;           //氷結時間
    public GameObject _iceEffect;         //氷結エフェクト

    public float _blackHoleTime;            //吸い込み総時間
    public float _blackHoleMaxPower;        //吸い込み最大の力
    public float _blackHoleMinPower;        //吸い込み最小の力
    public float _blackHoleRange;           //吸い込み範囲


    private float _curIceTime;        //現在の氷結時間
    private bool _isIce;             //氷結しているかフラグ
    private GameObject _cloneIceEffect;    //プレイ中氷結エフェクト


    private Rigidbody _rigidBody;         //リジッドボディ

    private float _curBlackHoleTime;        //現在の吸い込み時間
    private bool _isBlackHole;              //吸い込んでいるか
    private Vector3 _blackHolePos;          //ブラックホール座標

    private GameObject _player;
    private GameObject _hitPlayer;

    private GoalScore _goalScore;

    private Vector3 networkPosition;
    private Quaternion networkRotation;

    private bool _isGoal;
    private float _notGoalTime;

    // Start is called before the first frame update
    void Start()
    {
        _isIce = _isBlackHole = false;
        _curIceTime = _curBlackHoleTime = 0f;
        _rigidBody = transform.GetComponent<Rigidbody>();
        _cloneIceEffect = null;
        _hitPlayer = null;
        _goalScore = GetComponent<GoalScore>();
        _isGoal = false;
        _notGoalTime = 3f;
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
        FixedUpdate();

        if (_isGoal)
        {
            _notGoalTime -= Time.deltaTime;
            if (_notGoalTime <= 0f)
            {
                _isGoal = false;
            }
        }
    }

    //氷結スキル発動
    [PunRPC]
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

    [PunRPC]
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
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this._rigidBody.position);
            stream.SendNext(this._rigidBody.rotation);
            stream.SendNext(this._rigidBody.velocity);
        }
        else
        {
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
            _rigidBody.velocity = (Vector3)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.timestamp));
            networkPosition += (this._rigidBody.velocity * lag);
        }
    }

    public void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            _rigidBody.position = Vector3.MoveTowards(_rigidBody.position, networkPosition, Time.fixedDeltaTime);
            _rigidBody.rotation = Quaternion.RotateTowards(_rigidBody.rotation, networkRotation, Time.fixedDeltaTime * 100.0f);
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
            {
                GetComponent<PhotonView>().RPC("FinIceEffect", RpcTarget.All);
            }
        }
    }

    //  ゴール判定
    private void OnTriggerEnter(Collider other)
    {
        if (!_isGoal)
        {
            if (other.gameObject.tag == "RedGoal")
            {
                //  ブルーの人ならゴール数追加
                //if(_hitPlayer.GetComponent<CharaScore>()._scoreInfo._teamKind == 1)
                //{
                    _hitPlayer.GetComponent<PhotonView>().RPC("AddGoal", RpcTarget.All);
                //}
                _isGoal = true;
                _notGoalTime = 3f;
            }
            else if (other.gameObject.tag == "BlueGoal")
            {
                //  レッドの人ならゴール数追加
                //if (_hitPlayer.GetComponent<CharaScore>()._scoreInfo._teamKind == 0)
                //{
                    _hitPlayer.GetComponent<PhotonView>().RPC("AddGoal", RpcTarget.All);
                //}
                _isGoal = true;
                _notGoalTime = 3f;
            }
        }
    }
}
