using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PunCallback : MonoBehaviour, IPunOwnershipCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    // ���L���̃��N�G�X�g���s��ꂽ���ɌĂ΂��R�[���o�b�N
    void IPunOwnershipCallbacks.OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        // ���g�����L�������C���X�^���X�ŏ��L���̃��N�G�X�g���s��ꂽ��A��ɋ����ď��L�����ڏ�����
        if (targetView.IsMine)
        {
            bool acceptsRequest = true;
            if (acceptsRequest)
            {
                targetView.TransferOwnership(requestingPlayer);
            }
            else
            {
                // ���N�G�X�g�����ۂ���ꍇ�́A�������Ȃ�
            }
        }
    }

    // ���L���̈ڏ����s��ꂽ���ɌĂ΂��R�[���o�b�N
    void IPunOwnershipCallbacks.OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        string id = targetView.ViewID.ToString();
        string p1 = previousOwner.NickName;
        string p2 = targetView.Owner.NickName;
        Debug.Log($"ViewID {id} �̏��L���� {p1} ���� {p2} �Ɉڏ�����܂���");
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        throw new System.NotImplementedException();
    }
}