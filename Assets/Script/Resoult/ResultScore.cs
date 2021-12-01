using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScore : MonoBehaviour
{
    private string[] _scoreText;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText = new string[CharaScoreManager._playerCnt];
         

        for (int i = 0; i < CharaScoreManager._playerCnt; i++)
        {
            CharaScore.CharaScoreInfo info = CharaScoreManager._allScoreInfo[i];

            _scoreText[i] = "���O�F���@�{�[���F"+ info._ballAtk + 
                "�O���b�v��" + info._grap;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
