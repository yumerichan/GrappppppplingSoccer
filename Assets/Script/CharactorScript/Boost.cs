using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private GameObject ball_;               //�{�[��
    private Rigidbody rb_;                  //���W�b�g�{�f�B
    private GameObject player_;             //�v���C���[
    private float maxBoostGage_ = 100.0f;   //�u�[�X�g�Q�[�W�ő��
    private float initBoostGage_ = 50.0f;   //�u�[�X�g�Q�[�W�����l

    [SerializeField]
    private float boostPower_;              //�u�[�X�g�̃p���[
    [SerializeField]
    private float boostGageSpeed_;          //�u�[�X�g�Q�[�W�����܂鑬��
    [SerializeField]
    private float consumptionAmount_;       //�Q�[�W�����
    [SerializeField]
    private float curBoostGage_;            //���݂̃u�[�X�g�Q�[�W
    [SerializeField]
    private GameObject boostUI_;            //�u�[�X�gUI

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //  �u�[�X�g�{�^���������ꂽ��
    public void OnBoost()
    {
        //  �Q�[�W�����܂��ĂȂ��Ȃ�
        if(consumptionAmount_ > curBoostGage_)
        {
            //  ���ĂȂ��I�I�I
            return;
        }

        ball_ = GameObject.FindGameObjectWithTag("Ball");
        rb_ = GetComponent<Rigidbody>();

        //  �ړ��ʃ��Z�b�g
        rb_.velocity = new Vector3(0, 0, 0);

        //  �L�����N�^�[����{�[���ւ̃x�N�g������
        Vector3 boost_vec = (ball_.transform.position - this.transform.position).normalized * boostPower_;

        //  �{�[���̕����ɗ͂�������
        rb_.AddForce(boost_vec, ForceMode.Impulse);

        player_ = GameObject.FindGameObjectWithTag("Player");

       
        //  �Q�[�W�X�V����
        curBoostGage_ -= consumptionAmount_;
        boostUI_.GetComponent<CircleSlider>().SetBoost(curBoostGage_);
    }

    public float GetBoostPower()
    {
        return boostPower_;
    }
    public float GetGageSpeed()
    {
        return boostGageSpeed_;
    }
    public float GetConsumptionAmount()
    {
        return consumptionAmount_;
    }
    public float GetCurBoostGage()
    {
        return curBoostGage_;
    }
    public void SetCurBoostGage(float gage)
    {
        curBoostGage_ = gage * 100f;
    }
}
