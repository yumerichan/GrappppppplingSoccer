using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResoultScene : MonoBehaviour
{
    //  =======�X�R�A�{��=============

    //  ���_      �@�F800
    //  �A�V�X�g    �F300
    //  �X�L������  �F100

    //================================

    private int _goalScore = 800;
    private int _assistScore = 300;
    private int _skillScore = 100;

    private int _myScore;           //�����̃`�[���̃X�R�A
    private int _opponentScore;     //����̃X�R�A

    private int[] _goalNum;         //�`�[���̃S�[���̐�
    private int[] _assistNum;       //�`�[���̃A�V�X�g�̐�
    private int[] _skillNum;        //�`�[���̃X�L���A�V�X�g�̐�
    private int[] _sumScore;        //�X�R�A�̍��v

    // Start is called before the first frame update
    void Start()
    {
        //  �v���C�V�[������X�R�A�̏��������Ă���
    }

    // Update is called once per frame
    void Update()
    {

    }
}
