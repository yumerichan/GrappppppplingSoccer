
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SelectTeam : MonoBehaviour
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
   
    public Image RedImage;
    public Image BlueImage;
    public Text RedNumText;
    public Text BlueNumText;
    public Image RedArrowImage;
    public Image BlueArrowImage;
    public Text TeamSelectUnderText;
    public Text TeamSelectTopText;
    public Text RedText;
    public Text BlueText;
    public Text RedNumDispText;
    public Text BlueNumDispText;
    public Text RedMaxNumText;
    public Text BlueMaxNumText;
    public Text RedSlashText;
    public Text BlueSlashText;
    public Text ErrorText;
    public Image TeamSelectImage;
    

    private bool _isDecide;
    public bool GetIsDecide() { return _isDecide; }

    private NewWorkInfo nw_info;

    private float curTime;

    public bool IsDebug;

    private float _selectCnt;

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
        RedMaxNumText.text = TeamNumber.ToString();
        BlueMaxNumText.text = TeamNumber.ToString();

        curTime = 0.0f;
        _selectCnt = 1f;
        BlueArrowImage.SetOpacity(0.0f);

        nw_info = GameObject.Find("NetWork").GetComponent<NewWorkInfo>();

        ErrorText.color = new Color(ErrorText.color.r, ErrorText.color.g, ErrorText.color.b, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        ////自分だけが入る
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

        if ((int)PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {

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
        }

        //決定したら動かさない
        //おぱしてぃーせってい
        if (IsSelect)
        {

            //全員が決定したら
            if (IsAllSelect || IsDebug)
            {
                RedImage.SetOpacity(0.0f);
                BlueImage.SetOpacity(0.0f);
                RedNumText.color = new Color(RedNumText.color.r, RedNumText.color.g, RedNumText.color.b, 0.0f);
                BlueNumText.color = new Color(BlueNumText.color.r, BlueNumText.color.g, BlueNumText.color.b, 0.0f);
                RedArrowImage.SetOpacity(0.0f);
                BlueArrowImage.SetOpacity(0.0f);
                TeamSelectUnderText.color = new Color(TeamSelectUnderText.color.r, TeamSelectUnderText.color.g, TeamSelectUnderText.color.b, 0.0f);
                TeamSelectTopText.color = new Color(TeamSelectTopText.color.r, TeamSelectTopText.color.g, TeamSelectTopText.color.b, 0.0f);
                RedText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                BlueText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                RedNumDispText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                BlueNumDispText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                RedSlashText.color= new Color(0.0f, 0.0f, 0.0f, 0.0f);
                BlueSlashText.color= new Color(0.0f, 0.0f, 0.0f, 0.0f);
                ErrorText.color = new Color(ErrorText.color.r, ErrorText.color.g, ErrorText.color.b, 0.0f);
                TeamSelectImage.SetOpacity(0.0f);
            }
        }
        else
        {
            bool is_right = false;
            bool is_left = false;

            //  パッドの十字ボタン取得(正の数：右　負の数：左)
            float input_button;
            input_button = Input.GetAxis("Horizontal");

            //  パッドのスティック値取得
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
                

                if ((int)PhotonNetwork.CurrentRoom.PlayerCount > 1)
                {
                    if (GameObject.Find("CharaViewManager(Clone)").
                                GetComponent<PhotonCharaView>() != null)
                    {
                        _selectCnt -= Time.deltaTime;

                        if(_selectCnt < 0f)
                            SelectingTeam();
                    }
                }
                else;
                {
                    SelectingTeam();
                }
            }

            if (TeamSelect == 0)
            {
                RedArrowImage.SetOpacity(1.0f);
                BlueArrowImage.SetOpacity(0.0f);
            }

            if (TeamSelect == 1)
            {
                BlueArrowImage.SetOpacity(1.0f);
                RedArrowImage.SetOpacity(0.0f);
            }
        }

        //全員が決めた
        if (RedIsFull && BlueIsFull)
        {
            IsAllSelect = true;
        }
    }

    private void SelectingTeam()
    {
        IsSelect = IsDebug;

        //満員だったらエラーオパシティ
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
}
