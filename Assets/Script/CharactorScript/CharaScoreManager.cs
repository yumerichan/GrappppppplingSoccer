using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class CharaScoreManager : MonoBehaviour
{
    public static CharaScore.CharaScoreInfo[] _allScoreInfo;
    public static int _playerCnt;

    private CharaScore[] _all;
    private int i;

    private GameObject _minePlayer;

    // Start is called before the first frame update
    void Start()
    {
        _allScoreInfo = new CharaScore.CharaScoreInfo[8];

        for(int i = 0;i < _allScoreInfo.Length;i++)
        {
            _allScoreInfo[i]._goal = _allScoreInfo[i]._grap = _allScoreInfo[i]._skill = _allScoreInfo[i]._skillCnt = 0;
            _allScoreInfo[i]._ballAtk = 0;
            _allScoreInfo[i]._teamKind = -1;
            _allScoreInfo[i]._goalNum = 0;
            _allScoreInfo[i]._isMine = false;
            _allScoreInfo[i]._isData = false;
        }
        _playerCnt = 0;
        _all = new CharaScore[8];
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        

        //  デバッグ終了
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {

            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

            for(int i = 0;i < player.Length;i++)
            {
                _allScoreInfo[i] = player[i].
                    GetComponent<CharaScore>()._scoreInfo;

                if(player[i] == _minePlayer)
                {
                    _allScoreInfo[i]._isMine = true;
                }
            }

            SceneManager.LoadScene("ResultScene");
        }
    }

    public void ReceiveScoreInfo(CharaScore.CharaScoreInfo info)
    {
        //int a = info._allScore;

        //CharaScore.CharaScoreInfo infoo;
        //infoo._allScore = a;
        //infoo._ballAtk = 10;
        //infoo._goal = 0;
        //infoo._goalNum = 12;
        //infoo._grap = 1;
        //infoo._name = "ff";
        //infoo._skill = 123;
        //infoo._skillCnt = 0;
        //infoo._teamKind = 0;

        _allScoreInfo[_playerCnt] = info;
        _playerCnt++;
    }

    public void ReceiveCharaScore(CharaScore score)
    {
        //_all[i] = score;
        i++;
    }

    public void SetMinePlayer(GameObject player)
    {
        _minePlayer = player;
    }

    public void LetsGoResult()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < player.Length; i++)
        {
            _allScoreInfo[i] = player[i].
                GetComponent<CharaScore>()._scoreInfo;

            if (player[i] == _minePlayer)
            {
                _allScoreInfo[i]._isMine = true;
            }
        }

        SceneManager.LoadScene("ResultScene");
    }
}
