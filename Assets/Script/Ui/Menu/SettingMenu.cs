using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviourPunCallbacks
{
    public Image _optionUi;       //オプション画面
    public Image _settingUi;        //設定画面
    public Image _caneraSettingUi;  //カメラ感度設定
    public Image _valumeSettingUi;  //音量設定
    public Image _operationUi;      //操作説明UI;
    public bool _isMenu;        //メニュー画面が開かれているか

    public Image[] _menuUis;    //メニュー関連のUI

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
            if(_isMenu == false)
            {
                //  セッティング画面表示
                _optionUi.gameObject.SetActive(true);
                _isMenu = true;
            }
            else if(_isMenu == true)
            {

            }
        }
    }
}
