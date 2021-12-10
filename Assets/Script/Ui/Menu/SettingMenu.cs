using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviourPunCallbacks
{
    public bool _isMenu { get; set; }        //���j���[��ʂ��J����Ă��邩

    public Image[] _menuUis { get; set; }    //���j���[�֘A��UI

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
            //  ���j���[���J����Ă��Ȃ�
            if(_isMenu == false)
            {
                //  �Z�b�e�B���O��ʕ\��
                _menuUis[(int)MenuUiType.MENU_TYPE_OPTION].gameObject.SetActive(true);
                _isMenu = true;
            }

            //  ��������̃��j���[���J����Ă���ꍇ
            else if(_isMenu == true)
            {
                DeleteUi();
            }
        }
    }

    private void DeleteUi()
    {
        //  ���ׂẴ��j���[���A�N�e�B�u�ɂ��āA���j���[�t���O��܂�
        foreach (Image image in _menuUis)
        {
            image.gameObject.SetActive(false);
            _isMenu = false;
        }
    }

    //  Ui�A�N�e�B�u�Z�b�g
    public void SetUi(MenuUiType type, bool active)
    {
        _menuUis[(int)type].gameObject.SetActive(active);
    }
}
