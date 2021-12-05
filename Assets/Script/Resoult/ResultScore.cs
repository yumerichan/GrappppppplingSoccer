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



    private ResultScoreInfo[] _resultInfo;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText = new string[CharaScoreManager._playerCnt];
        _resultInfo = new ResultScoreInfo[8];

        for (int i = 0; i < 8; i++)
        {
            _resultInfo[i]._nameTxt = RESULT.transform.Find("Name" + (i + 1)).gameObject.GetComponent<Text>();
            _resultInfo[i]._goalTxt = RESULT.transform.Find("Goal" + (i + 1)).gameObject.GetComponent<Text>();
            _resultInfo[i]._scoreTxt = RESULT.transform.Find("Score" + (i + 1)).gameObject.GetComponent<Text>();
            _resultInfo[i]._teamTxt = RESULT.transform.Find("Team" + (i + 1)).gameObject.GetComponent<Text>();
        }

        //CharaScore.CharaScoreInfo info1 = CharaScoreManager._allScoreInfo[0];
        //CharaScoreManager._allScoreInfo[0] = CharaScoreManager._allScoreInfo[2];
        //CharaScoreManager._allScoreInfo[2] = info1;

        for (int i = 0; i < CharaScoreManager._playerCnt; i++)
        {
            CharaScore.CharaScoreInfo info = CharaScoreManager._allScoreInfo[i];

            _resultInfo[i]._nameTxt.text = info._name;
            _resultInfo[i]._goalTxt.text = info._goalNum.ToString();
            _resultInfo[i]._scoreTxt.text = info._allScore.ToString();

            if (info._teamKind == 0)
            {
                _resultInfo[i]._teamTxt.text = "Red";
                _resultInfo[i]._teamTxt.color = Color.red;
            }
            else
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
