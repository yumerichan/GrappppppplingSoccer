using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalScore : MonoBehaviour
{
    //  得点表示用
    public Text _redScoreText;
    public Text _buleScoreText;

    //  得点
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

    //  赤チームスコア加算
    public void AddRedScore()
    {
        _redScore += 1;
        _redScoreText.text = _redScore.ToString();
    }

    //  青チームスコア加算
    public void AddBuleScore()
    {
        _buleScore += 1;
        _buleScoreText.text = _buleScore.ToString();
    }
}
