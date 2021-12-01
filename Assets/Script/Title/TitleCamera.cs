using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour
{

    //================================================
    public float on_speed = 0.5f;
    public float on_radius = 90f;
    public float on_startRot = 90f;
    public float on_startY = 85f;
    public float on_rot;
    //================================================

    //================================================
    public float tw_startRotX = -45f;
    public float tw_rotSpeed = 0.18f;
    public Vector3 tw_startPos;
    public Vector3 tw_rot;
    //================================================

    //================================================
    public float th_speed = 0.18f;
    public Vector3 th_startPos;
    public float th_finPos = -22f;
    //================================================


    public GameObject _lookObj;

    float _x;
    float _z;


    // Start is called before the first frame update
    void Start()
    {
        StartMoveCamera3();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoveCamera3();
    }


    void StartMoveCamera1()
    {
        _x = on_radius * Mathf.Sin(on_startRot);
        _z = on_radius * Mathf.Cos(on_startRot);
        on_rot = on_startRot;

        transform.position = new Vector3(_x, on_startY, _z);
    }
    void UpdateMoveCamera1()
    {
        on_rot += Time.deltaTime * on_speed;

        _x = on_radius * Mathf.Sin(on_rot);
        _z = on_radius * Mathf.Cos(on_rot);

        transform.position = new Vector3(_x, transform.position.y, _z);

        transform.LookAt(_lookObj.gameObject.transform);
    }

    void StartMoveCamera2()
    {
        tw_rot = new Vector3(tw_startRotX, 0, 0);

        transform.position = tw_startPos;
        transform.localEulerAngles = tw_rot;
    }
    void UpdateMoveCamera2()
    {
        tw_rot.y += Time.deltaTime * tw_rotSpeed;

        transform.localEulerAngles = tw_rot;
    }

    void StartMoveCamera3()
    {
        transform.position = th_startPos;
        transform.localEulerAngles = Vector3.zero;
    }
    void UpdateMoveCamera3()
    {
        Vector3 pos = transform.position;

        pos.x -= Time.deltaTime * th_speed;

        transform.position = pos;
    }
}
