using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class PhotonCharaView : MonoBehaviour, IPunObservable
{
    private PhotonView photonView;

    public string _text;
    public int _allPlayerNum;
    public int _redNum;
    public int _blueNum;
    public int _redTeamNum;
    public int _blueTeamNum;

    public string Text
    {
        get { return _text; }
        set { _text = value; RequestOwner(); }
    }

    public int AllPlayerNum
    {
        get { return _allPlayerNum; }
        set { _allPlayerNum = value; RequestOwner(); }
    }

    public int RedNum
    {
        get { return _redNum; }
        set { _redNum = value; RequestOwner(); }
    }

    public int BlueNum
    {
        get { return _blueNum; }
        set { _blueNum = value; RequestOwner(); }
    }

    public int RedTeamNum
    {
        get { return _redTeamNum; }
        set { _redNum = value; RequestOwner(); }
    }

    public int BlueTeamNum
    {
        get { return _blueTeamNum; }
        set { _blueNum = value; RequestOwner(); }
    }

    void Awake()
    {
        this.photonView = GetComponent<PhotonView>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // �I�[�i�[�̏ꍇ
        if (stream.IsWriting)
        {
            stream.SendNext(this._allPlayerNum);
            stream.SendNext(this._redNum);
            stream.SendNext(this._blueNum);
            stream.SendNext(this._redTeamNum);
            stream.SendNext(this._blueTeamNum);
        }
        // �I�[�i�[�ȊO�̏ꍇ
        else
        {
            this._allPlayerNum = (int)stream.ReceiveNext();
            this._redNum = (int)stream.ReceiveNext();
            this._blueNum = (int)stream.ReceiveNext();
            this._redTeamNum = (int)stream.ReceiveNext();
            this._blueTeamNum = (int)stream.ReceiveNext();
        }
    }

    private void RequestOwner()
    {
        if (this.photonView.IsMine == false)
        {
            if (this.photonView.OwnershipTransfer != OwnershipOption.Request)
                Debug.LogError("OwnershipTransfer��Request�ɕύX���Ă��������B");
            else
                this.photonView.RequestOwnership();
        }
    }
}