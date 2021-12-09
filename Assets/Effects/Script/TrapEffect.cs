using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TrapEffect : MonoBehaviour
{
    public enum State
    {
        START,
        LOOP,
        FIN,

        STATE_NUM,
    };

    public float _addAlpha;
    public float _timeLimit;
    public GameObject _TrapBombEffect;
    private SphereCollider _boxColl;
    private GameObject _player;

    private TrapEffect.State _state;

    private float _curTime;
    private float _alpha;

    public int _trapTeamKind;


    public float _bombSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _curTime = 0;
        _state = State.START;
        _alpha = 1f;
        GetComponent<Renderer>().material.SetFloat("_Dissolve", 1);
        _boxColl = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.START:
                StepStart(); break;
            case State.LOOP:
                StepLoop(); break;
            case State.FIN:
                StepFin(); break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //  ボールとの当たり判定
        if (other.gameObject.tag == "Ball")
        {

            if (this._state == State.LOOP)
            {
                //ボールは思いっきりぶっ飛ばす
                Vector3 vel = new Vector3(Random.Range(-1, 1),
                    Random.Range(-1, 1), Random.Range(-1, 1)).normalized * _bombSpeed;
                other.gameObject.GetComponent<Rigidbody>().velocity = vel;

                CreateBomb();
                PhotonNetwork.Destroy(this.gameObject);
                _player.gameObject.GetComponent<CharactorBasic>().isSkill_ = false;
            }
        }
    }

    public void CreateBomb()
    {
        PhotonNetwork.Instantiate("TrapBomb", transform.position, Quaternion.identity);
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

        if (_timeLimit <= _curTime)
        {
            _state = State.FIN;
            _boxColl.enabled = false;
            _player.GetComponent<CharactorBasic>().isSkill_ = false;
        }
    }

    void StepFin()
    {
        _alpha += _addAlpha;
        GetComponent<Renderer>().material.SetFloat("_Dissolve", _alpha);
        if (_alpha >= 1f)
        {
            PhotonNetwork.Destroy(this.gameObject);
           

            _player = null;
        }
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;

    }

    public void SetTrapTeamKind(int kind)
    {
        _trapTeamKind = kind;
    }
}
