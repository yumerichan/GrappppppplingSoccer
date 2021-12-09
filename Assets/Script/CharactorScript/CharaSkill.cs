using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharaSkill : MonoBehaviour
{
    public GameObject _ball;
    public GameObject _wallEffect;
    private GameObject _trapEffect;
    public GameObject _blackHole;

    //=============
    public GameObject _bomb;
    //=============

    // Start is called before the first frame update
    void Start()
    {
        _ball = GameObject.Find("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDebugSkills();
    }

    private void UpdateDebugSkills()
    {
        //  �{�[����~
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //if(!_ball.transform.gameObject.GetComponent<Ball>().GetIsIce())
                 _ball.transform.gameObject.GetComponent<Ball>().StartIceEffect(this.gameObject);
        }

        //  ��
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Vector3 wall_pos = transform.Find("WallPos").gameObject.transform.position;

            PhotonNetwork.Instantiate("BarrierWall", wall_pos, transform.rotation);
        }

        //  �
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Vector3 wall_pos = transform.Find("TrapPos").gameObject.transform.position;

            GameObject a = PhotonNetwork.Instantiate("TrapEffect", wall_pos, transform.rotation);
            a.GetComponent<TrapEffect>().SetPlayer(this.gameObject);
        
        }

        //  bura
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Instantiate(_blackHole, transform.position, transform.rotation);

            _ball.transform.gameObject.GetComponent<Ball>().StartBlackHole(transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //  㩂Ƃ̓����蔻��
        if (other.gameObject.tag == "Trap")
        {
            //  �����`�[���̐l�ɂ͓�����Ȃ�
            if (this.gameObject.GetComponent<CharaScore>()._scoreInfo._teamKind !=
                other.gameObject.GetComponent<TrapEffect>()._trapTeamKind)
            {

                gameObject.GetComponent<CharactorBasic>().GetCaughtInTrap();
                other.gameObject.GetComponent<TrapEffect>().CreateBomb();
                PhotonNetwork.Destroy(other.gameObject);
                this.gameObject.GetComponent<CharactorBasic>().isSkill_ = false;
            }
        }
    }

    //  �X�L�������֘A

    //  �ǐݒu
    public void OnSkillWall()
    {
        //Vector3 wall_pos = transform.Find("WallPos").gameObject.transform.position;

        Vector3 wall_pos = transform.position + (Camera.main.transform.forward * 3);

        GameObject baria = PhotonNetwork.Instantiate("BarrierWall", wall_pos, Camera.main.transform.rotation);
        baria.transform.GetChild(0).GetComponent<WallEffect>().SetPlayer(this.gameObject);
    }


    //  �{�[����~
    public void OnSkillFriaze()
    {
        GameObject ball = GameObject.Find("Ball(Clone)");
        ball.transform.gameObject.GetComponent<Ball>().StartIceEffect(this.gameObject);
    }

    //  �g���b�v
    public void OnSkillTrap()
    {
        Vector3 wall_pos = transform.Find("TrapPos").gameObject.transform.position;

        GameObject a = PhotonNetwork.Instantiate("TrapEffect", wall_pos, transform.rotation);
        a.GetComponent<TrapEffect>().SetPlayer(this.gameObject);
        a.GetComponent<TrapEffect>().SetTrapTeamKind
            (this.gameObject.GetComponent<CharaScore>()._scoreInfo._teamKind);
    }

    //  �u���b�N�z�[��
    public void OnSkillBlackHole()
    {
        Instantiate(_blackHole, transform.position, transform.rotation);

        GameObject.Find("Ball").transform.gameObject.GetComponent<Ball>().StartBlackHole(transform.position);
    }
}
