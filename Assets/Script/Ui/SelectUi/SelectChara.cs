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

    //  選択
    private void Selecte()
    {
        bool is_right = false;
        bool is_left = false;

        //  パッドの十字ボタン取得(正の数：右　負の数：左)
        float input_button;
        input_button = Input.GetAxis("Horizontal");

        //  パッドのスティック値取得
        float input_stick = Input.GetAxis("Horizontal");

        if (input_button > 0 || input_stick > 0)
            is_right = true;

        if (input_button < 0 || input_stick < 0)
            is_left = true;

        
        //  右が押された
        if(preIsRight_ == false && is_right)
        {
            IsRightRotation = true;

            //  カメラ回転
            CurrentCameraY += 90.0f;           

            charaNumber_++;

            if (charaNumber_ > (int)CharaType.BlacHole)
                charaNumber_ = (int)CharaType.Wall;
        }

        //  左が押された
        if(preIsLeft_ == false && is_left)
        {
            IsLeftRotation = true;

            CurrentCameraY -= 90.0f;

            if (CurrentCameraY == -90.0f)
            {
                CurrentCameraY = 270.0f;

                Vector3 cam_rot = camera_.transform.rotation.eulerAngles;

                cam_rot -= new Vector3(0.0f, 1.0f, 0.0f);
  
                    //  回転核変換
                camera_.transform.rotation = Quaternion.Euler(cam_rot);
            }

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

    private bool IsCheckRotation()
    {
        Quaternion rot = camera_.transform.rotation;
        //  回転角計算用
        //  Quaternion　をオイラー核に変換
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

            //  回転核変換
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
            

            //  回転核変換
            camera_.transform.rotation = Quaternion.Euler(cam_rot);

            return false;
        }

        return true;
    }
}
