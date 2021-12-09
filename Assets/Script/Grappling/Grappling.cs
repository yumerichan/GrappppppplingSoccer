using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Grappling : MonoBehaviourPunCallbacks
{
    private Transform grapplePos_;      //�O���b�v��������W
    private GameObject anchorClone_;    //�A���J�[�̃R�s�[(������΂�)
    private CharactorBasic _charaBasic;

    //[SerializeField]
    //private GameObject anchor_;     //�A���J�[(��΂���̌�)
    [SerializeField]
    private Transform shotPos_;     //�A���J�[���ˈʒu
    [SerializeField]
    private Transform cameraPos_;   //�J�����ʒu
    [SerializeField]
    private Transform playerPos_;   //�v���C���[���W
    [SerializeField]
    private Transform auxAnchorDir_;   //�A���J�[�̕���
    [SerializeField]
    private float searchRange_;         //�u���b�N�̃T�[�`�͈�

    [HideInInspector]
    public Vector3 shotDir_;        //���˕���
    [HideInInspector]
    public bool isGrappling_ { get; set; }      //�O���b�v�������ǂ���
    [HideInInspector]
    public GameObject block_ { get; set; }      //�O���b�v���Ώۃu���b�N

    //  ���˕����A�N�Z�T
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

    //  �A���J�[����
    public void ShotAnchor()
    {
        if (anchorClone_)
            return;

        if (block_ == null)
            return;

       // �u���b�N���W�ɂ߂����ăA���J�[���΂�
          grapplePos_ = Instantiate(auxAnchorDir_, block_.transform.position, Quaternion.identity) as Transform;

        //  ���˕����v�Z
        shotDir_ = (grapplePos_.transform.position - shotPos_.transform.position).normalized;
        Quaternion anchorRot_ = Quaternion.Euler(shotDir_);

        //  �A���J�[����
        anchorClone_ = PhotonNetwork.Instantiate("Anchor", block_.transform.position, anchorRot_) as GameObject;
        anchorClone_.GetComponent<Anchor>().InitAnchor(this.gameObject);
        Destroy(grapplePos_.gameObject);

        _charaBasic.state_ = CharactorBasic.CharactorStateType.STATE_TYPE_GRAPPLE;
        _charaBasic.anime_.SetInteger("AnimeState", (int)_charaBasic.state_);

        isGrappling_ = true;
    }

    //  ���C���[���w���u���b�N����
    private GameObject SearchBlock()
    {
        GameObject nier_block = null;
        float rot = 0;
        Renderer temp_ren = null;
        Canvas temp_cnv = null;
        Canvas pre_cnv = null;

        //  �f�o�b�O�p
        string name = null;

        float temp_rot; //�p�x�i�[�ϐ�

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Block"))
        {
            //  �I�u�W�F�N�g�̃e�N�X�`��
            temp_cnv = obj.transform.Find("Canvas").GetComponent<Canvas>();

            //  �J�����̃g�����X�t�H�[��
            cameraPos_ = GameObject.FindGameObjectWithTag("MainCamera").transform;

            temp_cnv.enabled = false;

            //  �����͈͊O�Ȃ��΂�
            if (Vector3.Distance(cameraPos_.position, obj.transform.position) > searchRange_)
                continue;
            

            //  �p�x�v�Z
            //  �J�����̃x�N�g���ƁA�J�������u���b�N�̃x�N�g���̂Ȃ��p���������I�u�W�F�N�g��
            //  ���e�B�N�������킹��

            Vector3 cam_vec = cameraPos_.forward;
            Vector3 block_vec = (obj.transform.position - cameraPos_.position).normalized;
            temp_rot = GetAngle(cam_vec, block_vec);

            //  �p�x���
            if (rot == 0 || rot > temp_rot)
            {
                //  2�ڈȍ~�͏��������O�̃u���b�N�̃��e�B�N�����\����
                if (nier_block != null)
                {
                    nier_block.transform.Find("Canvas").GetComponent<Canvas>().enabled = false;
                }

                //  �㏑��
                rot = temp_rot;
                nier_block = obj;
                temp_cnv.enabled = true;
            }
        }

        //  �ǂ̃u���b�N���J�����Ɏʂ��Ă��Ȃ�
        if(nier_block == null)
            return null;

        return nier_block;
    }

    //  ��̃x�N�g���̂Ȃ��p�����߂�
    float GetAngle(Vector3 vec1, Vector3 vec2)
    {
        //�@���x�N�g���̒�����0���Ɠ������o�Ȃ��̂Œ��ӁI�I�I�I

        //�x�N�g��A��B�̒������v�Z����
        float length_1 = GetVectorLength(vec1);
        float length_2 = GetVectorLength(vec2);

        //���ςƃx�N�g���������g����cos�Ƃ����߂�
        float cos_sita = GetDotProducut(vec1, vec2) / (length_1 * length_2);

        //cos�Ƃ���Ƃ����߂�
        float sita = Mathf.Acos(cos_sita);

        //�Ƃ����W�A������x�ɕϊ�
        sita = (float)(sita * 180.0 / Mathf.PI);

        return sita;
    }

    //  �x�N�g���̒������v�Z����
    float GetVectorLength(Vector3 vec)
    {
        return Mathf.Pow((vec.x * vec.x) + (vec.y * vec.y) + (vec.z * vec.z), 0.5f);
    }

    //  �x�N�g������
    float GetDotProducut(Vector3 vec1, Vector3 vec2)
    {
        return vec1.x * vec2.x + vec1.y * vec2.y + vec1.z * vec2.z;
    }
}


    
