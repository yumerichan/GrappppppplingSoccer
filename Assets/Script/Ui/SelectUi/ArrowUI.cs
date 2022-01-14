using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

public class ArrowUI : MonoBehaviour
{
    public int _curNumber;

    public GameObject _image1v1;
    public GameObject _image2v2;
    public GameObject _image3v3;
    public GameObject _image4v4;
    public GameObject _imageBackToTitle;
    public int _imageNum;

    public GameObject _fade;
    public GameObject _myFade;

    public float _countParam1 = 0.5f;

    private GameObject[] _imageInfo;
    private RectTransform _rectTransform;
    private bool _isDecide;
    private bool _isCanDecide;

    static public int selectNumber_;    //人数

    public bool _isDebug = false;

    private float _coolTime;

    /* サウンド関連 */
    private AudioSource _audioSource;   //オーディオソース
    public AudioClip _cursor;           //カーソル
    public AudioClip _diside;           //決定

    // Start is called before the first frame update
    void Start()
    {

        Application.targetFrameRate = 60;
        _coolTime = 0f;

        _curNumber = 0;
        _imageInfo = new GameObject[] { _image1v1, _image2v2, _image3v3, _image4v4, _imageBackToTitle };
        _rectTransform = gameObject.GetComponent<RectTransform>();

        gameObject.SetActive(false);
        _isCanDecide = false;
        _isDecide = false;

        _audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        bool is_down = false;
        bool is_up = false;

        //  パッドの十字ボタン取得(正の数：右　負の数：左)
        float input_button;
        input_button = Input.GetAxis("Vertical");

        //  パッドのスティック値取得
        float input_stick = Input.GetAxis("Vertical");

        if (input_button > 0 || input_stick > 0)
            is_up = true;

        if (input_button < 0 || input_stick < 0)
            is_down = true;

        if (_coolTime <= 0f)
        {
            //  下に移動
            if (Input.GetKeyDown(KeyCode.DownArrow) || is_down)
            {
                _curNumber++;
                if (_curNumber >= _imageNum)
                {
                    _curNumber = 0;
                }

                _coolTime = 0.4f;

                //  カーソル音再生
                _audioSource.PlayOneShot(_cursor);
            }

            //  上に移動
            if (Input.GetKeyDown(KeyCode.UpArrow) || is_up)
            {
                _curNumber--;
                if (_curNumber < 0)
                {
                    _curNumber = _imageNum - 1;
                }

                _coolTime = 0.4f;

                //  カーソル音再生
                _audioSource.PlayOneShot(_cursor);
            }
        }
        else
        {
            _coolTime -= Time.deltaTime;

            if (!Input.GetKeyDown(KeyCode.DownArrow) && !is_up &&
                !Input.GetKeyDown(KeyCode.UpArrow) && !is_down)
            {
                _coolTime = 0f;
            }
        }


        //  決定
        if (_isCanDecide)
        {
            if (!_isDecide)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Enter"))
                {
                    for (int i = 0; i < _imageInfo.Length; i++)
                    {
                        if (i == _curNumber)
                        {
                            _imageInfo[i].transform.GetComponent<SelectUI>().LetsSelect();
                            selectNumber_ = (i + 1) * 2;
                        }
                        else
                            _imageInfo[i].transform.GetComponent<SelectUI>().LetsNotSelect();
                    }

                    gameObject.SetActive(true);
                    _isDecide = true;
                    //  決定音再生
                    _audioSource.PlayOneShot(_diside);
                }
            }
        }

        //  座標を反映
        Vector3 pos = _rectTransform.localPosition;
        pos.y = _imageInfo[_curNumber].gameObject.GetComponent<RectTransform>().localPosition.y;
        _rectTransform.localPosition = pos;


        if(_isDecide)
        {
            _countParam1 -= Time.deltaTime;

            if (_isDebug) { _countParam1 = -1f; }

            if(_countParam1 < 0f)
            {
                //  終了
                MyFade._myFade.LoadLevel("CharactorSelect", 1f);
            }
        }
    }

    public void SetIsDecide(bool is_decide)
    {
        _isCanDecide = is_decide;
    }

    public void StartingUI()
    {
        for (int i = 0; i < _imageInfo.Length; i++)
        {          
            _imageInfo[i].transform.GetComponent<SelectUI>().SetIsStart(true);  
        }
    }

    private void ChangeScene()
    {
        //  キャラ選択画面に飛ぶ
        SceneManager.LoadScene("CharactorSelect");
    }

    public bool GetIsDebug()
    {
        return _isDebug;
    }
}
