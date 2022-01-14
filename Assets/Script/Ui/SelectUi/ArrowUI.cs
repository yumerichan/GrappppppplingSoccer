using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

public class ArrowUI : MonoBehaviour
{
    public int _curNumber;

    public GameObject _image1v1;
    public GameObject _image2v2;
    public GameObject _image3v3;
    public GameObject _image4v4;
    public GameObject _imageBackToTitle;
    public int _imageNum;

    public GameObject _fade;
    public GameObject _myFade;

    public float _countParam1 = 0.5f;

    private GameObject[] _imageInfo;
    private RectTransform _rectTransform;
    private bool _isDecide;
    private bool _isCanDecide;

    static public int selectNumber_;    //�l��

    public bool _isDebug = false;

    private float _coolTime;

    /* �T�E���h�֘A */
    private AudioSource _audioSource;   //�I�[�f�B�I�\�[�X
    public AudioClip _cursor;           //�J�[�\��
    public AudioClip _diside;           //����

    // Start is called before the first frame update
    void Start()
    {

        Application.targetFrameRate = 60;
        _coolTime = 0f;

        _curNumber = 0;
        _imageInfo = new GameObject[] { _image1v1, _image2v2, _image3v3, _image4v4, _imageBackToTitle };
        _rectTransform = gameObject.GetComponent<RectTransform>();

        gameObject.SetActive(false);
        _isCanDecide = false;
        _isDecide = false;

        _audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        bool is_down = false;
        bool is_up = false;

        //  �p�b�h�̏\���{�^���擾(���̐��F�E�@���̐��F��)
        float input_button;
        input_button = Input.GetAxis("Vertical");

        //  �p�b�h�̃X�e�B�b�N�l�擾
        float input_stick = Input.GetAxis("Vertical");

        if (input_button > 0 || input_stick > 0)
            is_up = true;

        if (input_button < 0 || input_stick < 0)
            is_down = true;

        if (_coolTime <= 0f)
        {
            //  ���Ɉړ�
            if (Input.GetKeyDown(KeyCode.DownArrow) || is_down)
            {
                _curNumber++;
                if (_curNumber >= _imageNum)
                {
                    _curNumber = 0;
                }

                _coolTime = 0.4f;

                //  �J�[�\�����Đ�
                _audioSource.PlayOneShot(_cursor);
            }

            //  ��Ɉړ�
            if (Input.GetKeyDown(KeyCode.UpArrow) || is_up)
            {
                _curNumber--;
                if (_curNumber < 0)
                {
                    _curNumber = _imageNum - 1;
                }

                _coolTime = 0.4f;

                //  �J�[�\�����Đ�
                _audioSource.PlayOneShot(_cursor);
            }
        }
        else
        {
            _coolTime -= Time.deltaTime;

            if (!Input.GetKeyDown(KeyCode.DownArrow) && !is_up &&
                !Input.GetKeyDown(KeyCode.UpArrow) && !is_down)
            {
                _coolTime = 0f;
            }
        }


        //  ����
        if (_isCanDecide)
        {
            if (!_isDecide)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Enter"))
                {
                    for (int i = 0; i < _imageInfo.Length; i++)
                    {
                        if (i == _curNumber)
                        {
                            _imageInfo[i].transform.GetComponent<SelectUI>().LetsSelect();
                            selectNumber_ = (i + 1) * 2;
                        }
                        else
                            _imageInfo[i].transform.GetComponent<SelectUI>().LetsNotSelect();
                    }

                    gameObject.SetActive(true);
                    _isDecide = true;
                    //  ���艹�Đ�
                    _audioSource.PlayOneShot(_diside);
                }
            }
        }

        //  ���W�𔽉f
        Vector3 pos = _rectTransform.localPosition;
        pos.y = _imageInfo[_curNumber].gameObject.GetComponent<RectTransform>().localPosition.y;
        _rectTransform.localPosition = pos;


        if(_isDecide)
        {
            _countParam1 -= Time.deltaTime;

            if (_isDebug) { _countParam1 = -1f; }

            if(_countParam1 < 0f)
            {
                //  �I��
                MyFade._myFade.LoadLevel("CharactorSelect", 1f);
            }
        }
    }

    public void SetIsDecide(bool is_decide)
    {
        _isCanDecide = is_decide;
    }

    public void StartingUI()
    {
        for (int i = 0; i < _imageInfo.Length; i++)
        {          
            _imageInfo[i].transform.GetComponent<SelectUI>().SetIsStart(true);  
        }
    }

    private void ChangeScene()
    {
        //  �L�����I����ʂɔ��
        SceneManager.LoadScene("CharactorSelect");
    }

    public bool GetIsDebug()
    {
        return _isDebug;
    }
}
