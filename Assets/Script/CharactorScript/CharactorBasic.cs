using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class CharactorBasic : MonoBehaviourPunCallbacks
{
    //  �L�����N�^�[�̏��
    [HideInInspector]
    public enum CharactorStateType
    {
        STATE_TYPE_NONE = -1,

        STATE_TYPE_IDLE,    //�ҋ@
        STATE_TYPE_RUN,     //����
        STATE_TYPE_JUMP,    //�W�����v
        STATE_TYPE_LANDING, //���n
        STATE_TYPE_FALL,    //����
        STATE_TYPE_SHOT,    //�A���J�[����
        STATE_TYPE_GRAPPLE, //�O���b�v��
        STATE_TYPE_BOOST,   //�u�[�X�g
        STATE_TYPE_SKILL,   //�X�L��

        STATE_TYPE_NUM,
    }
    public enum SkillType
    {
        SKILL_TYPE_NONE = -1,

        SKILL_TYPE_WALL,        //��
        SKILL_TYPE_FREEZE,      //�X
        SKILL_TYPE_TRAP,        //�g���b�v
        SKILL_TYPE_BLACKHOLE,   //�u���b�N�z�[��

        SKILL_TYPE_NUM,
    }

    private float rotDedZone_ = 0.001f; //��]�̃f�b�h�]�[��
    private float WallSkillCoolTime_ = 10.0f;       //�ǐݒu�X�L��CT(�b)
    private float FreezeSkillCoolTime_ = 10.0f;     //�{�[����~�X�L��CT(�b)
    private float TrapSkillCoolTime_ = 10.0f;       //�g���b�v�X�L��CT(�b)
    private float BlackHoleSkillCoolTime_ = 10.0f;  //�u���b�N�z�[���X�L��CT(�b)

    private Vector3 move_;              //�ړ���
    private Vector3 moveDirection_;     //�ړ�����
    private Vector3 latestPos_;         //�O�t�����W
    private Vector2 tempDirection_;     //���͒l�i�[
   
    private float curSkillCoolTime_;    //���݂̃X�L��CT
    private float skillCoolTime_;       //�ݒ肵�Ă���X�L��CT
    private bool isLanding;
    private CinemachineFreeLook freelookCamera_;    //�V�l�}�V�[���t���[���b�N
    private GameObject camera_;                     //�J�����̃Q�[���I�u�W�F�N�g
    [SerializeField]
    private float speed_;               //�ړ��X�s�[�h
    [SerializeField]
    private float jumpPower_;           //�W�����v��
    [SerializeField]
    private float maxMoveSpeed_;        //�ړ��ō����x
    [SerializeField]
    private float maxGrappleSpeed_;     //�O���b�v�����ō����x
    [SerializeField]
    private float maxRestrantTime;      //㩍S������

    public Canvas charaCanvas_;         //�L�����N�^�[��UI�L�����o�X
    [HideInInspector]
    public CharactorStateType state_ { get; set; }   //�L�����̏��
    [HideInInspector]
    public string charaName_ { get; set; }          //�L������
    [HideInInspector]
    public SkillType skillType_ { get; set; }       //�L�������Ƃ̃X�L��
    [HideInInspector]
    public bool isSkill_ { get; set; }               //�X�L�����������ǂ���(�v���C���[�̏�ԂƂ͕�)
    [HideInInspector]
    public Animator anime_ { get; set; }           //�A�j���[�V��������

    private Rigidbody charaRb_;         //�L������RigitBody
    private bool isCaughtTrap;          //㩂Ɉ����������Ă��邩
    private float curRestrantTime;      //���݂̍S������

    //  �X�L�������p�f���Q�[�g
    private delegate void SkillFunc();
    SkillFunc skillFunc_;

    /*   �f�o�b�O�p */
    private CharactorStateType _prestate;

    // Start is called before the first frame update

    public static CharactorBasic Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        //  ��x�S�L������UI���\����
        charaCanvas_.gameObject.SetActive(false);

        //  ���������삷��v���C���[�̂ݐݒ�
        if (photonView.IsMine)
        {
            //  �J�����ݒ�
            //  VirtualCamera �̃Q�[���I�u�W�F�N�g���擾
            camera_ = GameObject.FindGameObjectWithTag("FreeLookCamera");

            //  VirtualCamera �̃R���|�[�l���g���擾
            freelookCamera_ = camera_.GetComponent<CinemachineFreeLook>();

            //  �Ǐ]
            freelookCamera_.Follow = transform;

            //  �����_
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

        //  �L�������Ƃ̏����ݒ�
        if (photonView.IsMine)
        {
            charaCanvas_.gameObject.SetActive(true);
            GameObject network = GameObject.FindGameObjectWithTag("NetWork");
            charaName_ = network.GetComponent<PNNetWork>().charaName_;
            CharaTypeInit();
           
        }
    }

    // Update is called once per frame
    void Update()
    {

        /*  �O�t����Ԃ�ۑ� */
        _prestate = state_;


        //  ����
        CharaInput();

        //  �ړ��X�V
        MoveUpdate();

        //  �i�s�����ɉ�]�X�V
        RotatioinUpdate();

        //  �X�L���X�V
        UpdateSkill();

        if(anime_.GetInteger("AnimState") == (int)CharactorStateType.STATE_TYPE_LANDING)
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

    //  ���͊֘A===============================================
    private void CharaInput()
    {
        //  ���������삷��v���C���[�ȊO�͓��͂ł��Ȃ�
        if (!photonView.IsMine)
            return;

        //  �X�e�B�b�N�̒l���擾
        tempDirection_.x = Input.GetAxis("Horizontal");
        tempDirection_.y = Input.GetAxis("Vertical");

        //  �g���K�[�̓��͒l
        //  �g���K�[�l�����̐� : LeftTrigger
        //  �g���K�[�l�����̐� : RightTrigger
        float trigger_value = Input.GetAxis("Boost_Skill");

        //  �{�^�����͎擾

        //  �u�[�X�g
        bool is_boost = false;

        //  �X�L��
        bool is_skill = false;

        //  �g���K�[�̒l�œ��͂��ꂽ���앪��
        if ((trigger_value > 0 || Input.GetKeyDown(KeyCode.E)) &&
            state_ != CharactorStateType.STATE_TYPE_BOOST)
            is_boost = true;
        else if (trigger_value < 0 || Input.GetKeyDown(KeyCode.Q))
            is_skill = true;

        //  �W�����v    ============================================================================
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            if (IsJump())
            {
                //  �W�����v�͉��Z
                charaRb_.AddForce(transform.up * jumpPower_, ForceMode.Impulse);
                {
                    state_ = CharactorStateType.STATE_TYPE_JUMP;
                    anime_.SetInteger("AnimState", (int)state_);
                }
            }
        }

        //   �O���b�v��
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Grapple"))
        {
            //  �O���b�v���t���O�擾
            bool is_grapple = this.GetComponent<Grappling>().isGrappling_;

            if (is_grapple == false)
            {
                this.GetComponent<Grappling>().ShotAnchor();

                //�O���b�v���X�R�A���Z
                GetComponent<PhotonView>().RPC("AddGrap", RpcTarget.All);
            }
            else if (is_grapple)
            {
                this.GetComponent<Grappling>().isGrappling_ = false;
                state_ = CharactorStateType.STATE_TYPE_FALL;
                anime_.SetInteger("AnimState", (int)state_);
            }
        }

        //   �u�[�X�g
        if (is_boost)
        {
            this.GetComponent<Boost>().OnBoost();
            state_ = CharactorStateType.STATE_TYPE_BOOST;
            anime_.SetInteger("AnimState", (int)state_);
        }

        //  �X�L��
        if (is_skill)
        {
            OnSkill();
            state_ = CharactorStateType.STATE_TYPE_SKILL;
            anime_.SetInteger("AnimState", (int)state_);
        }
    }

        //  �ړ�����
        private void MoveUpdate()
    {
        //  �g���b�v�S�����͈ړ��ł��Ȃ�
        if (isCaughtTrap)
        {
            UpdateCaughtTrap();
            return;
        }

        //  �u�[�X�g���͈ړ��ł��Ȃ�
        if (state_ == CharactorStateType.STATE_TYPE_BOOST || 
            state_ == CharactorStateType.STATE_TYPE_GRAPPLE)
            return;

        //  �J�����̌����Ɠ��͒l����ړ������ݒ�
        Quaternion rot = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        moveDirection_ = rot * new Vector3(tempDirection_.x, 0, tempDirection_.y);

        //  �ړ��ʂ𐳋K�����A���x�v�Z
        moveDirection_.Normalize();
        move_ = moveDirection_ * speed_;

        //  �ړ��ʂ����Ȃ�������ړ����Ă��Ȃ�����
        if((move_.x <= 0.3 && move_.x >= -0.3) &&
            (move_.z <= 0.3 && move_.z >= -0.3))
        {
            charaRb_.velocity = new Vector3(0, charaRb_.velocity.y, 0);

            if (anime_.GetInteger("AnimState") == (int)CharactorStateType.STATE_TYPE_RUN)
            {
                state_ = CharactorStateType.STATE_TYPE_IDLE;
                anime_.SetInteger("AnimState", (int)state_);
            }

            return;
        }

        //  �n�ʂ̏ꍇ�͑���
        if (anime_.GetInteger("AnimState") == (int)CharactorStateType.STATE_TYPE_IDLE)
        {
            state_ = CharactorStateType.STATE_TYPE_RUN;
            anime_.SetInteger("AnimState", (int)state_);
        }

        //  �͂���������
        charaRb_.AddForce(move_, ForceMode.Impulse);

        //  ������������ꍇ
        if (IsSpeedLimmit())
        {
            float max_speed;

            //  �O���b�v�����͈ړ����x������
            if (state_ == CharactorStateType.STATE_TYPE_GRAPPLE)
            {
                max_speed = maxGrappleSpeed_;

                //  �ō����x�ɒB���Ă�����
                if (charaRb_.velocity.magnitude > max_speed)
                    charaRb_.velocity = charaRb_.velocity.normalized * max_speed;
            }
            //  ����ȊO�͂������
            else
            {
                max_speed = maxMoveSpeed_;

                //  �ō����x�ɒB���Ă�����
                if (charaRb_.velocity.magnitude > max_speed)
                {
                    Vector3 temp = charaRb_.velocity.normalized * max_speed;
                    charaRb_.velocity = new Vector3(temp.x, charaRb_.velocity.y, temp.z);
                }
            }
        }
    }

    //  ��]����
    private void RotatioinUpdate()
    {
        //  �O�t���Ƃ̈ʒu�̍�����i�s����������o��
        Vector3 differenceDis = new Vector3(transform.position.x, 0, transform.position.z) -
            new Vector3(latestPos_.x, 0, latestPos_.z);

        //  �O�t���X�V
        latestPos_ = transform.position;

        //  �ړ����Ă�����
        if(Mathf.Abs(differenceDis.x) > rotDedZone_ || Mathf.Abs(differenceDis.z) > rotDedZone_)
        {
            ////  �ړ������������Ȃ��]���Ȃ�
            //if (moveDirection_ == new Vector3(0, 0, 0))
            //    return;

            //  �i�s�����Ɍ������ĉ�]
            Quaternion rot = Quaternion.LookRotation(differenceDis);
            rot = Quaternion.Slerp(charaRb_.transform.rotation, rot, 0.2f);
            this.transform.rotation = rot;
        }
    }

    //  �����蔻��
    private void OnCollisionEnter(Collision other)
    {
        //  �u�[�X�g���ɉ����ɓ����������痎��
        if (state_ == CharactorStateType.STATE_TYPE_BOOST)
        {
            charaRb_.velocity = new Vector3(0, 0, 0);
            state_ = CharactorStateType.STATE_TYPE_FALL;
            anime_.SetInteger("AnimState", (int)state_);
        }

        //  �n�ʂɓ���������ҋ@��Ԃ�
        if (other.gameObject.tag.Equals("Ground") && 
            anime_.GetInteger("AnimState") == (int)CharactorStateType.STATE_TYPE_FALL)
        {
            state_ = CharactorStateType.STATE_TYPE_LANDING;
            anime_.SetInteger("AnimState", (int)state_);
            isLanding = true;
        }
    }

    //  �L�������Ƃ̏����ݒ�֐�
    private void CharaTypeInit()
    {
        //  �X�L���^�C�v�A�X�L��CT�A�X�L�������֐��ݒ�

        //  ��
        if (charaName_ == "Chara_Wall")
        {
            skillType_ = SkillType.SKILL_TYPE_WALL;
            skillCoolTime_ = WallSkillCoolTime_;
            skillFunc_ = this.GetComponent<CharaSkill>().OnSkillWall;
        }
        //  �{�[����~
        else if (charaName_ == "Chara_Freeze")
        {
            skillType_ = SkillType.SKILL_TYPE_FREEZE;
            skillCoolTime_ = FreezeSkillCoolTime_;
            skillFunc_ = this.GetComponent<CharaSkill>().OnSkillFriaze;
        }
        //  �g���b�v
        else if (charaName_ == "Chara_Trap")
        {
            skillType_ = SkillType.SKILL_TYPE_TRAP;
            skillCoolTime_ = TrapSkillCoolTime_;
            skillFunc_ = this.GetComponent<CharaSkill>().OnSkillTrap;
        }
        //  �u���b�N�z�[��
        else if (charaName_ == "Chara_BlackHole")
        {
            skillType_ = SkillType.SKILL_TYPE_BLACKHOLE;
            skillCoolTime_ = BlackHoleSkillCoolTime_;
            skillFunc_ = this.GetComponent<CharaSkill>().OnSkillBlackHole;
        }

    }

    //  ���x��������
    private bool IsSpeedLimmit()
    {
        //  �ҋ@�A����A�W�����v�A�O���b�v�����͑��x����
        switch(state_)
        {
            case CharactorStateType.STATE_TYPE_IDLE:
            case CharactorStateType.STATE_TYPE_RUN:
            case CharactorStateType.STATE_TYPE_JUMP:
            case CharactorStateType.STATE_TYPE_FALL:
            case CharactorStateType.STATE_TYPE_GRAPPLE:
            case CharactorStateType.STATE_TYPE_SHOT:
                return true;
        }

        return false;
    }

    //  �W�����v�\����
    private bool IsJump()
    {
        if (photonView.IsMine)
        {
            //  �ҋ@��Ԃ܂��͑����ԂŃW�����v�\
            switch (state_)
            {
                case CharactorStateType.STATE_TYPE_IDLE:
                case CharactorStateType.STATE_TYPE_RUN:
                    return true;
            }
        }

        return false;
    }

    //  ��������
    private bool IsFall()
    {
        //  �O���b�v�����ˁA�u�[�X�g���͗������[�V�����ɓ���Ȃ�
        switch (state_)
        {
            case CharactorStateType.STATE_TYPE_GRAPPLE:
            case CharactorStateType.STATE_TYPE_SHOT:
            case CharactorStateType.STATE_TYPE_BOOST:
                return false;
        }

        return true;
    }

    //  �g���b�v�Ɉ��������������ɍŏ��ɓ��鏈��
    public void GetCaughtInTrap()
    {
        isCaughtTrap = true;
        curRestrantTime = 0f;
        charaRb_.useGravity = false;
        charaRb_.velocity = Vector3.zero;
    }

    //  �g���b�v�S�����X�V����
    private void UpdateCaughtTrap()
    {
        curRestrantTime += Time.deltaTime;
        charaRb_.velocity = Vector3.zero;

        //  �I��
        if (curRestrantTime >= maxRestrantTime)
        {
            isCaughtTrap = false;
            curRestrantTime = 0f;
            charaRb_.useGravity = true;

            state_ = CharactorStateType.STATE_TYPE_IDLE;
            anime_.SetInteger("AnimState", (int)state_);
        }
    }

   

    //  �X�L��
    private void OnSkill()
    {
        //  �X�L���������łȂ����ACT�����܂��Ă����甭���\
        if (isSkill_)
            return;

        //  CT����
        if (curSkillCoolTime_ >= skillCoolTime_)
        {
            //  �X�L������
            skillFunc_();

            //  �X�L���t���O���Ă�
            isSkill_ = true;

            //  �X�L��CT���Z�b�g
            curSkillCoolTime_ = 0.0f;

            //�X�L�������X�R�A���Z
            this.GetComponent<CharaScore>().AddSkillCnt();
        }    
    }

    private void UpdateSkill()
    {
        //  �X�L���������ȊO��CT�̍X�V
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
}
