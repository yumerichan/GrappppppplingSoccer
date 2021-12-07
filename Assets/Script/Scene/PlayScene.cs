using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviourPunCallbacks
{
    //  �ԃ`�[���̃v���C���[���X�g
    public List<GameObject> _redPlayerList { get; set; } = new List<GameObject>();

    //  �`�[���̃v���C���[���X�g
    public List<GameObject> _bluePlayerList { get; set; } = new List<GameObject>();

    //  �ԃ`�[���������W
    private Vector3[] _redInitPos = new [] { 
        new Vector3(0, 0 ,50),
        new Vector3(0, 0 ,0),
        new Vector3(0, 0 ,0),
        new Vector3(0, 0 ,0),
    };

    //  �`�[���������W
    private Vector3[] _blueInitPos = new[] {
        new Vector3(0, 0 ,-50),
        new Vector3(0, 0 ,0),
        new Vector3(0, 0 ,0),
        new Vector3(0, 0 ,0),
    };

    //  �{�[���̃Q�[���I�u�W�F�N�g
    public GameObject _ball;

    private NewWorkInfo nw_info;

    //�S�̂Ŏg��
    static public bool m_IsStart;
    private bool m_IsEnd;

    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 60;

        m_IsStart = false;
        m_IsEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        ////�J�n���~�߂�
        //if(m_IsStart == false)
        //    StartCoroutine("StartStop");

        ////�I������
        //if(m_IsEnd == true)
        //     StartCoroutine("EndStop");

        //�V�[���ōs����������
    }

    //  �Q�[���X�^�[�g
    public void RequestRestartGame()
    {
        //  �����ʒu�ݒ�

        //  �e�`�[���J�E���g
        int red_count = 0;
        int blue_count = 0;

        //  �v���C���[
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in players)
        {
            //  �ԃ`�[��
            if (obj.GetComponent<CharactorBasic>()._teamColor == 0)
            {
                obj.transform.position = _redInitPos[red_count];
                red_count++;
            }
            //  �`�[��
            else
            {
                obj.transform.position = _blueInitPos[red_count];
                blue_count++;
            }
        }

        //  �{�[��
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _ball.transform.position = new Vector3(0, 80, 0);

        //  �X�^�[�g�̉��o ==============
        //  �o�����炱���Ń��N�G�X�g����

    }

    //  �ԃ`�[���ǉ�
    public void AddRedPlayer(GameObject player)
    {
        _redPlayerList.Add(player);
    }

    //  �`�[���ǉ�
    public void AddBluePlayer(GameObject player)
    {
        _bluePlayerList.Add(player);
    }

    /*
      
      �����������񏈗�����O���Ƃ��܂�




      //�J�n����
      //�ŏ��͎~�߂������߂����Ńt���O��܂�
      IEnumerator StartStop()
        {
            yield return new WaitForSeconds(0);  //�҂�

            //�҂��Ă�ԂɃt�F�[�h��������

            if (PhotonNetwork.CurrentRoom.IsOpen == false)
            {
                //�J�n����

                yield return new WaitForSeconds(3);  //3�b�҂�

                m_IsStart = true;
            }
        }

        //�I������
        //������������ł����
        IEnumerator EndStop()
        {
            yield return new WaitForSeconds(0);  //�҂�

            //�҂��Ă�ԂɃt�F�[�h��������

            //�t�F�[�h���I������t���O�ōs������
            SceneManager.LoadScene("result");
        }

    */
}
