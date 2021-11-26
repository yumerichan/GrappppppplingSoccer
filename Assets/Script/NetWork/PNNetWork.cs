using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PNNetWork : MonoBehaviourPunCallbacks,IMatchmakingCallbacks
{
    int player_number;
    [HideInInspector]
    public string charaName_ { get; set; }
    private NewWorkInfo nw_info;

    bool isBallCreate_;
    bool IsInstiate;

    private void Start()
    {
        player_number = 0;
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
        isBallCreate_ = false;

        nw_info = GameObject.Find("NetWork").GetComponent<NewWorkInfo>();
    }

    private void Update()
    {
        

        if (!IsInstiate) return;
        if (!nw_info.GetInstiate()) return;
        if (!photonView.IsMine) return;

        string name = SelectChara.charaName_;

        //チームカラー、ナンバーで決める
        //赤
        if (nw_info.GetTeamColor() == 0)
        {
            switch(nw_info.GetTeamNumber())
            {
                case 0:
                    {
                        var position = new Vector3(0.0f, 0.0f, 0.0f);
                        PhotonNetwork.Instantiate(name, position, Quaternion.identity);
                        charaName_ = name;

                        PhotonNetwork.Instantiate("Ball", new Vector3(0, 30, 0), Quaternion.identity);

                        IsInstiate = false;
                    }
                    break;
                case 1:
                    {
                        var position = new Vector3(0.0f, 0.0f, 0.0f);
                        PhotonNetwork.Instantiate(name, position, Quaternion.identity);
                        charaName_ = name;
                        IsInstiate = false;
                    }
                    break;
                case 2:
                    {
                        var position = new Vector3(0.0f, 0.0f, 0.0f);
                        PhotonNetwork.Instantiate(name, position, Quaternion.identity);
                        charaName_ = name;
                        IsInstiate = false;
                    }
                    break;
                case 3:
                    {
                        var position = new Vector3(0.0f, 0.0f, 0.0f);
                        PhotonNetwork.Instantiate(name, position, Quaternion.identity);
                        charaName_ = name;
                        IsInstiate = false;
                    }
                    break;

            }
        }
        //赤
        if (nw_info.GetTeamColor() == 1)
        {
            switch (nw_info.GetTeamNumber())
            {
                case 0:
                    {
                var position = new Vector3(0.0f, 0.0f, 0.0f);
                PhotonNetwork.Instantiate(name, position, Quaternion.identity);
                charaName_ = name;
                IsInstiate = false;
            }
            break;
                case 1:
                    {
                        var position = new Vector3(0.0f, 0.0f, 0.0f);
                        PhotonNetwork.Instantiate(name, position, Quaternion.identity);
                        charaName_ = name;
                        IsInstiate = false;
                    }
                    break;
                case 2:
                    {
                var position = new Vector3(0.0f, 0.0f, 0.0f);
                PhotonNetwork.Instantiate(name, position, Quaternion.identity);
                charaName_ = name;
                IsInstiate = false;
            }
            break;
                case 3:
                    {
                var position = new Vector3(0.0f, 0.0f, 0.0f);
                PhotonNetwork.Instantiate(name, position, Quaternion.identity);
                charaName_ = name;
                IsInstiate = false;
            }
            break;
        }
        }

        // ルームが満員になったら、以降そのルームへの参加を不許可にする
        if (PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            nw_info.SetInstiate(false);
        }
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {

        //// ランダムなルームに参加する

        // ランダムなルームに参加する
        //PhotonNetwork.JoinRandomRoom();

        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)ArrowUI.selectNumber_;
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("room", roomOptions, TypedLobby.Default);
    }

    // ランダムで参加できるルームが存在しないなら、新規でルームを作成する
    void IMatchmakingCallbacks.OnCreatedRoom()
    {
        // ルームの参加人数を設定する

        //PhotonNetwork.Instantiate("Ball", new Vector3(0, 30, 0), Quaternion.identity);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        IsInstiate = true;

        //デバッグ用

        if ((int)PhotonNetwork.CurrentRoom.MaxPlayers == 0)
        {
            string name = SelectChara.charaName_;
            switch ((int)PhotonNetwork.CurrentRoom.PlayerCount)
            {
                //順番に入れる
                case 1:
                    {
                        var position = new Vector3(0.0f, 0.0f, 0.0f);
                        PhotonNetwork.Instantiate("Chara_Trap", position, Quaternion.identity);
                        player_number++;

                        PhotonNetwork.Instantiate("Ball", new Vector3(0, 30, 0), Quaternion.identity);
                    }
                    break;
                case 2:
                    {
                        var position = new Vector3(0.0f, 0.0f, 0.0f);
                        PhotonNetwork.Instantiate("Chara_Trap", position, Quaternion.identity);

                        charaName_ = name;



                    }
                    break;
                case 3:
                    {
                        var position = new Vector3(0.0f, 0.0f, 0.0f);
                        PhotonNetwork.Instantiate(name, position, Quaternion.identity);
                        player_number++;
                        charaName_ = name;



                    }
                    break;
                case 4:
                    {
                        var position = new Vector3(0.0f, 0.0f, 0.0f);
                        PhotonNetwork.Instantiate(name, position, Quaternion.identity);
                        player_number++;
                        charaName_ = name;



                    }
                    break;
                case 5:
                    {
                        var position = new Vector3(0.0f, 0.0f, 0.0f);
                        PhotonNetwork.Instantiate(name, position, Quaternion.identity);
                        player_number++;
                        charaName_ = name;



                    }
                    break;
                case 6:
                    {
                        var position = new Vector3(0.0f, 0.0f, 0.0f);
                        PhotonNetwork.Instantiate(name, position, Quaternion.identity);
                        player_number++;
                        charaName_ = name;



                    }
                    break;
                case 7:
                    {
                        var position = new Vector3(0.0f, 0.0f, 0.0f);
                        PhotonNetwork.Instantiate(name, position, Quaternion.identity);
                        player_number++;
                        charaName_ = name;



                    }
                    break;
            }



            // ルームが満員になったら、以降そのルームへの参加を不許可にする
            if (PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }

        }
    }
}
