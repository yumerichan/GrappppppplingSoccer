using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private GameObject ball_;               //ボール
    private Rigidbody rb_;                  //リジットボディ
    private GameObject player_;             //プレイあー
    private float maxBoostGage_ = 100.0f;   //ブーストゲージ最大量
    private float initBoostGage_ = 50.0f;   //ブーストゲージ初期値

    [SerializeField]
    private float boostPower_;              //ブーストのパワー
    [SerializeField]
    private float boostGageSpeed_;          //ブーストゲージがたまる速さ
    [SerializeField]
    private float consumptionAmount_;       //ゲージ消費量
    [SerializeField]
    private float curBoostGage_;            //現在のブーストゲージ
    [SerializeField]
    private GameObject boostUI_;            //ブーストUI

    [SerializeField]
    private Camera _camera;                 //カメラ

    // Start is called before the first frame update
    void Start()
    {
        //  カメラ取得
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //  ブーストボタンが押されたら
    public void OnBallBoost()
    {
        //  ゲージが溜まってないなら
        if(consumptionAmount_ > curBoostGage_)
        {
            //  撃てない！！！
            return;
        }

        ball_ = GameObject.FindGameObjectWithTag("Ball");
        rb_ = GetComponent<Rigidbody>();

        //  移動量リセット
        rb_.velocity = new Vector3(0, 0, 0);

        ////  キャラクターからボールへのベクトル生成
        Vector3 boost_vec = (ball_.transform.position - this.transform.position).normalized * boostPower_;

        //  ボールの方向に力を加える
        rb_.AddForce(boost_vec, ForceMode.Impulse);
       
        //  ゲージ更新処理
        curBoostGage_ -= consumptionAmount_;
        boostUI_.GetComponent<CircleSlider>().SetBoost(curBoostGage_);
    }

    //  カメラ方向にブースト
    public void OnCamBoost()
    {
        //  ゲージが溜まってないなら
        if (consumptionAmount_ > curBoostGage_)
        {
            //  撃てない！！！
            return;
        }

        //  移動量リセット
        rb_.velocity = new Vector3(0, 0, 0);

        //  カメラが向いている方向にブースト
        Vector3 boost_vec = _camera.transform.forward.normalized * boostPower_;

        //  ボールの方向に力を加える
        rb_.AddForce(boost_vec, ForceMode.Impulse);

        //  ゲージ更新処理
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
