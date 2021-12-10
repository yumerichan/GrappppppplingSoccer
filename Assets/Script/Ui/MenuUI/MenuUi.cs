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

    private bool[] _MenuActive = new bool[(int)MenuType.MENU_TYPE_NUM];

    [SerializeField]
    private GameObject[] _firstSelectedObjects = new GameObject[(int)MenuType.MENU_TYPE_NUM];

    public enum VolumeType
    {
        VOLUME_TYPE_BGM,
        VOLUME_TYPE_SE,
        VOLUME_TYPE_MASTER,
        VOLUME_TYPE_NUM,
    }

    private int[] _volume = new int[(int)VolumeType.VOLUME_TYPE_NUM];
    public Text[] _volumetext;
    public enum CameraType
    {
        CAMERA_VER,
        CAMERA_HOR,
        CAMERA_NUM,
    }

    private int[] _camera = new int[(int)CameraType.CAMERA_NUM];
    public Text[] _cameratext;

    // Start is called before the first frame update
    void Start()
    {
        _isOpenMenu = false;
        EventSystem.current.SetSelectedGameObject(_firstSelectedObjects[(int)MenuType.MENU_TYPE_OPTION]);


        _volume[(int)VolumeType.VOLUME_TYPE_BGM] = (int)GameObject.Find("BgmVolSlider").GetComponent<Slider>().value;
        _volume[(int)VolumeType.VOLUME_TYPE_SE] = (int)GameObject.Find("SeVolSlider").GetComponent<Slider>().value;
        _volume[(int)VolumeType.VOLUME_TYPE_MASTER] = (int)GameObject.Find("MasterValSlider").GetComponent<Slider>().value;
        _camera[(int)CameraType.CAMERA_VER] = (int)GameObject.Find("VerSlider").GetComponent<Slider>().value;
        _camera[(int)CameraType.CAMERA_HOR] = (int)GameObject.Find("VerValueText").GetComponent<Slider>().value;
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


        CancelMenu();

        Volume();


    }

    private void CancelMenu()
    {
        //キャンセル時
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown("joystick button 0"))
        {
            int _backmenu = -1;

            for (int n = 0; n < (int)MenuType.MENU_TYPE_NUM; n++)
            {

                if (GetMenuActive((MenuType)n)) { _backmenu = n; break; }

            }
            if (_backmenu == (int)MenuType.MENU_TYPE_CAM_SENSI || _backmenu == (int)MenuType.MENU_TYPE_VALUME)
            {
                OnValumeToSetting();
                OnCamToSetting();
            }
            else
            {
                OnSettingToOption();
                OnOpeToOption();
            }

        }
    }

    private void Volume()
    {
        if (!GetMenuActive(MenuType.MENU_TYPE_VALUME)) return;

        _volumetext[(int)VolumeType.VOLUME_TYPE_BGM].text = /*_volume[(int)VolumeType.VOLUME_TYPE_BGM].ToString()*/"2121" + "%";
        _volumetext[(int)VolumeType.VOLUME_TYPE_SE].text = _volume[(int)VolumeType.VOLUME_TYPE_SE].ToString() + "%";
        _volumetext[(int)VolumeType.VOLUME_TYPE_MASTER].text = _volume[(int)VolumeType.VOLUME_TYPE_MASTER].ToString() + "%";
    }

    private void Camera()
    {
        if (!GetMenuActive(MenuType.MENU_TYPE_CAM_SENSI)) return;

        _cameratext[(int)CameraType.CAMERA_HOR].text = _camera[(int)CameraType.CAMERA_HOR].ToString() + "%";
        _cameratext[(int)CameraType.CAMERA_VER].text = _camera[(int)CameraType.CAMERA_VER].ToString() + "%";

    }

    //  メニュー画面開く
    public void OpenMenu()
    {
        _menuUi[(int)MenuType.MENU_TYPE_OPTION].gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_firstSelectedObjects[(int)MenuType.MENU_TYPE_OPTION]);
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
        _MenuActive[(int)type] = is_active;
    }

    public bool GetMenuActive(MenuType type)
    {
        return _MenuActive[(int)type];
    }

    //  設定画面開く
    public void OnSetting()
    {
        //  メニュー画面を閉じて設定画面を開く
        SetMenuActive(MenuType.MENU_TYPE_OPTION, false);
        SetMenuActive(MenuType.MENU_TYPE_SETTING, true);

        EventSystem.current.SetSelectedGameObject(_firstSelectedObjects[(int)MenuType.MENU_TYPE_SETTING]);

    }

    public void OnSettingToOption()
    {
        SetMenuActive(MenuType.MENU_TYPE_SETTING, false);
        SetMenuActive(MenuType.MENU_TYPE_OPTION, true);

        EventSystem.current.SetSelectedGameObject(_firstSelectedObjects[(int)MenuType.MENU_TYPE_OPTION]);
    }

    public void OnValumeToSetting()
    {
        SetMenuActive(MenuType.MENU_TYPE_VALUME, false);
        SetMenuActive(MenuType.MENU_TYPE_SETTING, true);

        EventSystem.current.SetSelectedGameObject(_firstSelectedObjects[(int)MenuType.MENU_TYPE_SETTING]);
    }

    public void OnCamToSetting()
    {
        SetMenuActive(MenuType.MENU_TYPE_CAM_SENSI, false);
        SetMenuActive(MenuType.MENU_TYPE_SETTING, true);

        EventSystem.current.SetSelectedGameObject(_firstSelectedObjects[(int)MenuType.MENU_TYPE_SETTING]);
    }

    public void OnOpeToOption()
    {
        SetMenuActive(MenuType.MENU_TYPE_OPERETION, false);
        SetMenuActive(MenuType.MENU_TYPE_OPTION, true);

        EventSystem.current.SetSelectedGameObject(_firstSelectedObjects[(int)MenuType.MENU_TYPE_OPTION]);
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
