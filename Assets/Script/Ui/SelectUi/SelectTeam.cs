
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
    private int RedNumber;
    private int BlueNumber;

    public Text ErrorText;
    public Image ArrowImage;
    public Image ShitaImage;
    public Text ExplanationText;

    private NewWorkInfo nw_info;

    private float curTime;

    public bool IsDebug;

    // Start is called before the first frame update
    void Start()
    {
        IsSelect = false;
        IsAllSelect = false;
        RedIsFull = false;
        BlueIsFull = false;
        TeamSelect = 0;
        TeamNumber = ArrowUI.selectNumber_;
        TeamNumber /= 2;
        RedNumber = 0;
        BlueNumber = 0;
        curTime = 0.0f;

        nw_info = GameObject.Find("NetWork").GetComponent<NewWorkInfo>();

        ErrorText.color = new Color(ErrorText.color.r, ErrorText.color.g, ErrorText.color.b, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        ////自分だけが入る
        //if (photonView.IsMine)
        //{
        if(curTime != 0.0f)
        {
            curTime -= Time.deltaTime;

            if(curTime <= 0.0f)
            {
                curTime = 0.0f;
                ErrorText.color = new Color(ErrorText.color.r, ErrorText.color.g, ErrorText.color.b, 0.0f);
            }
        }

 
        //PhotonCharaView view = null;
        //if ((int)PhotonNetwork.CurrentRoom.PlayerCount > 1)
        //{
        //    if (GameObject.Find("CharaScoreManager(Clone)").
        //               GetComponent<PhotonCharaView>() != null)
        //    {
        //        view = GameObject.Find("CharaScoreManager(Clone)").
        //                       GetComponent<PhotonCharaView>();
        //    }
        //}


        //決定したら動かさない
        //おぱしてぃーせってい
        if (IsSelect)
        {
            ArrowImage.SetOpacity(0.0f);
            ShitaImage.SetOpacity(0.0f);
            ErrorText.color = new Color(ErrorText.color.r, ErrorText.color.g, ErrorText.color.b, 0.0f);
            ExplanationText.color = new Color(ErrorText.color.r, ErrorText.color.g, ErrorText.color.b, 0.0f);

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
                    RedNumber++;

                    //if ((int)PhotonNetwork.CurrentRoom.PlayerCount > 1 &&
                    //    view != null)
                    //    view.RedNum = RedNumber;
                }
                else if (TeamSelect == 1)
                {
                    nw_info.SetTeamColor(1);
                    BlueNumber++;

                    //if ((int)PhotonNetwork.CurrentRoom.PlayerCount > 1 &&
                    //   view != null)
                    //    view.BlueNum = BlueNumber;
                }

                IsSelect = true;
                nw_info.SetInstiate(true);
            }

          

            if (TeamSelect == 0)
            {
                ArrowImage.transform.localEulerAngles = new Vector3(0,0, 180.0f);
                
            }

            if (TeamSelect == 1)
            {
                ArrowImage.transform.localEulerAngles = new Vector3(0, 0, 0.0f);
                

            }
        }



        //if ((int)PhotonNetwork.CurrentRoom.PlayerCount > 1 &&
        //               view != null)
        //{
        //    if (view.RedNum == TeamNumber)
        //    {
        //        RedIsFull = true;
        //    }

        //    if (view.BlueNum == TeamNumber)
        //    {
        //        BlueIsFull = true;
        //    }
        //}

    }
}
