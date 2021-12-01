using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaScoreManager : MonoBehaviour
{
    public static CharaScore.CharaScoreInfo[] _allScoreInfo;
    public static int _playerCnt;

    // Start is called before the first frame update
    void Start()
    {
        _allScoreInfo = new CharaScore.CharaScoreInfo[8];
        _playerCnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveScoreInfo(CharaScore.CharaScoreInfo info)
    {
        _allScoreInfo[_playerCnt] = info;
        _playerCnt++;
    }
}
