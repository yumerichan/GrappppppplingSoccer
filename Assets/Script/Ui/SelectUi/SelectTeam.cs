
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SelectTeam : MonoBehaviourPunCallbacks
{
    private bool IsAllSelect;
    private bool IsSelect;

    private bool RedIsFull;
    private bool BlueIsFull;
    private int TeamSelect;
    public int GetTeamSelect()
    {
        return TeamSelect;
    }

    private int TeamNumber;

    public Text ErrorText;
    public Image ArrowImage;
    public Image ShitaImage;
    public Text ExplanationText;
    public Text RedNumText;
    public Text BlueNumText;
    public Image RedNumImage;
    public Image BlueNumImage;

    private bool _isDecide;
    public bool GetIsDecide() { return _isDecide; }

    private NewWorkInfo nw_info;

    private float curTime;

    public bool IsDebug;

    // Start is called before the first frame update
    void Start()
    {
        IsSelect = false;
        IsAllSelect = false;
        RedIsFull = false;
        _isDecide = false;
        BlueIsFull = false;
        TeamSelect = 0;
        TeamNumber = ArrowUI.selectNumber_;
        TeamNumber /= 2;
        curTime = 0.0f;

        nw_info = GameObject.Find("NetWork").GetComponent<NewWorkInfo>();

       

        ErrorText.color = new Color(ErrorText.color.r, ErrorText.color.g, ErrorText.color.b, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        ////��������������
        //if (photonView.IsMine)
        //{
        if (curTime != 0.0f)
        {
            curTime -= Time.deltaTime;

            if (curTime <= 0.0f)
            {
                curTime = 0.0f;
                ErrorText.color = new Color(ErrorText.color.r, ErrorText.color.g, ErrorText.color.b, 0.0f);
            }
        }

        //���肵���瓮�����Ȃ�
        //���ς��Ă��[�����Ă�
        if (IsSelect)
        {
            //�S�������肵����
            if (IsAllSelect || IsDebug)
            {
                ArrowImage.SetOpacity(0.0f);
                ShitaImage.SetOpacity(0.0f);
                RedNumImage.SetOpacity(0.0f);
                BlueNumImage.SetOpacity(0.0f);
                RedNumText.color = new Color(RedNumText.color.r, RedNumText.color.g, RedNumText.color.b, 0.0f);
                BlueNumText.color = new Color(BlueNumText.color.r, BlueNumText.color.g, BlueNumText.color.b, 0.0f);
                ErrorText.color = new Color(ErrorText.color.r, ErrorText.color.g, ErrorText.color.b, 0.0f);
                ExplanationText.color = new Color(ErrorText.color.r, ErrorText.color.g, ErrorText.color.b, 0.0f);
            }
        }
        else
        {
            bool is_right = false;
            bool is_left = false;

            //  �p�b�h�̏\���{�^���擾(���̐��F�E�@���̐��F��)
            float input_button;
            input_button = Input.GetAxis("Horizontal");

            //  �p�b�h�̃X�e�B�b�N�l�擾
            float input_stick = Input.GetAxis("Horizontal");

            if (input_button > 0 || input_stick > 0)
                is_right = true;

            if (input_button < 0 || input_stick < 0)
                is_left = true;

            if (Input.GetKeyDown(KeyCode.LeftArrow) || is_left)
            {
                TeamSelect = 0;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || is_right)
            {
                TeamSelect = 1;
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Enter"))
            {
                IsSelect = IsDebug;


                //������������G���[�I�p�V�e�B
                if (TeamSelect == 0 && RedIsFull)
                {
                    ErrorText.color = new Color(ErrorText.color.r, ErrorText.color.g, ErrorText.color.b, 100.0f);
                    curTime = 2.0f;
                    return;
                }
                else if (TeamSelect == 1 && BlueIsFull)
                {
                    ErrorText.color = new Color(ErrorText.color.r, ErrorText.color.g, ErrorText.color.b, 100.0f);
                    curTime = 2.0f;
                    return;
                }

                if (TeamSelect == 0)
                {
                    nw_info.SetTeamColor(0);
                }
                else if (TeamSelect == 1)
                {
                    nw_info.SetTeamColor(1);
                }

                _isDecide = true;
                  IsSelect = true;
                nw_info.SetInstiate(true);
            }



            if (TeamSelect == 0)
            {
                ArrowImage.transform.localEulerAngles = new Vector3(0, 0, 180.0f);

            }

            if (TeamSelect == 1)
            {
                ArrowImage.transform.localEulerAngles = new Vector3(0, 0, 0.0f);

            }
        }

        PhotonCharaView view = GameObject.Find("CharaViewManager(Clone)").
                    GetComponent<PhotonCharaView>();

        RedNumText.text = view.RedNum.ToString();
        BlueNumText.text = view.BlueNum.ToString();

        if (view.RedNum == TeamNumber)
        {
            RedIsFull = true;
        }

        if (view.BlueNum == TeamNumber)
        {
            BlueIsFull = true;
        }
        

        //�S�������߂�
        if(RedIsFull && BlueIsFull)
        {
            IsAllSelect = true;
        }
    }
}
