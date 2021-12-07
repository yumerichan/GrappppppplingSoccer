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
    private NewWorkInfo _nwInfo;
    private int i;

    // Start is called before the first frame update
    void Start()
    {
        _allScoreInfo = new CharaScore.CharaScoreInfo[8];
        _playerCnt = 0;
        _all = new CharaScore[8];
        _nwInfo = GameObject.Find("NetWork").GetComponent<NewWorkInfo>();
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
}
