using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    [SerializeField]
    private float initGameTime_;    //ゲームの時間
    private float curTime_;         //現在の残り時間
    public Text gameTimeText_;      //表示用

    // Start is called before the first frame update
    void Start()
    { 
        //  時間初期化
        curTime_ = initGameTime_;
    }

    // Update is called once per frame
    void Update()
    {
        //  経過時間を引く
        curTime_ -= Time.deltaTime;

        int minutes = (int)(curTime_ / 60);
        int second = (int)(curTime_ % 60);
        
        //  時間を表示
        gameTimeText_.text = string.Format("{0:00}:{1:00}", minutes, second);

       
    }
}
