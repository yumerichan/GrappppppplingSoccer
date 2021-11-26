using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectChara : MonoBehaviour
{
    static public string charaName_;    //選択したキャラクターの名前

    public Camera camera_;      //カメラ

    private string[] name_ = { "Chara_Wall", "Chara_Freeze" ,
        "Chara_Trap" , "Character_Blackhole" };     //名前

    private int charaNumber_;   //番号

    private bool preIsRight_ = false;   //前フレ右入力
    private bool preIsLeft_ = false;    //前フレ左入力

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
        Selecte(); 
    }

    //  選択
    private void Selecte()
    {
        bool is_right = false;
        bool is_left = false;

        Quaternion rot = camera_.transform.rotation;

        //  パッドの十字ボタン取得(正の数：右　負の数：左)
        float input_button;
        input_button = Input.GetAxis("Horizontal");

        //  パッドのスティック値取得
        float input_stick = Input.GetAxis("Horizontal");

        if (input_button > 0 || input_stick > 0)
            is_right = true;

        if (input_button < 0 || input_stick < 0)
            is_left = true;

        //  回転角計算用
        //  Quaternion　をオイラー核に変換
        Vector3 cam_rot = rot.eulerAngles;

        //  右が押された
        if(preIsRight_ == false && is_right)
        {
            //  カメラ回転
            cam_rot += new Vector3(0, 90.0f, 0);

            //  回転核変換
            camera_.transform.rotation = Quaternion.Euler(cam_rot);

            charaNumber_++;

            if (charaNumber_ > (int)CharaType.BlacHole)
                charaNumber_ = (int)CharaType.Wall;
        }

        //  左が押された
        if(preIsLeft_ == false && is_left)
        {
            //  カメラ回転
            //  カメラ回転
            cam_rot += new Vector3(0, -90.0f, 0);

            //  回転核変換
            camera_.transform.rotation = Quaternion.Euler(cam_rot);

            charaNumber_--;

            if (charaNumber_ < (int)CharaType.Wall)
                charaNumber_ = (int)CharaType.BlacHole;
        }

        //  入力振らg保存
        preIsRight_ = is_right;
        preIsLeft_ = is_left;

        //  キャラ決定
        if(Input.GetButtonDown("Enter"))
        {
            //  キャラ名設定
            charaName_ = name_[charaNumber_];

            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        //  キャラ選択画面に飛ぶ
        SceneManager.LoadScene("PlayScene");
    }
}
