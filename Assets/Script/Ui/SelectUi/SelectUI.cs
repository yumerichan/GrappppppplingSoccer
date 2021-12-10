using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUI : MonoBehaviour
{


    public enum State
    {
        START,
        IDLE,
        SELECT,
        NOT_SELECT,
    };

    public float _selectUISpeed;
    public float _selectingSpeed;
    public int _uiNumber;

    public float _countParam1 = 0.5f;


    private State _state;
    private RectTransform _rectTransform;
    public GameObject _arrowUI;
    private bool _isStart;


    // Start is called before the first frame update
    void Start()
    {
        _state = State.START;
        _rectTransform = gameObject.GetComponent<RectTransform>();
        _isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (MyFade._myFade._isFading == false)
        {
            _isStart = true;
        }

        switch (_state)
        {
            case State.START:
                {
                    if (_isStart)
                    {
                        Vector3 pos = _rectTransform.localPosition;

                        if(!_arrowUI.GetComponent<ArrowUI>()._isDebug)
                            pos.x -= _selectUISpeed * Time.deltaTime;
                        else
                            pos.x -= 500;

                        if (pos.x <= 0)
                        {
                            pos.x = 0;
                            _state = State.IDLE;

                            _arrowUI.gameObject.SetActive(true);
                            _arrowUI.transform.GetComponent<ArrowUI>().SetIsDecide(true);
                        }

                        _rectTransform.localPosition = pos;
                    }

                    break;
                }
            case State.IDLE:
                {


                    break;
                }
            case State.SELECT:
                {
                    _countParam1 -= Time.deltaTime;

                    if (_countParam1 < 0f)
                    {
                        Vector3 pos = _rectTransform.localPosition;

                        if (pos.y > 0)
                        {
                            if (!_arrowUI.GetComponent<ArrowUI>()._isDebug)
                                pos.y -= _selectingSpeed * Time.deltaTime;
                            else
                                pos.y -= 500;

                            //  èIóπ
                            if (pos.y <= 0)
                            {
                                pos.y = 0;
                                _state = State.IDLE;
                            }
                        }
                        if (pos.y < 0)
                        {
                            if (!_arrowUI.GetComponent<ArrowUI>()._isDebug)
                                pos.y += _selectingSpeed * Time.deltaTime;
                            else
                                pos.y += 500;


                            //  èIóπ
                            if (pos.y >= 0)
                            {
                                pos.y = 0;
                                _state = State.IDLE;
                            }
                        }

                        _rectTransform.localPosition = pos;
                    }
                    break;
                }
            case State.NOT_SELECT:
                {
                    Vector3 pos = _rectTransform.localPosition;
                    pos.x -= _selectUISpeed * Time.deltaTime;

                    if (pos.x <= -660)
                    {
                        pos.x = -660;
                        _state = State.IDLE;
                    }

                    _rectTransform.localPosition = pos;

                    break;
                }
        }

    }


    public void LetsSelect()
    {
        _state = State.SELECT;
    }

    public void LetsNotSelect()
    {
        _state = State.NOT_SELECT;
    }

    public void SetIsStart(bool is_start)
    {
        _isStart = is_start;
    }
}
