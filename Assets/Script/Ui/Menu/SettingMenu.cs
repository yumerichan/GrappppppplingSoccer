using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviourPunCallbacks
{
    public bool _isMenu { get; set; }        //メニュー画面が開かれているか

    public Image[] _menuUis { get; set; }    //メニュー関連のUI

    public enum MenuUiType
    {
        MENU_TYPE_OPTION,       //オプション
        MENU_TYPE_SETTING,      //設定
        MENU_TYPE_VALUME,       //音量
        MENU_TYPE_CAM_SENSI,    //カメラ感度
        MENU_TYPE_OPERETION,    //操作説明

    }

    // Start is called before the first frame update
    void Start()
    {
        _isMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        if(Input.GetButtonDown("Option"))
        {
            //  メニューが開かれていない
            if(_isMenu == false)
            {
                //  セッティング画面表示
                _menuUis[(int)MenuUiType.MENU_TYPE_OPTION].gameObject.SetActive(true);
                _isMenu = true;
            }

            //  何かしらのメニューが開かれている場合
            else if(_isMenu == true)
            {
                DeleteUi();
            }
        }
    }

    private void DeleteUi()
    {
        //  すべてのメニューを非アクティブにして、メニューフラグを折る
        foreach (Image image in _menuUis)
        {
            image.gameObject.SetActive(false);
            _isMenu = false;
        }
    }

    //  Uiアクティブセット
    public void SetUi(MenuUiType type, bool active)
    {
        _menuUis[(int)type].gameObject.SetActive(active);
    }
}
