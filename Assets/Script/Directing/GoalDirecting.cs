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
    private Vector3 _initGoalPos;   //�ړ��O�̏����ʒu
    [SerializeField]
    private Vector3 _initTeamPos;  //�ړ��O�̏����ʒu
    [SerializeField]
    private Image _goalImage;        //�S�[���摜
    [SerializeField]
    private Image[] _teamImages;        //�S�[�������`�[���摜

    private RectTransform _Goalrect;   //Ui�ړ��p���W
    private RectTransform _Teamrect;   //�`�[��Ui�ړ��p���W

    private float _dispCount;  //�\�����ԃJ�E���g

    private enum Phase
    { 
        Idle,       //�ҋ@
        Move,       //�ړ�
        Display,    //�\��
    }
    private Phase _phase;

    private enum GoalTeam
    {
        TeamNone = -1,

        Red,
        Blue,
    }
    private GoalTeam _goalTeam;

    // Start is called before the first frame update
    void Start()
    {
        _Goalrect = _goalImage.GetComponent<RectTransform>();
        _phase = Phase.Idle;
        _dispCount = 0.0f;
        _goalTeam = GoalTeam.TeamNone;
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
                    //  �S�[��UI�ړ�
                    Vector3 pos = _Goalrect.localPosition;
                    pos.x -= _moveSpeed * Time.unscaledDeltaTime * 15;

                    //  �`�[��UI�ړ�
                    Vector3 team_pos = _Teamrect.localPosition;
                    team_pos.x += _moveSpeed * Time.unscaledDeltaTime * 15;


                    //  ���W�𔽉f
                    _Goalrect.localPosition = pos;
                    _Teamrect.localPosition = team_pos;

                    //  �ړ��I���
                    if (pos.x < 0)
                    {
                        //  �\���t�F�[�Y��
                        pos.x = 0.0f;
                        _phase = Phase.Display;
                    }

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

    //  =======�S�[�������������̉��o���Ă�==========

    [PunRPC]
   //   �ԃS�[��
    public void RequestRedGoalDirecting()
    {
        //  �Q�[�����X���[���[�V�����ɂ���
        Time.timeScale = _slowTime;

        //  �ړ��t�F�[�Y�Ɉڍs
        _phase = Phase.Move;

        //  �ԃ`�[��
        _goalTeam = GoalTeam.Red;
        _Teamrect = _teamImages[(int)_goalTeam].gameObject.GetComponent<RectTransform>();

        //  ���X�^�[�g�̃t���O��܂��Ă���
        GameObject.FindGameObjectWithTag("playManager").GetComponent<PlayScene>()._isGoalDirecting = false;
    }

    [PunRPC]
    //  �S�[��
    public void RequestBlueGoalDirecting()
    {
        //  �Q�[�����X���[���[�V�����ɂ���
        Time.timeScale = _slowTime;

        //  �ړ��t�F�[�Y�Ɉڍs
        _phase = Phase.Move;

        //  �`�[���ݒ�
        _goalTeam = GoalTeam.Blue;
        _Teamrect = _teamImages[(int)_goalTeam].gameObject.GetComponent<RectTransform>();

        //  ���X�^�[�g�̃t���O��܂��Ă���
        GameObject.FindGameObjectWithTag("playManager").GetComponent<PlayScene>()._isGoalDirecting = false;
    }

    //  �S�[�����o�I��
    private void FinGoalDirecting()
    {
        //  ���Ԃ𐳏�ɖ߂�
        Time.timeScale = 1.0f;

        //  Ui�������ʒu�ɐݒ�
        Vector3 pos = _Goalrect.localPosition;
        pos = _initGoalPos;
        _Goalrect.localPosition = pos;

        Vector3 team_pos = _Teamrect.localPosition;
        team_pos = _initTeamPos;
        _Teamrect.localPosition = team_pos;

        //  �\�����ԃ��Z�b�g
        _dispCount = 0.0f;

        //  �ҋ@�t�F�[�Y�Ɉڍs
        _phase = Phase.Idle;

        //  ���X�^�[�g�֐�
        GameObject.FindGameObjectWithTag("playManager").GetComponent<PhotonView>().RPC("RequestRestartGame", RpcTarget.MasterClient);
    }
}
