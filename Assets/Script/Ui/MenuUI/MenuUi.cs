using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.EventSystems;

public class MenuUi : MonoBehaviour
{
    public Image[] _menuUi;

    public bool _isOpenMenu { get; set; }

    //  メニュー画面タイプ
    public enum MenuType
    {
        MENU_TYPE_OPTION,       //オプション
        MENU_TYPE_SETTING,      //設定
        MENU_TYPE_CAM_SENSI,    //カメラ感度
        MENU_TYPE_VALUME,       //音量
        MENU_TYPE_OPERETION,    //操作設定

        MENU_TYPE_NUM           //要素数
    }

    [SerializeField]
    private GameObject[] _firstSelectedObjects = new GameObject[(int)MenuType.MENU_TYPE_NUM];

    // Start is called before the first frame update
    void Start()
    {
        _isOpenMenu = false;
        EventSystem.current.SetSelectedGameObject(_firstSelectedObjects[(int)MenuType.MENU_TYPE_OPTION]);
    }

    // Update is called once per frame
    void Update()
    {
        //  スタートボタンで開閉
        if (Input.GetButtonDown("Option"))
        {
            //  開いていなかったらメニュー開く
            if (!_isOpenMenu)
            {
                OpenMenu();
            }
            //  開かれていたら閉じる
            else if(_isOpenMenu)
            {
                DeleteMenu();
            }
        }

    }

    //  メニュー画面開く
    public void OpenMenu()
    {
        _menuUi[(int)MenuType.MENU_TYPE_OPTION].gameObject.SetActive(true);
        _isOpenMenu = true;
    }

    //  メニュー画面削除
    public void DeleteMenu()
    {
        foreach (Image image in _menuUi)
        {
            image.gameObject.SetActive(false);
            _isOpenMenu = false;
        }
    }

    //  アクティブセット
    public void SetMenuActive(MenuType type, bool is_active)
    {
        _menuUi[(int)type].gameObject.SetActive(is_active);
    }

    //  設定画面開く
    public void OnSetting()
    {
        //  メニュー画面を閉じて設定画面を開く
        SetMenuActive(MenuType.MENU_TYPE_OPTION, false);
        SetMenuActive(MenuType.MENU_TYPE_SETTING, true);

        EventSystem.current.SetSelectedGameObject(_firstSelectedObjects[(int)MenuType.MENU_TYPE_SETTING]);

    }

    //  音量
    public void OnValume()
    {
        //  設定画面を閉じて音量設定画面を開く
        SetMenuActive(MenuType.MENU_TYPE_SETTING, false);
        SetMenuActive(MenuType.MENU_TYPE_VALUME, true);

        EventSystem.current.SetSelectedGameObject(_firstSelectedObjects[(int)MenuType.MENU_TYPE_VALUME]);
    }

    //  カメラ感度
    public void OnCamSensi()
    {
        //  設定画面を閉じてカメラ設定画面を開く
        SetMenuActive(MenuType.MENU_TYPE_SETTING, false);
        SetMenuActive(MenuType.MENU_TYPE_CAM_SENSI, true);
        EventSystem.current.SetSelectedGameObject(_firstSelectedObjects[(int)MenuType.MENU_TYPE_CAM_SENSI]);

    }

    //  操作説明画面開く
    public void OnOperetion()
    {
        //  メニュー画面を閉じて設定画面を開く
        SetMenuActive(MenuType.MENU_TYPE_OPTION, false);
        SetMenuActive(MenuType.MENU_TYPE_OPERETION, true);

        EventSystem.current.SetSelectedGameObject(_firstSelectedObjects[(int)MenuType.MENU_TYPE_OPERETION]);
    }
}
