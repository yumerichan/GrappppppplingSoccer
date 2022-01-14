using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class GameMng : MonoBehaviour
{
    private int _teamMaxNum;

    private bool _isStart;

    public int _curRed;
    public int _curBlue;

    /* �T�E���h�֘A */
    private AudioSource _audioSource;   //�I�[�f�B�I�\�[�X
    public AudioClip _playBgm;           //�v���CBGM�i�؂�ւ��悤�j

    // Start is called before the first frame update
    void Start()
    {
        _teamMaxNum = ArrowUI.selectNumber_ / 2;
        _isStart = false;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!_isStart)
        {
            if (GameObject.Find("CharaViewManager(Clone)").
               GetComponent<PhotonCharaView>() != null)
            {
                PhotonCharaView view = GameObject.Find("CharaViewManager(Clone)").
                   GetComponent<PhotonCharaView>();
                _curRed = view._redNum;
                _curBlue = view._blueNum;

                //  �����̃`�[�������܂�����
                if (_curRed == _teamMaxNum &&
                    _curBlue == _teamMaxNum)
                {
                    //�^�C�}�[�J�n
                    GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

                    //  BGM�؂�ւ�
                    _audioSource.clip = _playBgm;
                    _audioSource.Play();

                    for (int i = 0; i < player.Length; i++)
                    {
                        player[i].GetComponent<PhotonView>()
                            .RPC("StartGame", RpcTarget.All);
                        _isStart = true;
                    }
                }
            }
        }
    }
}
