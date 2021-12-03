using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class Respawn : MonoBehaviourPunCallbacks
{
    private NewWorkInfo nw_info;

    // Start is called before the first frame update
    void Start()
    {
        nw_info = GameObject.Find("NetWork").GetComponent<NewWorkInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetRespawn()
    {
        RedSrart red1 = GameObject.Find("Red1").GetComponent<RedSrart>();
        RedSrart2 red2 = GameObject.Find("Red2").GetComponent<RedSrart2>();
        RedSrart3 red3 = GameObject.Find("Red3").GetComponent<RedSrart3>();
        BlueStart blue1 = GameObject.Find("Blue1").GetComponent<BlueStart>();
        BlueStart2 blue2 = GameObject.Find("Blue2").GetComponent<BlueStart2>();
        BlueStart3 blue3 = GameObject.Find("Blue3").GetComponent<BlueStart3>();
        red1.SetIsCollision();
        red2.SetIsCollision();
        red3.SetIsCollision();
        blue1.SetIsCollision();
        blue2.SetIsCollision();
        blue3.SetIsCollision();
    }

    public Vector3 RespawnPlayer()
    {
        RedSrart red1 = GameObject.Find("Red1").GetComponent<RedSrart>();
        RedSrart2 red2 = GameObject.Find("Red2").GetComponent<RedSrart2>();
        RedSrart3 red3 = GameObject.Find("Red3").GetComponent<RedSrart3>();
        BlueStart blue1 = GameObject.Find("Blue1").GetComponent<BlueStart>();
        BlueStart2 blue2 = GameObject.Find("Blue2").GetComponent<BlueStart2>();
        BlueStart3 blue3 = GameObject.Find("Blue3").GetComponent<BlueStart3>();

        Vector3 r_vec = GameObject.Find("RedStart").transform.position;
        var redpotision = r_vec;
        Vector3 b_vec = GameObject.Find("BlueStart").transform.position;
        var bluepotision = b_vec;

        //赤
        if (nw_info.GetTeamColor() == 0)
        {
            if (red1.GetOnRedCollision())
            {
                redpotision = new Vector3(r_vec.x + 40.0f, r_vec.y, r_vec.z);
            }

            if (red2.GetOnRedCollision())
            {
                redpotision = new Vector3(r_vec.x - 40.0f, r_vec.y, r_vec.z);
            }

            if (red3.GetOnRedCollision())
            {
                redpotision = new Vector3(r_vec.x - 80.0f, r_vec.y, r_vec.z);
            }

            //この座標をプレイヤーにいれるだけでいける
            return redpotision;
        }

        //青
        if (nw_info.GetTeamColor() == 1)
        {
            if (blue1.GetOnRedCollision())
            {
                bluepotision = new Vector3(b_vec.x + 40.0f, b_vec.y, b_vec.z);
            }

            if (blue2.GetOnRedCollision())
            {
                bluepotision = new Vector3(b_vec.x - 40.0f, b_vec.y, b_vec.z);
            }

            if (blue3.GetOnRedCollision())
            {
                bluepotision = new Vector3(b_vec.x - 80.0f, b_vec.y, b_vec.z);
            }

            return bluepotision;
        }

        return new Vector3(0.0f, 0.0f, 0.0f);
    }
}
