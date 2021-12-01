using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WallEffect : MonoBehaviour
{
    public enum State
    { 
        START,
        LOOP,
        FIN,

        STATE_NUM,
    };

    private GameObject _player;

    public float _NormalHeight;     //壁の正規化の高さ
    public float _height;           //実際の壁の高さ
    public float _addSpd;           //壁の登場スピード
    public float _addAlpha;

    public float _effectTime;

    private float _curTime;
    private float _alpha;
    private WallEffect.State _state;
    private BoxCollider _boxColl;

    // Start is called before the first frame update
    void Start()
    {
        _boxColl = GetComponent<BoxCollider>();
        _boxColl.enabled = false;
        _state = State.START;
        _alpha = 1f;
        _curTime = 0f;
        GetComponent<Renderer>().material.SetFloat("_Dissolve", 1);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.START:
                StepStart();    break;
            case State.LOOP:
                StepLoop();     break;
            case State.FIN:
                StepFin();      break;
        }
    }

    void StepStart()
    {
#if false
        _NormalHeight -= _addSpd;
        GetComponent<Renderer>().material.SetFloat("_HeightFactor", _NormalHeight);

        Vector3 coll_center =_boxColl.center;
        Vector3 coll_size = _boxColl.size;
        coll_center.y -= (_height * _addSpd) / 2f;
        coll_size.y += _height * _addSpd;
        _boxColl.center = coll_center;
        _boxColl.size = coll_size;


        GetComponent<HC.Debug.ColliderVisualizer>().SetCenter(_boxColl);

        if (_NormalHeight <= 0f)
        {
            _NormalHeight = 0f;
            _state = State.LOOP;
        }
#endif
        _alpha -= _addAlpha;
        GetComponent<Renderer>().material.SetFloat("_Dissolve", _alpha);
        if (_alpha <= 0f)
        {
            _alpha = 0f;
            _state = State.LOOP;
            _boxColl.enabled = true;
        }

        
    }
    void StepLoop()
    {
        _curTime += Time.deltaTime;
        if(_effectTime <= _curTime)
        {
            _state = State.FIN;
        }
    }

    void StepFin()
    {
        _alpha += _addAlpha;
        GetComponent<Renderer>().material.SetFloat("_Dissolve", _alpha);
        if (_alpha >= 1f)
        {
            PhotonNetwork.Destroy(transform.parent.gameObject);
            _player.GetComponent<CharactorBasic>().isSkill_ = false;

            _player = null;
        }
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;

    }
}
