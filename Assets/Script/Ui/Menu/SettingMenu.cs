using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviourPunCallbacks
{
    public Image _optionUi;       //�I�v�V�������
    public Image _settingUi;        //�ݒ���
    public Image _caneraSettingUi;  //�J�������x�ݒ�
    public Image _valumeSettingUi;  //���ʐݒ�
    public Image _operationUi;      //�������UI;
    public bool _isMenu;        //���j���[��ʂ��J����Ă��邩

    public Image[] _menuUis;    //���j���[�֘A��UI

    public enum MenuUiType
    {
        MENU_TYPE_OPTION,       //�I�v�V����
        MENU_TYPE_SETTING,      //�ݒ�
        MENU_TYPE_VALUME,       //����
        MENU_TYPE_CAM_SENSI,    //�J�������x
        MENU_TYPE_OPERETION,    //�������

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
                //  �Z�b�e�B���O��ʕ\��
                _optionUi.gameObject.SetActive(true);
                _isMenu = true;
            }
            else if(_isMenu == true)
            {

            }
        }
    }
}
