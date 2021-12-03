using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class CharaScoreManager : MonoBehaviourPunCallbacks
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

            for (int j = 0; j < i; j++)
            {
                _all[j].SendScoreInfo();
            }

            SceneManager.LoadScene("ResultScene");
        }
    }

    public void ReceiveScoreInfo(CharaScore.CharaScoreInfo info)
    {
        _allScoreInfo[_playerCnt] = info;
        _playerCnt++;
    }

    public void ReceiveCharaScore(CharaScore score)
    {
        _all[i] = score;
        i++;
    }
}
