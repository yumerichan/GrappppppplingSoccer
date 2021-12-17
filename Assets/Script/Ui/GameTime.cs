using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameTime : MonoBehaviour
{
    [SerializeField]
    private float initGameTime_;    //�Q�[���̎���
    private float curTime_;         //���݂̎c�莞��
    public Text gameTimeText_;      //�\���p

    enum TimeKind
    {
        WAIT,
        PLAY,
        PAUSE,
        FIN,
    };

    TimeKind _timeKind;

    // Start is called before the first frame update
    void Start()
    { 
        //  ���ԏ�����
        curTime_ = initGameTime_;
        _timeKind = TimeKind.WAIT;

        gameTimeText_.text = "PleaseWait...";
    }

    

    // Update is called once per frame
    void Update()
    {
        switch(_timeKind)
        {
            case TimeKind.WAIT:
                StepWait(); break;
            case TimeKind.PLAY:
                StepPlay(); break;
            case TimeKind.PAUSE:
                StepPause(); break;
            case TimeKind.FIN:
                StepFin(); break;
        }
       
    }


    private void StepWait()
    {
        
    }


    public void StartPlay()
    {
        _timeKind = TimeKind.PLAY;
    }

    private void StepPlay()
    {
        //  �o�ߎ��Ԃ�����
        curTime_ -= Time.deltaTime;

        int minutes = (int)(curTime_ / 60);
        int second = (int)(curTime_ % 60);

        //  ���Ԃ�\��
        gameTimeText_.text = string.Format("{0:00}:{1:00}", minutes, second);
    
        if (curTime_ <= 0f)
        {
            _timeKind = TimeKind.FIN;
        }
    
    }


    private void StepPause()
    {

    }


    private void StepFin()
    {
        //GameObject.Find("CharaScoreManager").GetComponent<CharaScoreManager>().FinGame();
    }
}
