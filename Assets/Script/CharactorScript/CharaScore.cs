using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharaScore : MonoBehaviourPunCallbacks
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
        public bool _isMine;
        public bool _isData;
    }

    public CharaScoreInfo _scoreInfo;
    private GameObject _scoreManager;
    private NewWorkInfo _nwInfo;

    public int _charaNumber;


    public SelectTeam _selectTeam;

    bool _isTeamSet;


    // Start is called before the first frame update
    void Start()
    {
        //if (!photonView.IsMine) { return; }

        _scoreManager = GameObject.Find("CharaScoreManager");
        _scoreManager.GetComponent<CharaScoreManager>().SetMinePlayer(this.gameObject);
        _scoreInfo._goal = _scoreInfo._grap = _scoreInfo._skill = _scoreInfo._skillCnt = 0;
        _scoreInfo._ballAtk = 0;
        _scoreInfo._teamKind = -1;
        _scoreInfo._goalNum = 0;
        _scoreInfo._isMine = false;
        _scoreInfo._isData = true;
        _nwInfo = GameObject.Find("NetWork").GetComponent<NewWorkInfo>();

        _isTeamSet = false;


        _selectTeam = GameObject.Find("GameCanvas").transform.Find("TeamSelectCanvas").gameObject.GetComponent<SelectTeam>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) { return; }

        StepTeamSet(); 
    }

    
    public void StepTeamSet()
    {
        if(_isTeamSet) { return; }
        if (_selectTeam.GetIsDecide() == false) { return; }


        if (GameObject.Find("CharaViewManager(Clone)").
                 GetComponent<PhotonCharaView>() != null)
        {
            if (_nwInfo.GetTeamColor() == 0)
            {
                PhotonCharaView view = GameObject.Find("CharaViewManager(Clone)").
                     GetComponent<PhotonCharaView>();

                int all_num = view._allPlayerNum;
                int red = view._redNum;
                int blue = view._blueNum;


                view.RedNum = red + 1;
                view.BlueNum = blue;
                view.AllPlayerNum = all_num + 1;


            }
            else if (_nwInfo.GetTeamColor() == 1)
            {
                PhotonCharaView view = GameObject.Find("CharaViewManager(Clone)").
                     GetComponent<PhotonCharaView>();

                int all_num = view._allPlayerNum;
                int red = view._redNum;
                int blue = view._blueNum;


                view.RedNum = red;
                view.BlueNum = blue + 1;
                view.AllPlayerNum = all_num + 1;
            }

            GetComponent<PhotonView>().RPC("SetInfo", RpcTarget.All); 
        }
    }

    [PunRPC]
    public void SetInfo()
    {
        _scoreInfo._teamKind = _selectTeam.GetTeamSelect();
        _isTeamSet = true;
        _scoreInfo._isData = true;
    }


    [PunRPC]
    public void AddGoal()
    {
        _scoreInfo._goal += 800;
        _scoreInfo._allScore += 800;

        _scoreInfo._goalNum++;
    }

    [PunRPC]
    public void AddSkill()
    {
        _scoreInfo._skill += 70;
        _scoreInfo._allScore += 70;
    }

    [PunRPC]
    public void AddSkillCnt()
    {
        _scoreInfo._skillCnt += 20;
        _scoreInfo._allScore += 20;
    }

    [PunRPC]
    public void AddGrap()
    {
        _scoreInfo._grap += 10;
        _scoreInfo._allScore += 10;
    }

    [PunRPC]
    public void AddBallAtk()
    {
        _scoreInfo._goal += 25;
        _scoreInfo._allScore += 25;
    }

    [PunRPC]
    public void SendScoreInfo(int i)
    {
        _scoreInfo._teamKind = _nwInfo.GetTeamColor();

        _scoreManager.transform.GetComponent<CharaScoreManager>
            ().ReceiveScoreInfo(_scoreInfo);
    }

    [PunRPC]
    public void StartGame()
    {
        GameObject.Find("GameCanvas/Score&Timer/Timer01/Text").GetComponent<GameTime>().StartPlay();
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
