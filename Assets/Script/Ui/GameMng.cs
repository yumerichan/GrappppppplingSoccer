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


    // Start is called before the first frame update
    void Start()
    {
        _teamMaxNum = ArrowUI.selectNumber_ / 2;
        _isStart = false;
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
