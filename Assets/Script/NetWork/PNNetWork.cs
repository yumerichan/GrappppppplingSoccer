using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PNNetWork : MonoBehaviourPunCallbacks,IMatchmakingCallbacks
{
    int player_number;
    [HideInInspector]
    public string charaName_ { get; set; }
    private NewWorkInfo nw_info;

    public string _roomName;

    bool IsInstiate;

    private void Start()
    {

        player_number = 0;
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
        IsInstiate = false;

        nw_info = GameObject.Find("NetWork").GetComponent<NewWorkInfo>();
    }

    private void Update()
    {
        //Debug.Log(nw_info.GetTeamNumber());


        if (!IsInstiate) return;

        Debug.Log("Isins");

        if (!nw_info.GetInstiate()) return;

        Debug.Log("netins");

        //if (!photonView.IsMine) return;

        //Debug.Log("ismine");

        string name = SelectChara.charaName_;

        //�`�[���J���[�A�i���o�[�Ō��߂�
        //��
        if (nw_info.GetTeamColor() == 0)
        {
            Vector3 vec = GameObject.Find("RedStart").transform.position;

            var position = new Vector3(vec.x, vec.y, vec.z);

            if (GameObject.Find("Red1").GetComponent<RedSrart>().GetOnRedCollision())
            {
                position = new Vector3(vec.x + 40.0f, vec.y, vec.z );
            }

            if(GameObject.Find("Red2").GetComponent<RedSrart2>().GetOnRedCollision())
            {
                position = new Vector3(vec.x - 40.0f, vec.y, vec.z);
            }

            if (GameObject.Find("Red3").GetComponent<RedSrart3>().GetOnRedCollision())
            {
                position = new Vector3(vec.x - 80.0f, vec.y, vec.z);
            }

            Quaternion rot = new Quaternion(0.0f,180.0f,0.0f,0.0f);

            //_gPlayerList[(int)PhotonNetwork.CurrentRoom.PlayerCount - 1] = PhotonNetwork.Instantiate(name, position, rot);
            //_playScene.GetComponent<PlayScene>().AddRedPlayer(_gPlayerList[(int)PhotonNetwork.CurrentRoom.PlayerCount - 1]);

           GameObject aa = PhotonNetwork.Instantiate(name, position, rot,0);
           GameObject.FindGameObjectWithTag("playManager").GetComponent<PlayScene>().AddRedPlayer(aa);

            aa.transform.GetComponent<CharaScore>()._charaNumber = (int)PhotonNetwork.CurrentRoom.PlayerCount - 1;
            aa.transform.GetComponent<CharactorBasic>()._teamColor = nw_info.GetTeamColor();

            charaName_ = name;
            IsInstiate = false;
        }
        //��
        if (nw_info.GetTeamColor() == 1)
        {
            Vector3 vec = GameObject.Find("BlueStart").transform.position;

           
            BlueStart blue = GameObject.Find("Blue1").GetComponent<BlueStart>();
            BlueStart2 blue2 = GameObject.Find("Blue2").GetComponent<BlueStart2>();
            BlueStart3 blue3 = GameObject.Find("Blue3").GetComponent<BlueStart3>();

            var position = new Vector3(vec.x, vec.y, vec.z);

            if (blue.GetOnRedCollision())
            {
                position = new Vector3(vec.x + 40.0f, vec.y, vec.z);
            }

            if (blue2.GetOnRedCollision())
            {
                position = new Vector3(vec.x - 40.0f, vec.y, vec.z);
            }

            if (blue3.GetOnRedCollision())
            {
                position = new Vector3(vec.x - 80.0f, vec.y, vec.z);
            }


            //_gPlayerList[(int)PhotonNetwork.CurrentRoom.PlayerCount - 1] = PhotonNetwork.Instantiate(name, position, Quaternion.identity);
            //_playScene.GetComponent<PlayScene>().AddBluePlayer(_gPlayerList[(int)PhotonNetwork.CurrentRoom.PlayerCount - 1]);



            GameObject aa = PhotonNetwork.Instantiate(name, position, Quaternion.identity,0);
            GameObject.FindGameObjectWithTag("playManager").GetComponent<PlayScene>().AddBluePlayer(aa);

            aa.transform.GetComponent<CharaScore>()._charaNumber = (int)PhotonNetwork.CurrentRoom.PlayerCount - 1;
            aa.transform.GetComponent<CharactorBasic>()._teamColor = nw_info.GetTeamColor();

            charaName_ = name;
            IsInstiate = false;

        }

        if ((int)PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.Instantiate("Ball", new Vector3(0, 30, 0), Quaternion.identity);
            PhotonNetwork.Instantiate("CharaViewManager", new Vector3(0, 30, 0), Quaternion.identity);
            PhotonNetwork.Instantiate("GoalCanvas", new Vector3(0, 0, 0), Quaternion.identity);
        }

        // ���[���������ɂȂ�����A�ȍ~���̃��[���ւ̎Q����s���ɂ���
        if (PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            nw_info.SetInstiate(false);
        }
    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {

        //// �����_���ȃ��[���ɎQ������

        // �����_���ȃ��[���ɎQ������
        //PhotonNetwork.JoinRandomRoom();

        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)ArrowUI.selectNumber_;
        // "Room"�Ƃ������O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���ĎQ������j
        PhotonNetwork.JoinOrCreateRoom("rooom", roomOptions, TypedLobby.Default);

        
    }

    // �����_���ŎQ���ł��郋�[�������݂��Ȃ��Ȃ�A�V�K�Ń��[�����쐬����
    void IMatchmakingCallbacks.OnCreatedRoom()
    {
        // ���[���̎Q���l����ݒ肷��

        //PhotonNetwork.Instantiate("Ball", new Vector3(0, 30, 0), Quaternion.identity);

        
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        if ((int)PhotonNetwork.CurrentRoom.PlayerCount == 1)
            PhotonNetwork.Instantiate("PlaySceneManager", new Vector3(0, 0, 0), Quaternion.identity);

        IsInstiate = true;

        //�f�o�b�O�p

        if ((int)PhotonNetwork.CurrentRoom.MaxPlayers == 0)
        {
            string name = SelectChara.charaName_;
            switch ((int)PhotonNetwork.CurrentRoom.PlayerCount)
            {
                //���Ԃɓ����
                case 1:
                    {
                        var position = new Vector3(0.0f, 0.0f, 0.0f);
                        PhotonNetwork.Instantiate(name, position, Quaternion.identity);
                        player_number++;

                        PhotonNetwork.Instantiate("Ball", new Vector3(0, 30, 0), Quaternion.identity);
                    }
                    break;
                case 2:
                    {
                        var position = new Vector3(0.0f, 0.0f, 0.0f);
                        PhotonNetwork.Instantiate(name, position, Quaternion.identity);

                        charaName_ = name;
                    }
                    break;
                case 3:
                    {
                        var position = new Vector3(0.0f, 0.0f, 0.0f);
                        PhotonNetwork.Instantiate(name, position, Quaternion.identity);
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



            // ���[���������ɂȂ�����A�ȍ~���̃��[���ւ̎Q����s���ɂ���
            if (PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }

        }
    }
}
