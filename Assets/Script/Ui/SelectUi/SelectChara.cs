using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectChara : MonoBehaviour
{
    static public string charaName_;    //�I�������L�����N�^�[�̖��O

    public Camera camera_;      //�J����

    private string[] name_ = { "Chara_Wall", "Chara_Freeze" ,
        "Chara_Trap" , "Character_Blackhole" };     //���O

    private int charaNumber_;   //�ԍ�

    private bool preIsRight_ = false;   //�O�t���E����
    private bool preIsLeft_ = false;    //�O�t��������

    private bool IsRightRotation;
    private bool IsLeftRotation;

    private float CurrentCameraY;
 
    private enum CharaType
    {
        Wall,
        Freeze,
        Trap, 
        BlacHole,
    }

    // Start is called before the first frame update
    void Start()
    {
        charaNumber_ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCheckRotation())
        {
            Selecte();
        }
    }

    //  �I��
    private void Selecte()
    {
        bool is_right = false;
        bool is_left = false;

        //  �p�b�h�̏\���{�^���擾(���̐��F�E�@���̐��F��)
        float input_button;
        input_button = Input.GetAxis("Horizontal");

        //  �p�b�h�̃X�e�B�b�N�l�擾
        float input_stick = Input.GetAxis("Horizontal");

        if (input_button > 0 || input_stick > 0)
            is_right = true;

        if (input_button < 0 || input_stick < 0)
            is_left = true;

        
        //  �E�������ꂽ
        if(preIsRight_ == false && is_right)
        {
            IsRightRotation = true;

            //  �J������]
            CurrentCameraY += 90.0f;           

            charaNumber_++;

            if (charaNumber_ > (int)CharaType.BlacHole)
                charaNumber_ = (int)CharaType.Wall;
        }

        //  ���������ꂽ
        if(preIsLeft_ == false && is_left)
        {
            IsLeftRotation = true;

            CurrentCameraY -= 90.0f;

            if (CurrentCameraY == -90.0f)
            {
                CurrentCameraY = 270.0f;

                Vector3 cam_rot = camera_.transform.rotation.eulerAngles;

                cam_rot -= new Vector3(0.0f, 1.0f, 0.0f);
  
                    //  ��]�j�ϊ�
                camera_.transform.rotation = Quaternion.Euler(cam_rot);
            }

            charaNumber_--;

            if (charaNumber_ < (int)CharaType.Wall)
                charaNumber_ = (int)CharaType.BlacHole;
        }

        //  ���͐U��g�ۑ�
        preIsRight_ = is_right;
        preIsLeft_ = is_left;

        //  �L��������
        if(Input.GetButtonDown("Enter"))
        {
            //  �L�������ݒ�
            charaName_ = name_[charaNumber_];

            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        //  �L�����I����ʂɔ��
        SceneManager.LoadScene("PlayScene");
    }

    private bool IsCheckRotation()
    {
        Quaternion rot = camera_.transform.rotation;
        //  ��]�p�v�Z�p
        //  Quaternion�@���I�C���[�j�ɕϊ�
        Vector3 cam_rot = rot.eulerAngles;

        if (IsLeftRotation)
        {   
            if (CurrentCameraY == 0.0f)
            {
                if (0 != (int)rot.eulerAngles.y)
                {
                    cam_rot -= new Vector3(0.0f, 1.0f, 0.0f);
                }
                else
                {
                    CurrentCameraY = 0;
                    cam_rot.y = CurrentCameraY;
                    IsLeftRotation = false;
                }
            }
            else
            {
                if (CurrentCameraY < rot.eulerAngles.y)
                {

                    cam_rot -= new Vector3(0.0f, 1.0f, 0.0f);
                }
                else
                {
                    IsLeftRotation = false;
                }
            }

            //  ��]�j�ϊ�
            camera_.transform.rotation = Quaternion.Euler(cam_rot);

            return false;
        }
        else if(IsRightRotation)
        {
            if (CurrentCameraY == 360.0f)
            {
                if (0 != (int)rot.eulerAngles.y)
                {
                    cam_rot += new Vector3(0.0f, 1.0f, 0.0f);
                }
                else
                {
                    CurrentCameraY = 0;
                    cam_rot.y = CurrentCameraY;
                    IsRightRotation = false;
                }
            }
            else
            {
                if (CurrentCameraY > rot.eulerAngles.y)
                {
                    cam_rot += new Vector3(0.0f, 1.0f, 0.0f);
                }
                else
                {
                    cam_rot.y = CurrentCameraY;
                    IsRightRotation = false;
                }
            }
            

            //  ��]�j�ϊ�
            camera_.transform.rotation = Quaternion.Euler(cam_rot);

            return false;
        }

        return true;
    }
}
