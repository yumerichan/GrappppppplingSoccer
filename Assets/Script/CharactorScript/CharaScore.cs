using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaScore : MonoBehaviour
{
    public struct CharaScoreInfo
    {
        public int _goalNum;
        public int _goal;
        public int _skill;
        public int _skillCnt;
        public int _grap;
        public int _ballAtk;

        public int _allScore;

        public int _teamKind;
        public string _name;
    }

    public CharaScoreInfo _scoreInfo;
    private GameObject _scoreManager;
    private NewWorkInfo _nwInfo;

    // Start is called before the first frame update
    void Start()
    {
        _scoreInfo._goal = _scoreInfo._grap = _scoreInfo._skill = _scoreInfo._skillCnt = 0;
        _scoreInfo._ballAtk = 0;
        _scoreInfo._teamKind = 0;
        _scoreInfo._goalNum = 0;
        _nwInfo = GameObject.Find("NetWork").GetComponent<NewWorkInfo>();
        _scoreManager = GameObject.Find("CharaScoreManager");
        SendThis();


    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void AddGoal()
    {
        _scoreInfo._goal += 800;
        _scoreInfo._allScore += 800;

        _scoreInfo._goalNum++;
    }

    public void AddSkill()
    {
        _scoreInfo._skill += 70;
        _scoreInfo._allScore += 70;
    }

    public void AddSkillCnt()
    {
        _scoreInfo._goal += 20;
        _scoreInfo._allScore += 20;
    }

    public void AddGrap()
    {
        _scoreInfo._grap += 10;
        _scoreInfo._allScore += 10;
    }

    public void AddBallAtk()
    {
        _scoreInfo._goal += 25;
        _scoreInfo._allScore += 25;
    }

    public void SendScoreInfo()
    {
        _scoreInfo._teamKind = _nwInfo.GetTeamColor();

        _scoreManager.transform.GetComponent<CharaScoreManager>
            ().ReceiveScoreInfo(_scoreInfo);
    }

    public void SendThis()
    {
        _scoreManager.GetComponent<CharaScoreManager>().ReceiveCharaScore(this);
    }

    public int GetTeamColor()
    {
        return _nwInfo.GetTeamColor();
    }
}
