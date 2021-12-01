using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviourPunCallbacks
{
    //  初期位置

    //  赤チームの初期位置
    public Vector3[] _redTiemInitPos = new[] {
       new Vector3 ( 0, 0, 0 ), //一番
       new Vector3 ( 0, 0, 0 ), //二番
       new Vector3 ( 0, 0, 0 ), //三番
       new Vector3 ( 0, 0, 0 ), //四番
    };

    //  青チームの初期位置
    public Vector3[] _buleTiemInitPos = new[] {
       new Vector3 ( 0, 0, 0 ), //一番
       new Vector3 ( 0, 0, 0 ), //二番
       new Vector3 ( 0, 0, 0 ), //三番
       new Vector3 ( 0, 0, 0 ), //四番
    };

    //全体で使う
    static public bool m_IsStart;
    private bool m_IsEnd;

    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 60;

        m_IsStart = false;
        m_IsEnd = false;

        if (GameObject.FindGameObjectWithTag("Ball") == null)
            PhotonNetwork.Instantiate("Ball", new Vector3(0, 30, 0), Quaternion.identity);
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
