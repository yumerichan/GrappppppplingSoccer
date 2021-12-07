using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GoalDirecting : MonoBehaviour
{
    [SerializeField]
    private float _dispTime; //�\�����鎞��
    [SerializeField]
    private float _moveSpeed;   //Ui���ړ����鑬��
    [SerializeField]
    private float _slowTime;    //�Q�[�����Ԃ��ǂꂾ���x���Ȃ邩
    [SerializeField]
    private Vector3 _initPos;   //�ړ��O�̏����ʒu

    private RectTransform _rect;   //Ui�ړ��p���W

    private float _dispCount;  //�\�����ԃJ�E���g

    private enum Phase
    { 
        Idle,       //�ҋ@
        Move,       //�ړ�
        Display,    //�\��
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
        //  �t�F�[�Y���Ƃ̏�������
        switch (_phase)
        {
            //  �ҋ@
            case Phase.Idle:
                {
                    //  ���ɂȂ�
                }
                break;

            //  �ړ�
            case Phase.Move: 
                {
                    //  Ui�̍��W���Q��
                    Vector3 pos = _rect.localPosition;
                    pos.x -= _moveSpeed * Time.unscaledDeltaTime * 15;

                    //  �ړ��I���
                    if(pos.x < 0)
                    {
                        //  �\���t�F�[�Y��
                        pos.x = 0.0f;
                        _phase = Phase.Display;
                    }

                    //  ���W�𔽉f
                    _rect.localPosition = pos;
                }
                break;

            //  �\��
            case Phase.Display:
                {
                    //   �\�����Ԃ��J�E���g
                    _dispCount += Time.unscaledDeltaTime;

                    //  �\�����Ԃ��߂��Ă����牉�o�I��
                    if (_dispCount > _dispTime)
                        FinGoalDirecting();
                }
                break;
        }
    }

    [PunRPC]
    //  �S�[�������������̉��o���Ă�
    public void RequestGoalDirecting()
    {
        //  �Q�[�����X���[���[�V�����ɂ���
        Time.timeScale = _slowTime;

        //  �ړ��t�F�[�Y�Ɉڍs
        _phase = Phase.Move;
    }

    //  �S�[�����o�I��
    private void FinGoalDirecting()
    {
        //  ���Ԃ𐳏�ɖ߂�
        Time.timeScale = 1.0f;

        //  Ui�������ʒu�ɐݒ�
        Vector3 pos = _rect.localPosition;
        pos = _initPos;
        _rect.localPosition = pos;

        //  �\�����ԃ��Z�b�g
        _dispCount = 0.0f;

        //  �ҋ@�t�F�[�Y�Ɉڍs
        _phase = Phase.Idle;

        //  ���X�^�[�g�֐�
        GameObject.FindGameObjectWithTag("playManager").GetComponent<PlayScene>().RequestRestartGame();
    }
}
