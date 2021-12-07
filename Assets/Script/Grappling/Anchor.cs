using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Anchor : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private float shotSpeed_;          //���C���[�̎ˏo�X�s�[�h
    [SerializeField]
    private float wireMaxLong_;        //���C���[�̒����̌��E�l
    [SerializeField]
    private float wireMinLong_;        //���C���[�̒����̍Œ�l 
    [SerializeField]
    private float wireSpringPower_;    //���C���[�̂΂˂̗�
    [SerializeField]
    private float wireWeight_;         //���C���[�̏d��
    [SerializeField]
    private int wirePullSpeed_;        //���C���[����J�鑬��

    private GameObject player_;         //�v���C���[
    private Rigidbody rigitBody_;      //���W�b�g�{�f�B
    private SpringJoint springJoint_;  //�X�v�����O�W���C���g
    private Vector3 move_;              //�ړ���
    private float disPlayer_;          //�v���C���[�Ƃ̋���
    private bool isHit_;               //�A���J�[�Փ˃t���O


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
    
    //  �A���J�[�����蔻��
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Block")
        {
            isHit_ = true;
            player_.GetComponent<CharactorBasic>().state_ = CharactorBasic.CharactorStateType.STATE_TYPE_GRAPPLE;
        }
    }

    //  �A���J�[�ړ�����
    private void MoveAnchor()
    {
        springJoint_.connectedBody = rigitBody_;
        springJoint_.spring = wireSpringPower_;
        springJoint_.damper = wireWeight_;

        if (disPlayer_ > wireMaxLong_)
            player_.GetComponent<Grappling>().isGrappling_ = false;
    }

    //  �A���J�[����J��
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
