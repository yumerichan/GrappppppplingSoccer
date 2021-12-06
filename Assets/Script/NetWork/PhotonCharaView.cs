using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class PhotonCharaView : MonoBehaviour, IPunObservable
{
    private PhotonView photonView;

    public string _text;
    public int _aaa;

    public string Text
    {
        get { return _text; }
        set { _text = value; RequestOwner(); }
    }

    public int AAA
    {
        get { return _aaa; }
        set { _aaa = value; RequestOwner(); }
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
            stream.SendNext(this._text);
            stream.SendNext(this._aaa);
        }
        // �I�[�i�[�ȊO�̏ꍇ
        else
        {
            this._text = (string)stream.ReceiveNext();
            this._aaa = (int)stream.ReceiveNext();
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