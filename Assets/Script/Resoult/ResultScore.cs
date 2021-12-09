using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class ResultScore : MonoBehaviour
{
    private string[] _scoreText;

    public Text _text;

    public GameObject RESULT;
    public struct ResultScoreInfo
    {
        public Text _nameTxt;
        public Text _goalTxt;
        public Text _teamTxt;
        public Text _scoreTxt;
    }

    private int _myTeamNumber;

    private ResultScoreInfo[] _resultInfo;

    // Start is called before the first frame update
    void Start()
    {
        //まずは自分のチームナンバーを入れる
        for (int i = 0; i < 8; i++)
        {
            CharaScore.CharaScoreInfo info = CharaScoreManager._allScoreInfo[i];
            if (info._isMine) { _myTeamNumber = info._teamKind; }
        }

            _scoreText = new string[CharaScoreManager._playerCnt];
        _resultInfo = new ResultScoreInfo[8];

        for (int i = 0; i < 8; i++)
        {
            _resultInfo[i]._nameTxt = RESULT.transform.Find("Name" + (i + 1)).gameObject.GetComponent<Text>();
            _resultInfo[i]._goalTxt = RESULT.transform.Find("Goal" + (i + 1)).gameObject.GetComponent<Text>();
            _resultInfo[i]._scoreTxt = RESULT.transform.Find("Score" + (i + 1)).gameObject.GetComponent<Text>();
            _resultInfo[i]._teamTxt = RESULT.transform.Find("Team" + (i + 1)).gameObject.GetComponent<Text>();
        }

        //narabikae
        for (int i = 0; i < CharaScoreManager._allScoreInfo.Length; i++)
        {
            for (int j = i + 1; j < CharaScoreManager._allScoreInfo.Length; j++)
            {
                if (CharaScoreManager._allScoreInfo[i]._allScore <
                    CharaScoreManager._allScoreInfo[j]._allScore)
                {
                    CharaScore.CharaScoreInfo tmp = CharaScoreManager._allScoreInfo[i];
                    CharaScoreManager._allScoreInfo[i] = CharaScoreManager._allScoreInfo[j];
                    CharaScoreManager._allScoreInfo[j] = tmp;
                }
                else if (CharaScoreManager._allScoreInfo[i]._allScore ==
                    CharaScoreManager._allScoreInfo[j]._allScore)
                {
                    if (CharaScoreManager._allScoreInfo[i]._goalNum <
                    CharaScoreManager._allScoreInfo[j]._goalNum)
                    {
                        CharaScore.CharaScoreInfo tmp = CharaScoreManager._allScoreInfo[i];
                        CharaScoreManager._allScoreInfo[i] = CharaScoreManager._allScoreInfo[j];
                        CharaScoreManager._allScoreInfo[j] = tmp;
                    }
                }
            }
        }


        for (int i = 0; i < 8; i++)
        {
            CharaScore.CharaScoreInfo info = CharaScoreManager._allScoreInfo[i];

            if (info._isData == false) { continue; }

            if(info._isMine)
            {
                _resultInfo[i]._nameTxt.text = "YOU";
            }
            else if(info._teamKind == _myTeamNumber)
            {
                _resultInfo[i]._nameTxt.text = "ALLY";
            }
            else if(info._teamKind != _myTeamNumber)
            {
                _resultInfo[i]._nameTxt.text = "ENEMY";
            }
            
            _resultInfo[i]._goalTxt.text = info._goalNum.ToString();
            _resultInfo[i]._scoreTxt.text = info._allScore.ToString();

            if (info._teamKind == 0)
            {
                _resultInfo[i]._teamTxt.text = "Red";
                _resultInfo[i]._teamTxt.color = Color.red;
            }
            else if(info._teamKind == 1)
            {
                _resultInfo[i]._teamTxt.text = "Blue";
                _resultInfo[i]._teamTxt.color = Color.blue;
            }

        }


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
