using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharactorControll : MonoBehaviourPunCallbacks
{
     private void Update()
    {
        // ���g�����������I�u�W�F�N�g�����Ɉړ��������s��
        if (photonView.IsMine)
        {
            var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            transform.Translate(6f * Time.deltaTime * input.normalized);
        }
    }
}