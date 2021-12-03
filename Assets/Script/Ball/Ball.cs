using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviour
{
    public float        _iceTime;           //�X������
    public GameObject   _iceEffect;         //�X���G�t�F�N�g

    public float _blackHoleTime;            //�z�����ݑ�����
    public float _blackHoleMaxPower;        //�z�����ݍő�̗�
    public float _blackHoleMinPower;        //�z�����ݍŏ��̗�
    public float _blackHoleRange;           //�z�����ݔ͈�


    private float       _curIceTime;        //���݂̕X������
    private bool        _isIce;             //�X�����Ă��邩�t���O
    private GameObject  _cloneIceEffect;    //�v���C���X���G�t�F�N�g
    

    private Rigidbody   _rigidBody;         //���W�b�h�{�f�B

    private float _curBlackHoleTime;        //���݂̋z�����ݎ���
    private bool _isBlackHole;              //�z������ł��邩
    private Vector3 _blackHolePos;          //�u���b�N�z�[�����W

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

    //�X���X�L������
    public void StartIceEffect(GameObject player)
    {
        //��Ƀu���b�N�z�[�����o�Ă��炵�イ��傤
        if (_isBlackHole)
        {
            FinBlackHole();
        }

        if (_cloneIceEffect != null)
        {
            PhotonNetwork.Destroy(_cloneIceEffect.gameObject);
        }

        //������
        _curIceTime = 0f;
        _isIce = true;

        //�G�t�F�N�g�𐶐����A�{�[���̎q�I�u�W�F�N�g�ɂ���
        _cloneIceEffect = PhotonNetwork.Instantiate("IceStopEffect", transform.position, Quaternion.identity);
        _cloneIceEffect.transform.SetParent(transform);

        //�d�̓I�t
        _rigidBody.useGravity = false;
        _rigidBody.velocity = Vector3.zero;

        _player = player;
        
    }

    private void FinIceEffect()
    {
        //�q���폜
        PhotonNetwork.Destroy(_cloneIceEffect.gameObject);
        _isIce = false;
        _cloneIceEffect = null;

        //�d�̓I��
        _rigidBody.useGravity = true;
        _player.GetComponent<CharactorBasic>().isSkill_ = false;
    }

    //�X���X�L���A�b�v�f�[�g
    private void UpdateIceEffect()
    {
        if (!_isIce)
            return;

        _curIceTime += Time.deltaTime;

        //�������
        if(_curIceTime >= _iceTime)
        {
            FinIceEffect();
        }
    }

    //�u���b�N�z�[���X�L������
    public void StartBlackHole(Vector3 pos)
    {
        //��ɃA�C�X���o�Ă��炵�イ��傤
        if(_isIce)
        {
            FinIceEffect();
        }

        //������
        _curBlackHoleTime = 0f;
        _isBlackHole = true;
        _blackHolePos = pos;

        //�d�̓I�t
        _rigidBody.useGravity = false;
        _rigidBody.velocity = Vector3.zero;
    }

    //�u���b�N�z�[���I��
    private void FinBlackHole()
    {
        _isBlackHole = false;
   
        //�d�̓I��
        _rigidBody.useGravity = true;
    }

    //�u���b�N�z�[���A�b�v�f�[�g
    private void UpdateBlackHole()
    {
        if (!_isBlackHole)
            return;

        //�u���b�N�z�[���̕��������Ă��̂܂܋z�����܂��
        transform.LookAt(_blackHolePos);
        _rigidBody.AddForce(transform.forward* 5);


        _curBlackHoleTime += Time.deltaTime;

        //�������
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
        //  �v���C���[�Ɠ���������
        if(collision.gameObject.tag == "Player")
        {
            //�X�V
            _hitPlayer = collision.gameObject;

            //�{�[���ɓ��������X�R�A���Z
            _hitPlayer.transform.GetComponent<CharaScore>().AddBallAtk();


            if (_isIce == true)
                FinIceEffect();
        }
    }

    //  �S�[������
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
