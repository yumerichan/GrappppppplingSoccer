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

    /* サウンド関連 */
    private AudioSource _audioSource;   //オーディオソース
    public AudioClip _playBgm;           //プレイBGM（切り替えよう）

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

                //  両方のチームが埋まったら
                if (_curRed == _teamMaxNum &&
                    _curBlue == _teamMaxNum)
                {
                    //タイマー開始
                    GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

                    //  BGM切り替え
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
