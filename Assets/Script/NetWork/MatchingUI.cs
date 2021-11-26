using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class MatchingUI : MonoBehaviourPunCallbacks
{
    Image image_;   //画像
    float opacity_; //透明度

    bool IsEnd;     //修了確認
    bool limitflag; //最大確認

    // Start is called before the first frame update
    void Start()
    {
        // 透明度の設定
        opacity_ = 1.0f;
        limitflag = false;
        IsEnd = false;
        // Imageの取得
        image_ = GetComponent<Image>();
        // 0=透明 1=不透明なので、1.0で完全に不透明になる
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
