using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class CharactorBasic : MonoBehaviourPunCallbacks
{
    //  キャラクターの状態
    [HideInInspector]
    public enum CharactorStateType
    {
        STATE_TYPE_NONE = -1,

        STATE_TYPE_IDLE,    //待機
        STATE_TYPE_RUN,     //走り
        STATE_TYPE_JUMP,    //ジャンプ
        STATE_TYPE_FALL,    //落下
        STATE_TYPE_LANDING, //着地
        STATE_TYPE_SHOT,    //アンカー発射
        STATE_TYPE_GRAPPLE, //グラップル
        STATE_TYPE_BOOST,   //ブースト
        STATE_TYPE_SKILL,   //スキル

        STATE_TYPE_NUM,
    }
    public enum SkillType
    {
        SKILL_TYPE_NONE = -1,

        SKILL_TYPE_WALL,        //壁
        SKILL_TYPE_FREEZE,      //氷
        SKILL_TYPE_TRAP,        //トラップ
        SKILL_TYPE_BLACKHOLE,   //ブラックホール

        SKILL_TYPE_NUM,
    }

    private float rotDedZone_ = 0.001f; //回転のデッドゾーン
    private float WallSkillCoolTime_ = 10.0f;       //壁設置スキルCT(秒)
    private float FreezeSkillCoolTime_ = 10.0f;     //ボール停止スキルCT(秒)
    private float TrapSkillCoolTime_ = 10.0f;       //トラップスキルCT(秒)
    private float BlackHoleSkillCoolTime_ = 10.0f;  //ブラックホールスキルCT(秒)

    private Vector3 move_;              //移動量
    private Vector3 moveDirection_;     //移動方向
    private Vector3 latestPos_;         //前フレ座標
    private Vector2 tempDirection_;     //入力値格納
   
    private float curSkillCoolTime_;    //現在のスキルCT
    private float skillCoolTime_;       //設定してあるスキルCT
    private bool isLanding;
    private CinemachineFreeLook freelookCamera_;    //シネマシーンフリールック
    private GameObject camera_;                     //カメラのゲームオブジェクト
    [SerializeField]
    private float speed_;               //移動スピード
    [SerializeField]
    private float jumpPower_;           //ジャンプ力
    [SerializeField]
    private float maxMoveSpeed_;        //移動最高速度
    [SerializeField]
    private float maxGrappleSpeed_;     //グラップル中最高速度
    [SerializeField]
    private float maxRestrantTime;      //罠拘束時間
    [SerializeField]
    private float _strengthTime;        //強化時間
    [SerializeField]
    private float _strengthAmount;      //強化倍率

    public Canvas charaCanvas_;         //キャラクターのUIキャンバス
    [HideInInspector]
    public CharactorStateType state_ { get; set; }   //キャラの状態
    [HideInInspector]
    public string charaName_ { get; set; }          //キャラ名
    [HideInInspector]
    public SkillType skillType_ { get; set; }       //キャラごとのスキル
    [HideInInspector]
    public bool isSkill_ { get; set; }               //スキル発動中かどうか(プレイヤーの状態とは別)
    [HideInInspector]
    public Animator anime_ { get; set; }           //アニメーション操作

    private Rigidbody charaRb_;         //キャラのRigitBody
    private bool isCaughtTrap;          //罠に引っかかっているか
    private float curRestrantTime;      //現在の拘束時間
    private bool _preIsBallBoost;           //前フレのブースト入力
    private bool _preIsCamBoost;           //前フレのブースト入力
    private bool _isStrength;               //強化フラグ
    private float _curStrengthTime;         //強化カウント

    //  チームカラー
    [HideInInspector]
    public int _teamColor { get; set; } //0：赤　１：青

    //  スキル発動用デリゲート
    private delegate void SkillFunc();
    SkillFunc skillFunc_;

    /* サウンド関連 */
    AudioSource _audioSource;
    public AudioClip _grappleSE;  //グラップルSE
    public AudioClip _boostSE;  //ブーストSE
    public AudioClip _skillSE;  //スキルSE
    public AudioClip _skillChargeSE;  //スキルがたまったSE
    public AudioClip _boostHitSE;  //ブースト時に何かに当たるSE
    public AudioClip _hitSE;  //何かに当たるSE

    /*   デバッグ用 */
    private CharactorStateType _prestate;

    // Start is called before the first frame update

    public static CharactorBasic Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        //  一度全キャラのUIを非表示に
        charaCanvas_.gameObject.SetActive(false);

        //  自分が操作するプレイヤーのみ設定
        if (photonView.IsMine)
        {
            //  カメラ設定
            //  VirtualCamera のゲームオブジェクトを取得
            camera_ = GameObject.FindGameObjectWithTag("FreeLookCamera");

            //  VirtualCamera のコンポーネントを取得
            freelookCamera_ = camera_.GetComponent<CinemachineFreeLook>();

            //  追従
            freelookCamera_.Follow = transform;

            //  注視点
            freelookCamera_.LookAt = transform.Find("LookPos");
        }
    }
    public void Delete()
    {
        Destroy(Instance);
    }

    void Start()
    {
        move_ = new Vector3(0, 0, 0);
        moveDirection_ = new Vector3(0, 0, 0);
        latestPos_ = new Vector3(0, 0, 0);
        tempDirection_ = new Vector2(0, 0);
        charaRb_ = GetComponent<Rigidbody>();
        state_ = CharactorStateType.STATE_TYPE_IDLE;
        anime_ = GetComponent<Animator>();
        anime_.SetInteger("AnimState", (int)state_);
        isSkill_ = false;
        curSkillCoolTime_ = 10.0f;
        isCaughtTrap = false;
        curRestrantTime = 0f;
        _preIsBallBoost = false;
        _preIsCamBoost = false;
        _isStrength = false;
        _curStrengthTime = 0.0f;

        //  キャラごとの初期設定
        if (photonView.IsMine)
        {
            charaCanvas_.gameObject.SetActive(true);
            GameObject network = GameObject.FindGameObjectWithTag("NetWork");
            charaName_ = network.GetComponent<PNNetWork>().charaName_;
            CharaTypeInit();
            _teamColor = -1;
            _audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        /*  前フレ状態を保存 */
        _prestate = state_;

        //  入力
        CharaInput();

        //  移動更新
        MoveUpdate();

        //  進行方向に回転更新
        RotatioinUpdate();

        //  スキル更新
        UpdateSkill();

        //  オーナーチェック
        CheckBallOwner();


        //  強化スキル更新
        if (_isStrength)
            UpdateStrength();

        if (anime_.GetInteger("AnimState") == (int)CharactorStateType.STATE_TYPE_LANDING)
        {
            if (isLanding) { isLanding = false; return; };

             AnimatorStateInfo animeStateInfo = anime_.GetCurrentAnimatorStateInfo(0);

            Debug.Log(animeStateInfo.normalizedTime);

            if (animeStateInfo.normalizedTime >= 0.9f)
            {
                state_ = CharactorStateType.STATE_TYPE_IDLE;
                anime_.SetInteger("AnimState", (int)state_);
            }
        }

        if (anime_.GetInteger("AnimState") == (int)CharactorStateType.STATE_TYPE_SKILL)
        {
           
            AnimatorStateInfo animeStateInfo = anime_.GetCurrentAnimatorStateInfo(0);

            Debug.Log(animeStateInfo.normalizedTime);

            if (animeStateInfo.normalizedTime >= 0.9f)
            {
                state_ = CharactorStateType.STATE_TYPE_IDLE;
                anime_.SetInteger("AnimState", (int)state_);
            }
        }

        if (anime_.GetInteger("AnimState") == (int)CharactorStateType.STATE_TYPE_JUMP && IsFall())
        {
            AnimatorStateInfo animeStateInfo = anime_.GetCurrentAnimatorStateInfo(0);
            if (animeStateInfo.normalizedTime >= 0.9f)
            {
                state_ = CharactorStateType.STATE_TYPE_FALL;
                anime_.SetInteger("AnimState", (int)state_);
            }
        }



        if(_prestate != state_)
        {
            Debug.Log(state_ );
            Debug.Log(anime_.GetInteger("AnimState"));
        }

    }

    //ボールオーナー判定
    private void CheckBallOwner()
    {
        Vector3 ball_pos = GameObject.FindGameObjectWithTag("Ball").transform.position;

        //  まず自分とボールの距離を出す
        float mine_distance = Vector3.Distance(this.transform.position, ball_pos);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < players.Length; i++)
        {
            if(players[i] == this.gameObject) { continue; }

            float distance = Vector3.Distance(players[i].transform.position, ball_pos);

            if(mine_distance > distance)
            {
                return;
            }
        }

        GameObject.FindGameObjectWithTag("Ball").
                     GetComponent<BallOwnerView>().RequestOwner();
    }

    //  入力関連===============================================
    private void CharaInput()
    {
        //  自分が操作するプレイヤー以外は入力できない
        if (!photonView.IsMine)
            return;

        //  スティックの値を取得
        tempDirection_.x = Input.GetAxis("Horizontal");
        tempDirection_.y = Input.GetAxis("Vertical");

        //  トリガーの入力値
        //  トリガー値が正の数 : LeftTrigger
        //  トリガー値が負の数 : RightTrigger
        float trigger_value = Input.GetAxis("Boost");

        //  ボタン入力取得

        //  ブースト
        bool is_ball_boost = false;
        bool is_cam_boost = false;

        Boost boost = this.GetComponent<Boost>();

        //  トリガーの値で入力された動作分け
        if ((trigger_value > 0 || Input.GetKeyDown(KeyCode.E)))
            is_ball_boost = true;
        else if (trigger_value < 0 || Input.GetKeyDown(KeyCode.R))
            is_cam_boost = true;

        //  ジャンプ    ============================================================================
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            if (IsJump())
            {
                //  ジャンプ力加算
                charaRb_.AddForce(transform.up * jumpPower_, ForceMode.Impulse);
                {
                    state_ = CharactorStateType.STATE_TYPE_JUMP;
                    anime_.SetInteger("AnimState", (int)state_);
                }
            }
        }

        //   グラップル
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Grapple"))
        {
            //  グラップルフラグ取得
            bool is_grapple = this.GetComponent<Grappling>().isGrappling_;

            if (is_grapple == false)
            {
                this.GetComponent<Grappling>().ShotAnchor();

                _audioSource.PlayOneShot(_grappleSE);

                //グラップルスコア加算
                GetComponent<PhotonView>().RPC("AddGrap", RpcTarget.All);
            }
            else if (is_grapple)
            {
                this.GetComponent<Grappling>().isGrappling_ = false;
                state_ = CharactorStateType.STATE_TYPE_FALL;
                anime_.SetInteger("AnimState", (int)state_);
            }
        }

        //   ブースト
        if (is_ball_boost && _preIsBallBoost == false)
        {
            //  すでにブースト状態だったらブーストキャンセル
            if (state_ == CharactorStateType.STATE_TYPE_BOOST &&
                boost._boostType == Boost.BoostType.BallBoost)
            {
                boost.CancelBoost();
                state_ = CharactorStateType.STATE_TYPE_FALL;
                anime_.SetInteger("AnimState", (int)state_);
            }
            else
            {
                boost.OnBallBoost();
                state_ = CharactorStateType.STATE_TYPE_BOOST;
                anime_.SetInteger("AnimState", (int)state_);

                _audioSource.PlayOneShot(_boostSE);
            }  
        }
        else if(is_cam_boost && _preIsCamBoost == false)
        {
            //  すでにブースト状態だったらブーストキャンセル
            if (state_ == CharactorStateType.STATE_TYPE_BOOST &&
                boost._boostType == Boost.BoostType.CamBoost)
            {
                boost.CancelBoost();
                state_ = CharactorStateType.STATE_TYPE_FALL;
                anime_.SetInteger("AnimState", (int)state_);
            }
            else
            {
                boost.OnCamBoost();
                state_ = CharactorStateType.STATE_TYPE_BOOST;
                anime_.SetInteger("AnimState", (int)state_);

                _audioSource.PlayOneShot(_boostSE);

            }
        }

        //  ブースト入力保存
        _preIsBallBoost = is_ball_boost;
        _preIsCamBoost = is_cam_boost;

        //  スキル
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Skill") && isSkill_ == false)
        {
            OnSkill();
            state_ = CharactorStateType.STATE_TYPE_SKILL;
            anime_.SetInteger("AnimState", (int)state_);

            _audioSource.PlayOneShot(_skillSE);

        }
    }

    //  移動処理
    private void MoveUpdate()
    {
        //  トラップ拘束中は移動できない
        if (isCaughtTrap)
        {
            UpdateCaughtTrap();
            return;
        }

        //  ブースト中は移動できない
        if (state_ == CharactorStateType.STATE_TYPE_BOOST || 
            state_ == CharactorStateType.STATE_TYPE_GRAPPLE)
            return;

        //  カメラの向きと入力値から移動方向設定
        Quaternion rot = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        moveDirection_ = rot * new Vector3(tempDirection_.x, 0, tempDirection_.y);

        //  移動量を正規化し、速度計算
        moveDirection_.Normalize();
        move_ = moveDirection_ * speed_;

        //  移動量が少なかったら移動していない判定
        if((move_.x <= 0.3 && move_.x >= -0.3) &&
            (move_.z <= 0.3 && move_.z >= -0.3))
        {
            //charaRb_.velocity = new Vector3(0, charaRb_.velocity.y, 0);

            if (anime_.GetInteger("AnimState") == (int)CharactorStateType.STATE_TYPE_RUN)
            {
                state_ = CharactorStateType.STATE_TYPE_IDLE;
                anime_.SetInteger("AnimState", (int)state_);
            }

            return;
        }

        //  地面の場合は走り
        if (anime_.GetInteger("AnimState") == (int)CharactorStateType.STATE_TYPE_IDLE)
        {
            state_ = CharactorStateType.STATE_TYPE_RUN;
            anime_.SetInteger("AnimState", (int)state_);
        }

        if (_isStrength)
            move_ *= _strengthAmount;

        //  力をを加える
        charaRb_.AddForce(move_, ForceMode.Impulse);

        //  制限をかける場合
        if (IsSpeedLimmit())
        {
            float max_speed;

            //  グラップル中は移動速度が速い
            if (state_ == CharactorStateType.STATE_TYPE_GRAPPLE)
            {
                max_speed = maxGrappleSpeed_;

                if (_isStrength)
                    max_speed *= _strengthAmount;

                //  最高速度に達していたら
                if (charaRb_.velocity.magnitude > max_speed)
                    charaRb_.velocity = charaRb_.velocity.normalized * max_speed;
            }
            //  それ以外はゆっくり
            else
            {
                max_speed = maxMoveSpeed_;

                if (_isStrength)
                    max_speed *= _strengthAmount;

                //  最高速度に達していたら
                if (charaRb_.velocity.magnitude > max_speed)
                {
                    Vector3 temp = charaRb_.velocity.normalized * max_speed;
                    charaRb_.velocity = new Vector3(temp.x, charaRb_.velocity.y, temp.z);
                }
            }
        }
    }

    //  回転処理
    private void RotatioinUpdate()
    {
        //  前フレとの位置の差から進行方向を割り出す
        Vector3 differenceDis = new Vector3(transform.position.x, 0, transform.position.z) -
            new Vector3(latestPos_.x, 0, latestPos_.z);

        //  前フレ更新
        latestPos_ = transform.position;

        //  移動していたら
        if(Mathf.Abs(differenceDis.x) > rotDedZone_ || Mathf.Abs(differenceDis.z) > rotDedZone_)
        {
            ////  移動方向が同じなら回転しない
            //if (moveDirection_ == new Vector3(0, 0, 0))
            //    return;

            //  進行方向に向かって回転
            Quaternion rot = Quaternion.LookRotation(differenceDis);
            rot = Quaternion.Slerp(charaRb_.transform.rotation, rot, 0.2f);
            this.transform.rotation = rot;
        }
    }

    //  当たり判定
    private void OnCollisionEnter(Collision other)
    {
        //  ブースト中に何かに当たった時ら落下
        if (state_ == CharactorStateType.STATE_TYPE_BOOST)
        {
            charaRb_.velocity = new Vector3(0, 0, 0);
            state_ = CharactorStateType.STATE_TYPE_FALL;
            anime_.SetInteger("AnimState", (int)state_);

            _audioSource.PlayOneShot(_boostHitSE);

        }
        else
            _audioSource.PlayOneShot(_hitSE);

        //  地面に当たったら待機状態へ
        if (other.gameObject.tag.Equals("Ground") &&
            IsGraund() == false)
        {
            state_ = CharactorStateType.STATE_TYPE_LANDING;
            anime_.SetInteger("AnimState", (int)state_);
            isLanding = true;
        }

        
    }

    //  キャラごとの初期設定関数
    private void CharaTypeInit()
    {
        //  スキルタイプ、スキルCT、スキル発動関数設定

        //  壁
        if (charaName_ == "Chara_Wall")
        {
            skillType_ = SkillType.SKILL_TYPE_WALL;
            skillCoolTime_ = WallSkillCoolTime_;
            skillFunc_ = this.GetComponent<CharaSkill>().OnSkillWall;
        }
        //  ボール停止
        else if (charaName_ == "Chara_Freeze")
        {
            skillType_ = SkillType.SKILL_TYPE_FREEZE;
            skillCoolTime_ = FreezeSkillCoolTime_;
            skillFunc_ = this.GetComponent<CharaSkill>().OnSkillFriaze;
        }
        //  トラップ
        else if (charaName_ == "Chara_Trap")
        {
            skillType_ = SkillType.SKILL_TYPE_TRAP;
            skillCoolTime_ = TrapSkillCoolTime_;
            skillFunc_ = this.GetComponent<CharaSkill>().OnSkillTrap;
        }
        //  ブラックホール
        else if (charaName_ == "Character_Blackhole")
        {
            skillType_ = SkillType.SKILL_TYPE_BLACKHOLE;
            skillCoolTime_ = BlackHoleSkillCoolTime_;
            skillFunc_ = OnSkillStrength;
        }

    }

    //  速度制限判定
    private bool IsSpeedLimmit()
    {
        //  待機、走り、ジャンプ、グラップル中は速度制限
        switch(state_)
        {
            case CharactorStateType.STATE_TYPE_IDLE:
            case CharactorStateType.STATE_TYPE_RUN:
            case CharactorStateType.STATE_TYPE_JUMP:
            case CharactorStateType.STATE_TYPE_FALL:
            case CharactorStateType.STATE_TYPE_GRAPPLE:
            case CharactorStateType.STATE_TYPE_SHOT:
            case CharactorStateType.STATE_TYPE_SKILL:

                return true;
        }

        return false;
    }

    //  ジャンプ可能判定
    private bool IsJump()
    {
        if (photonView.IsMine)
        {
            //  待機状態または走り状態でジャンプ可能
            switch (state_)
            {
                case CharactorStateType.STATE_TYPE_IDLE:
                case CharactorStateType.STATE_TYPE_RUN:
                    return true;
            }
        }

        return false;
    }

    //  落下判定
    private bool IsFall()
    {
        //  グラップル発射、ブースト時は落下モーションに入らない
        switch (state_)
        {
            case CharactorStateType.STATE_TYPE_GRAPPLE:
            case CharactorStateType.STATE_TYPE_SHOT:
            case CharactorStateType.STATE_TYPE_BOOST:
                return false;
        }

        return true;
    }

    //  地面判定
    private bool IsGraund()
    {
        switch (state_)
        {
            case CharactorStateType.STATE_TYPE_RUN:
            case CharactorStateType.STATE_TYPE_IDLE:
                return true;
        }

        return false;
    }

    //  トラップに引っかかった時に最初に入る処理
    public void GetCaughtInTrap()
    {
        isCaughtTrap = true;
        curRestrantTime = 0f;
        charaRb_.useGravity = false;
        charaRb_.velocity = Vector3.zero;
    }

    //  トラップ拘束中更新処理
    private void UpdateCaughtTrap()
    {
        curRestrantTime += Time.deltaTime;
        charaRb_.velocity = Vector3.zero;

        //  終了
        if (curRestrantTime >= maxRestrantTime)
        {
            isCaughtTrap = false;
            curRestrantTime = 0f;
            charaRb_.useGravity = true;

            state_ = CharactorStateType.STATE_TYPE_IDLE;
            anime_.SetInteger("AnimState", (int)state_);
        }
    }

   

    //  スキル
    private void OnSkill()
    {
        //  スキル発動中でないかつ、CTがたまっていたら発動可能
        if (isSkill_)
            return;

        //  CT判定
        if (curSkillCoolTime_ >= skillCoolTime_)
        {
            //  スキル発動
            skillFunc_();

            //  スキルフラグ立てる
            isSkill_ = true;

            //  スキルCTリセット
            curSkillCoolTime_ = 0.0f;

            //スキル発動スコア加算
            GetComponent<PhotonView>().RPC("AddSkillCnt", RpcTarget.All);
        }    
    }

    private void UpdateSkill()
    {
        //  スキル発動中以外でCTの更新
        if (isSkill_)
            return;
        
        if (curSkillCoolTime_ < skillCoolTime_)
            curSkillCoolTime_ += Time.deltaTime;

    }

    public float GetCurSkillCoolTime()
    {
        return curSkillCoolTime_;
    }

    public float GetSkillCoolTime()
    {
        return skillCoolTime_;
    }

    private void OnSkillStrength()
    {
        //  強化スキルエフェクト発生

        //  
        _isStrength = true;
    }

    //  強化スキル更新
    private void UpdateStrength()
    {
        _curStrengthTime += Time.deltaTime;

        if (_curStrengthTime > _strengthTime)
        {
            _isStrength = false;
            isSkill_ = false;
            _curStrengthTime = 0.0f;

            //  強化エフェクト削除
        }
    }

    [PunRPC]
    public void InitPos(Vector3 init_pos)
    {
        Vector3 pos = transform.position;
        pos = init_pos;
        transform.position = pos;
        charaRb_.velocity = new Vector3(0, 0, 0);
        state_ = CharactorStateType.STATE_TYPE_IDLE;
        anime_.SetInteger("AnimState", (int)state_);
    }
}
