using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalScore : MonoBehaviour
{
    //  ���_�\���p
    public Text _redScoreText;
    public Text _buleScoreText;

    //  ���_
    private int _redScore;
    private int _buleScore;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    //  �ԃ`�[���X�R�A���Z
    public void AddRedScore()
    {
        _redScore += 1;
        _redScoreText.text = _redScore.ToString();
    }

    //  �`�[���X�R�A���Z
    public void AddBuleScore()
    {
        _buleScore += 1;
        _buleScoreText.text = _buleScore.ToString();
    }
}
