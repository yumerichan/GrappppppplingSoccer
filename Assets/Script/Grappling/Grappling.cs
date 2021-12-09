using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Grappling : MonoBehaviourPunCallbacks
{
    private Transform grapplePos_;      //グラップルする座標
    private GameObject anchorClone_;    //アンカーのコピー(これを飛ばす)
    private CharactorBasic _charaBasic;

    //[SerializeField]
    //private GameObject anchor_;     //アンカー(飛ばすやつの元)
    [SerializeField]
    private Transform shotPos_;     //アンカー発射位置
    [SerializeField]
    private Transform cameraPos_;   //カメラ位置
    [SerializeField]
    private Transform playerPos_;   //プレイヤー座標
    [SerializeField]
    private Transform auxAnchorDir_;   //アンカーの方向
    [SerializeField]
    private float searchRange_;         //ブロックのサーチ範囲

    [HideInInspector]
    public Vector3 shotDir_;        //発射方向
    [HideInInspector]
    public bool isGrappling_ { get; set; }      //グラップル中かどうか
    [HideInInspector]
    public GameObject block_ { get; set; }      //グラップル対象ブロック

    //  発射方向アクセサ
    public Vector3 GetShotDir() { return shotDir_; }

    // Start is called before the first frame update
    void Start()
    {
        block_ = null;
        isGrappling_ = false;
        _charaBasic = GetComponent<CharactorBasic>();
    }

    // Update is called once per frame
    void Update()
    {
        block_ = SearchBlock();
    }

    //  アンカー発射
    public void ShotAnchor()
    {
        if (anchorClone_)
            return;

        if (block_ == null)
            return;

       // ブロック座標にめがけてアンカーを飛ばす
          grapplePos_ = Instantiate(auxAnchorDir_, block_.transform.position, Quaternion.identity) as Transform;

        //  発射方向計算
        shotDir_ = (grapplePos_.transform.position - shotPos_.transform.position).normalized;
        Quaternion anchorRot_ = Quaternion.Euler(shotDir_);

        //  アンカー生成
        anchorClone_ = PhotonNetwork.Instantiate("Anchor", block_.transform.position, anchorRot_) as GameObject;
        anchorClone_.GetComponent<Anchor>().InitAnchor(this.gameObject);
        Destroy(grapplePos_.gameObject);

        _charaBasic.state_ = CharactorBasic.CharactorStateType.STATE_TYPE_GRAPPLE;
        _charaBasic.anime_.SetInteger("AnimeState", (int)_charaBasic.state_);

        isGrappling_ = true;
    }

    //  ワイヤーを指すブロック検索
    private GameObject SearchBlock()
    {
        GameObject nier_block = null;
        float rot = 0;
        Renderer temp_ren = null;
        Canvas temp_cnv = null;
        Canvas pre_cnv = null;

        //  デバッグ用
        string name = null;

        float temp_rot; //角度格納変数

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Block"))
        {
            //  オブジェクトのテクスチャ
            temp_cnv = obj.transform.Find("Canvas").GetComponent<Canvas>();

            //  カメラのトランスフォーム
            cameraPos_ = GameObject.FindGameObjectWithTag("MainCamera").transform;

            temp_cnv.enabled = false;

            //  検索範囲外なら飛ばす
            if (Vector3.Distance(cameraPos_.position, obj.transform.position) > searchRange_)
                continue;
            

            //  角度計算
            //  カメラのベクトルと、カメラ→ブロックのベクトルのなす角が小さいオブジェクトに
            //  レティクルを合わせる

            Vector3 cam_vec = cameraPos_.forward;
            Vector3 block_vec = (obj.transform.position - cameraPos_.position).normalized;
            temp_rot = GetAngle(cam_vec, block_vec);

            //  角度比べ
            if (rot == 0 || rot > temp_rot)
            {
                //  2個目以降は書き換え前のブロックのレティクルを非表示に
                if (nier_block != null)
                {
                    nier_block.transform.Find("Canvas").GetComponent<Canvas>().enabled = false;
                }

                //  上書き
                rot = temp_rot;
                nier_block = obj;
                temp_cnv.enabled = true;
            }
        }

        //  どのブロックもカメラに写っていない
        if(nier_block == null)
            return null;

        return nier_block;
    }

    //  二つのベクトルのなす角を求める
    float GetAngle(Vector3 vec1, Vector3 vec2)
    {
        //　※ベクトルの長さが0だと答えが出ないので注意！！！！

        //ベクトルAとBの長さを計算する
        float length_1 = GetVectorLength(vec1);
        float length_2 = GetVectorLength(vec2);

        //内積とベクトル長さを使ってcosθを求める
        float cos_sita = GetDotProducut(vec1, vec2) / (length_1 * length_2);

        //cosθからθを求める
        float sita = Mathf.Acos(cos_sita);

        //θをラジアンから度に変換
        sita = (float)(sita * 180.0 / Mathf.PI);

        return sita;
    }

    //  ベクトルの長さを計算する
    float GetVectorLength(Vector3 vec)
    {
        return Mathf.Pow((vec.x * vec.x) + (vec.y * vec.y) + (vec.z * vec.z), 0.5f);
    }

    //  ベクトル内積
    float GetDotProducut(Vector3 vec1, Vector3 vec2)
    {
        return vec1.x * vec2.x + vec1.y * vec2.y + vec1.z * vec2.z;
    }
}


    
