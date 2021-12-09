using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class PhotonTeamView : MonoBehaviour, IPunObservable
{
    private PhotonView photonView;
    public int _redTeamNum;
    public int _blueTeamNum;
   
    public int RedTeamNum
    {
        get { return _redTeamNum; }
        set { _redTeamNum = value; RequestOwner(); }
    }

    public int BlueTeamNum
    {
        get { return _blueTeamNum; }
        set { _blueTeamNum = value; RequestOwner(); }
    }

    void Awake()
    {
        this.photonView = GetComponent<PhotonView>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // オーナーの場合
        if (stream.IsWriting)
        {
            stream.SendNext(this._redTeamNum);
            stream.SendNext(this._blueTeamNum);
        }
        // オーナー以外の場合
        else
        {
            this._redTeamNum = (int)stream.ReceiveNext();
            this._blueTeamNum = (int)stream.ReceiveNext();
        }
    }

    private void RequestOwner()
    {
        if (this.photonView.IsMine == false)
        {
            if (this.photonView.OwnershipTransfer != OwnershipOption.Request)
                Debug.LogError("OwnershipTransferをRequestに変更してください。");
            else
                this.photonView.RequestOwnership();
        }
    }
}