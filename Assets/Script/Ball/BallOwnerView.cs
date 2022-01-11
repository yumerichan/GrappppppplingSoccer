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
        // �I�[�i�[�̏ꍇ
        if (stream.IsWriting)
        {

        }
        // �I�[�i�[�ȊO�̏ꍇ
        else
        {

        }
    }

    public void RequestOwner()
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