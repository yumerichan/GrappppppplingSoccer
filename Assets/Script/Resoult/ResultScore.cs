using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    private string[] _scoreText;

    public Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText = new string[CharaScoreManager._playerCnt];
         

        for (int i = 0; i < CharaScoreManager._playerCnt; i++)
        {
            CharaScore.CharaScoreInfo info = CharaScoreManager._allScoreInfo[i];

            _scoreText[i] = "���O�F���@�{�[���F"+ info._ballAtk + 
                "�O���b�v���F" + info._grap + "�I�[���X�R�A�F" + info._allScore;
        }

        _text.text = _scoreText[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
