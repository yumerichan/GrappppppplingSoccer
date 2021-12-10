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

    //  ���j���[��ʃ^�C�v
    public enum MenuType
    {
        MENU_TYPE_OPTION,       //�I�v�V����
        MENU_TYPE_SETTING,      //�ݒ�
        MENU_TYPE_CAM_SENSI,    //�J�������x
        MENU_TYPE_VALUME,       //����
        MENU_TYPE_OPERETION,    //����ݒ�

        MENU_TYPE_NUM           //�v�f��
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
        //  �X�^�[�g�{�^���ŊJ��
        if (Input.GetButtonDown("Option"))
        {
            //  �J���Ă��Ȃ������烁�j���[�J��
            if (!_isOpenMenu)
            {
                OpenMenu();
            }
            //  �J����Ă��������
            else if(_isOpenMenu)
            {
                DeleteMenu();
            }
        }

    }

    //  ���j���[��ʊJ��
    public void OpenMenu()
    {
        _menuUi[(int)MenuType.MENU_TYPE_OPTION].gameObject.SetActive(true);
        _isOpenMenu = true;
    }

    //  ���j���[��ʍ폜
    public void DeleteMenu()
    {
        foreach (Image image in _menuUi)
        {
            image.gameObject.SetActive(false);
            _isOpenMenu = false;
        }
    }

    //  �A�N�e�B�u�Z�b�g
    public void SetMenuActive(MenuType type, bool is_active)
    {
        _menuUi[(int)type].gameObject.SetActive(is_active);
    }

    //  �ݒ��ʊJ��
    public void OnSetting()
    {
        //  ���j���[��ʂ���Đݒ��ʂ��J��
        SetMenuActive(MenuType.MENU_TYPE_OPTION, false);
        SetMenuActive(MenuType.MENU_TYPE_SETTING, true);

        EventSystem.current.SetSelectedGameObject(_firstSelectedObjects[(int)MenuType.MENU_TYPE_SETTING]);

    }

    //  ����
    public void OnValume()
    {
        //  �ݒ��ʂ���ĉ��ʐݒ��ʂ��J��
        SetMenuActive(MenuType.MENU_TYPE_SETTING, false);
        SetMenuActive(MenuType.MENU_TYPE_VALUME, true);

        EventSystem.current.SetSelectedGameObject(_firstSelectedObjects[(int)MenuType.MENU_TYPE_VALUME]);
    }

    //  �J�������x
    public void OnCamSensi()
    {
        //  �ݒ��ʂ���ăJ�����ݒ��ʂ��J��
        SetMenuActive(MenuType.MENU_TYPE_SETTING, false);
        SetMenuActive(MenuType.MENU_TYPE_CAM_SENSI, true);
        EventSystem.current.SetSelectedGameObject(_firstSelectedObjects[(int)MenuType.MENU_TYPE_CAM_SENSI]);

    }

    //  ���������ʊJ��
    public void OnOperetion()
    {
        //  ���j���[��ʂ���Đݒ��ʂ��J��
        SetMenuActive(MenuType.MENU_TYPE_OPTION, false);
        SetMenuActive(MenuType.MENU_TYPE_OPERETION, true);

        EventSystem.current.SetSelectedGameObject(_firstSelectedObjects[(int)MenuType.MENU_TYPE_OPERETION]);
    }
}
