using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class StartDirecting : MonoBehaviour
{
    //  ���X�^�[�g���o�t���O
    //  ���ꂪ�����Ă�����Q�[���͐i�s���Ȃ�
    public bool _isRestartDirection { get; set; }

    [SerializeField]
    private Image _image;       //�摜
    [SerializeField]
    private Text _text;         //�J�E���g�_�E���p�e�L�X�g
    [SerializeField]
    private float _countDownTime;   //�J�E���g�_�E���b��
    private int count;
    private float _curCountDown;   //�J�E���g�_�E��

    [SerializeField]
    private float _imageDispTime;       //�X�^�[�g�摜�\������
    private float _curImageDispTime;    //���݂̉摜�\������

    private enum Phase
    {
        Count,
        Display,
    }

    Phase _phase;

    // Start is called before the first frame update
    void Start()
    {
        //  ������
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

                        //  �c��0.1�b�ɂȂ�����X�^�[�g�摜�\��
                        if (_curCountDown < 0.1)
                        {
                            _image.gameObject.SetActive(true);

                            //  �J�E���g�_�E����\��
                            _text.gameObject.SetActive(false);

                            //  �t�F�[�Y�ȍ~
                            _phase = Phase.Display;
                        }
                    }
                    break;

                case Phase.Display:
                    {
                        _curImageDispTime += Time.deltaTime;

                        //  �\�����Ԃ��߂��Ă����牉�o�I��
                        if (_curImageDispTime > _imageDispTime)
                            FinRestartDirection();
                    }
                    break;
            }
           
        }
    }

    //  �X�^�[�g�֐����N�G�X�g
    [PunRPC]
    public void requestRestartDirection()
    {
        _isRestartDirection = true;
        _text.gameObject.SetActive(true);

    }

    //  �X�^�[�g���o�I��
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
