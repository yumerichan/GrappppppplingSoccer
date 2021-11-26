using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    [SerializeField]
    private float initGameTime_;    //�Q�[���̎���
    private float curTime_;         //���݂̎c�莞��
    public Text gameTimeText_;      //�\���p

    // Start is called before the first frame update
    void Start()
    { 
        //  ���ԏ�����
        curTime_ = initGameTime_;
    }

    // Update is called once per frame
    void Update()
    {
        //  �o�ߎ��Ԃ�����
        curTime_ -= Time.deltaTime;

        int minutes = (int)(curTime_ / 60);
        int second = (int)(curTime_ % 60);
        
        //  ���Ԃ�\��
        gameTimeText_.text = string.Format("{0:00}:{1:00}", minutes, second);

       
    }
}
