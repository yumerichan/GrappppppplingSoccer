using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class MatchingUI : MonoBehaviourPunCallbacks
{
    Image image_;   //�摜
    float opacity_; //�����x

    bool IsEnd;     //�C���m�F
    bool limitflag; //�ő�m�F

    // Start is called before the first frame update
    void Start()
    {
        // �����x�̐ݒ�
        opacity_ = 1.0f;
        limitflag = false;
        IsEnd = false;
        // Image�̎擾
        image_ = GetComponent<Image>();
        // 0=���� 1=�s�����Ȃ̂ŁA1.0�Ŋ��S�ɕs�����ɂȂ�
        //ImageCommon.SetOpacity(image_, opacity_);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsEnd == true) return;

        if (PhotonNetwork.CurrentRoom.IsOpen == true)
        {
            if (limitflag == false)
            {
                opacity_ -= 0.01f;

                if (opacity_ <= 0.0f)
                {
                    limitflag = true;
                }
            }
            else
            {
                opacity_ += 0.01f;

                if (opacity_ >= 1.0f)
                {
                    limitflag = false;
                }
            }

            //ImageCommon.SetOpacity(image_, opacity_);
        }
        else
        {
            opacity_ = 0.0f;
            //ImageCommon.SetOpacity(image_, opacity_);
            IsEnd = true;
        }
    }
}
