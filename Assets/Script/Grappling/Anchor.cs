using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Anchor : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private float shotSpeed_;          //ワイヤーの射出スピード
    [SerializeField]
    private float wireMaxLong_;        //ワイヤーの長さの限界値
    [SerializeField]
    private float wireMinLong_;        //ワイヤーの長さの最低値 
    [SerializeField]
    private float wireSpringPower_;    //ワイヤーのばねの力
    [SerializeField]
    private float wireWeight_;         //ワイヤーの重さ
    [SerializeField]
    private int wirePullSpeed_;        //ワイヤーを手繰る速さ

    private GameObject player_;         //プレイヤー
    private Rigidbody rigitBody_;      //リジットボディ
    private SpringJoint springJoint_;  //スプリングジョイント
    private Vector3 move_;              //移動量
    private float disPlayer_;          //プレイヤーとの距離
    private bool isHit_;               //アンカー衝突フラグ


    // Start is called before the first frame update
    void Start()
    {
        player_ = GameObject.FindGameObjectWithTag("Player");
        springJoint_ = player_.GetComponent<SpringJoint>();
        rigitBody_ = GetComponent<Rigidbody>();

        isHit_ = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        disPlayer_ = Vector3.Distance(transform.position, player_.transform.position);

        if (player_.GetComponent<Grappling>().isGrappling_)
            MoveAnchor();
        else
            PullAnchor();

        Transform shot_pos = player_.transform.Find("ShotPos");

        //  LineRenderer
        Vector3 pos = shot_pos.position;

        GetComponent<LineRenderer>().SetPosition(0, pos);
        GetComponent<LineRenderer>().SetPosition(1, transform.position);

    }
    
    //  アンカー当たり判定
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Block")
        {
            isHit_ = true;
            player_.GetComponent<CharactorBasic>().state_ = CharactorBasic.CharactorStateType.STATE_TYPE_GRAPPLE;
        }
    }

    //  アンカー移動処理
    private void MoveAnchor()
    {
        springJoint_.connectedBody = rigitBody_;
        springJoint_.spring = wireSpringPower_;
        springJoint_.damper = wireWeight_;

        if (disPlayer_ > wireMaxLong_)
            player_.GetComponent<Grappling>().isGrappling_ = false;
    }

    //  アンカーを手繰る
    private void PullAnchor()
    {
        transform.position = Vector3.MoveTowards(transform.position, player_.transform.position,
            wirePullSpeed_ * Time.deltaTime);

        if (disPlayer_ <= 2)
            Destroy(gameObject);
    }

    public void InitAnchor(GameObject player)
    {
        player_ = player;
        springJoint_ = player_.GetComponent<SpringJoint>();
    }
}
