using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviourPunCallbacks
{
    //  �����ʒu

    //  �ԃ`�[���̏����ʒu
    public Vector3[] _redTiemInitPos = new[] {
       new Vector3 ( 0, 0, 0 ), //���
       new Vector3 ( 0, 0, 0 ), //���
       new Vector3 ( 0, 0, 0 ), //�O��
       new Vector3 ( 0, 0, 0 ), //�l��
    };

    //  �`�[���̏����ʒu
    public Vector3[] _buleTiemInitPos = new[] {
       new Vector3 ( 0, 0, 0 ), //���
       new Vector3 ( 0, 0, 0 ), //���
       new Vector3 ( 0, 0, 0 ), //�O��
       new Vector3 ( 0, 0, 0 ), //�l��
    };

    //�S�̂Ŏg��
    static public bool m_IsStart;
    private bool m_IsEnd;

    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 60;

        m_IsStart = false;
        m_IsEnd = false;

        if (GameObject.FindGameObjectWithTag("Ball") == null)
            PhotonNetwork.Instantiate("Ball", new Vector3(0, 30, 0), Quaternion.identity);
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
