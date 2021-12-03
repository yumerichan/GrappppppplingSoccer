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
    private float on_curTime;
    public float on_maxTime = 4.5f;
    //================================================

    //================================================
    public float tw_startRotX = -45f;
    public float tw_rotSpeed = 0.18f;
    public Vector3 tw_startPos;
    public Vector3 tw_rot;
    private float tw_curTime;
    public float tw_maxTime = 11.5f;
    //================================================

    //================================================
    public float th_speed = 0.18f;
    public Vector3 th_startPos;
    public Vector3 th_startRot;
    public float th_finPos = -22f;
    //================================================

    //================================================
    public float fo_speed = 0.18f;
    public Vector3 fo_startPos;
    public Vector3 fo_startRot;
    public float fo_finPos = -22f;
    //================================================

    //================================================
    public float fi_speed = 0.18f;
    public Vector3 fi_startPos;
    public Vector3 fi_startRot;
    public float fi_finPos = -22f;
    //================================================

    //================================================
    public float si_speed = 0.18f;
    public Vector3 si_startPos;
    public Vector3 si_startRot;
    public float si_finPos = -22f;
    //================================================


    public GameObject _lookObj;

    float _x;
    float _z;
    private bool[] _isSet;
    private int[] _order;

    private int _curNumber;

    // Start is called before the first frame update
    void Start()
    {
        //_isSet = new bool[6];
        //_order = new int[6];
        //for (int i = 0; i < _isSet.Length; i++) { _isSet[i] = false; }
        //
        //for (int i = 0; i < _isSet.Length; i++)
        //{
        //    while (true)
        //    {
        //        int value = Random.Range(0, 6);
        //        if(!_isSet[value])
        //        {
        //            _isSet[value] = true;
        //            _order[i] = value + 1;
        //            break;
        //        }
        //    }
        //}

        on_curTime = tw_curTime = 0f;
        _curNumber = Random.Range(1, 6 + 1);
        _curNumber = 1;
        NextMoveCamera(_curNumber);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentMoveCamera(_curNumber);
    }

    void SetRandNum()
    {
        int i = 0;
        while (true)
        {
            int value = Random.Range(1, 6 + 1);

            if(value != _curNumber)
            {
                _curNumber = value;
                break;
            }
            else 
            { 
                i++; 
                if(i >= 10)
                {
                    _curNumber = Random.Range(1, 6 + 1);
                    break;
                }
            }
        }
    }

    void NextMoveCamera(int cur_num)
    {
        switch(cur_num)
        {
            case 1: StartMoveCamera1(); break;
            case 2: StartMoveCamera2(); break;
            case 3: StartMoveCamera3(); break;
            case 4: StartMoveCamera4(); break;
            case 5: StartMoveCamera5(); break;
            case 6: StartMoveCamera6(); break;
            default:
                Debug.Log(cur_num + "OMGOMGOMGOMGOMGOMGOMGOMGOMGOMG"); break;
        }
    }
    void CurrentMoveCamera(int cur_num)
    {
        switch (cur_num)
        {
            case 1: UpdateMoveCamera1(); break;
            case 2: UpdateMoveCamera2(); break;
            case 3: UpdateMoveCamera3(); break;
            case 4: UpdateMoveCamera4(); break;
            case 5: UpdateMoveCamera5(); break;
            case 6: UpdateMoveCamera6(); break;

            default:
                Debug.Log(cur_num + "OMGOMGOMGOMGOMGOMGOMGOMGOMGOMG");break;
        }
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

        on_curTime += Time.deltaTime;
        if (on_curTime >= on_maxTime)
        {
            on_curTime = 0f;

            SetRandNum();
            NextMoveCamera(_curNumber);
        }
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

        tw_curTime += Time.deltaTime;
        if (tw_curTime >= tw_maxTime)
        {
            tw_curTime = 0f;

            SetRandNum();
            NextMoveCamera(_curNumber);
        }
    }

    void StartMoveCamera3()
    {
        transform.position = th_startPos;
        transform.localEulerAngles = th_startRot;
    }
    void UpdateMoveCamera3()
    {
        Vector3 pos = transform.position;

        pos.x -= Time.deltaTime * th_speed;

        transform.position = pos;

        if(th_finPos < pos.x)
        {
            SetRandNum();
            NextMoveCamera(_curNumber);
        }
    }

    void StartMoveCamera4()
    {
        transform.position = fo_startPos;
        transform.localEulerAngles = fo_startRot;
    }
    void UpdateMoveCamera4()
    {
        Vector3 pos = transform.position;

        pos.x -= Time.deltaTime * fo_speed;

        transform.position = pos;

        if (fo_finPos < pos.x)
        {
            SetRandNum();
            NextMoveCamera(_curNumber);
        }
    }

    void StartMoveCamera5()
    {
        transform.position = fi_startPos;
        transform.localEulerAngles = fi_startRot;
    }
    void UpdateMoveCamera5()
    {
        Vector3 pos = transform.position;

        pos.x -= Time.deltaTime * fi_speed;

        transform.position = pos;

        if (fi_finPos < pos.x)
        {
            SetRandNum();
            NextMoveCamera(_curNumber);
        }
    }

    void StartMoveCamera6()
    {
        transform.position = si_startPos;
        transform.localEulerAngles = si_startRot;
    }
    void UpdateMoveCamera6()
    {
        Vector3 pos = transform.position;

        pos.x -= Time.deltaTime * si_speed;

        transform.position = pos;

        if (si_finPos > pos.x)
        {
            SetRandNum();
            NextMoveCamera(_curNumber);
        }
    }
}
