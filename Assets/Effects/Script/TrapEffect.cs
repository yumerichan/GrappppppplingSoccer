using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEffect : MonoBehaviour
{
    public float _timeLimit;
    public GameObject _TrapBombEffect;

    private float _curTime;

    // Start is called before the first frame update
    void Start()
    {
        _curTime = 0;

    }

    // Update is called once per frame
    void Update()
    {
        _curTime += Time.deltaTime;

        if(_timeLimit <= _curTime)
        {
            Destroy(this.gameObject);
        }
    }

    public void CreateBomb()
    {
        Instantiate(_TrapBombEffect, transform.position, Quaternion.identity);
    }
}
