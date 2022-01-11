using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOwnerView : MonoBehaviour, IPunObservable
{
    private PhotonView photonView;

    void Awake()
    {
        this.photonView = GetComponent<PhotonView>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // オーナーの場合
        if (stream.IsWriting)
        {

        }
        // オーナー以外の場合
        else
        {

        }
    }

    public void RequestOwner()
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