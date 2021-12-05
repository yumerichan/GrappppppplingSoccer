using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviourPunCallbacks
{
    //  赤チームのプレイヤーリスト
    public List<GameObject> _redPlayerList { get; set; } = new List<GameObject>();

    //  青チームのプレイヤーリスト
    public List<GameObject> _bluePlayerList { get; set; } = new List<GameObject>();

    //  赤チーム初期座標
    private Vector3[] _redInitPos = new [] { 
        new Vector3(0, 0 ,0),
        new Vector3(0, 0 ,0),
        new Vector3(0, 0 ,0),
        new Vector3(0, 0 ,0),
    };

    //  青チーム初期座標
    private Vector3[] _blueInitPos = new[] {
        new Vector3(0, 0 ,0),
        new Vector3(0, 0 ,0),
        new Vector3(0, 0 ,0),
        new Vector3(0, 0 ,0),
    };

    //  ボールのゲームオブジェクト
    public GameObject _ball;

    //全体で使う
    static public bool m_IsStart;
    private bool m_IsEnd;

    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 60;

        m_IsStart = false;
        m_IsEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        ////開始時止める
        //if(m_IsStart == false)
        //    StartCoroutine("StartStop");

        ////終了処理
        //if(m_IsEnd == true)
        //     StartCoroutine("EndStop");

        //シーンで行いたいこと
    }

    //  ゲームスタート
    public void RestartGame()
    {
        //  初期位置設定

        int count = 0;

        //  赤チーム位置リセット
        foreach (GameObject player in _redPlayerList)
        {
            player.transform.position = _redInitPos[count];
            count++;
        }

        count = 0;

        //  青チーム
        foreach (GameObject player in _bluePlayerList)
        {
            player.transform.position = _blueInitPos[count];
            count++;
        }

        //  ボール
        _ball.transform.position = new Vector3(0, 80, 0);

        //  スタートの演出 ==============
        //  出来たらここでリクエストする

    }

    //  赤チーム追加
    public void AddRedPlayer(GameObject player)
    {
        _redPlayerList.Add(player);
    }

    //  青チーム追加
    public void AddBluePlayer(GameObject player)
    {
        _bluePlayerList.Add(player);
    }

    /*
      
      ここいったん処理から外しときます




      //開始処理
      //最初は止めたいためここでフラグを折る
      IEnumerator StartStop()
        {
            yield return new WaitForSeconds(0);  //待つ

            //待ってる間にフェードしたいな

            if (PhotonNetwork.CurrentRoom.IsOpen == false)
            {
                //開始処理

                yield return new WaitForSeconds(3);  //3秒待つ

                m_IsStart = true;
            }
        }

        //終了処理
        //勝ち負け判定できるよ
        IEnumerator EndStop()
        {
            yield return new WaitForSeconds(0);  //待つ

            //待ってる間にフェードしたいな

            //フェードが終わったフラグで行けそう
            SceneManager.LoadScene("result");
        }

    */
}
