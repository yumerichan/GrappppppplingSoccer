using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TitleScene : MonoBehaviour
{
    private bool _isFading;
    private float _fadeAlpha;

    public Image _fadeImage;

    public Button _playB;
    public Button _opB;
    public Button _exitB;

    private int _curSelect;
    private Button[] _allButtons;

    private float _coolTime;

    [SerializeField] private EventSystem eventSystem;


    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _isFading = false;
        _curSelect = 0;
        _allButtons = new Button[3];
        _allButtons[0] = _playB;
        _allButtons[1] = _opB;
        _allButtons[2] = _exitB;

        Vector3 scale2 = _allButtons[_curSelect].transform.localScale;
        scale2.x = 1.3f;
        _allButtons[_curSelect].transform.localScale = scale2;

        Vector3 scale4 = _allButtons[_curSelect].transform.GetChild(0).transform.localScale;
        scale4.x = 0.8f;
        _allButtons[_curSelect].transform.GetChild(0).transform.localScale = scale4;

        GameObject.Find("FadeManager").gameObject.GetComponent<MyFade>().StartFadeOut();
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
            is_down = true;

        if (input_button < 0 || input_stick < 0)
            is_up = true;

        if (_coolTime <= 0f)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || is_up)
            {
                Vector3 scale = _allButtons[_curSelect].transform.localScale;
                scale.x = 1f;
                _allButtons[_curSelect].transform.localScale = scale;

                Vector3 scale3 = _allButtons[_curSelect].transform.GetChild(0).transform.localScale;
                scale3.x = 1f;
                _allButtons[_curSelect].transform.GetChild(0).transform.localScale = scale3;

                _curSelect++;

                Vector3 scale2 = _allButtons[_curSelect].transform.localScale;
                scale2.x = 1.3f;
                _allButtons[_curSelect].transform.localScale = scale2;

                Vector3 scale4 = _allButtons[_curSelect].transform.GetChild(0).transform.localScale;
                scale4.x = 0.8f;
                _allButtons[_curSelect].transform.GetChild(0).transform.localScale = scale4;

                if (_curSelect > 2)
                {
                    _curSelect = 0;
                }

                _coolTime = 0.5f;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || is_down)
            {
                Vector3 scale = _allButtons[_curSelect].transform.localScale;
                scale.x = 1f;
                _allButtons[_curSelect].transform.localScale = scale;

                Vector3 scale3 = _allButtons[_curSelect].transform.GetChild(0).transform.localScale;
                scale3.x = 1f;
                _allButtons[_curSelect].transform.GetChild(0).transform.localScale = scale3;


                _curSelect--;

                Vector3 scale2 = _allButtons[_curSelect].transform.localScale;
                scale2.x = 1.3f;
                _allButtons[_curSelect].transform.localScale = scale2;

                Vector3 scale4 = _allButtons[_curSelect].transform.GetChild(0).transform.localScale;
                scale4.x = 0.8f;
                _allButtons[_curSelect].transform.GetChild(0).transform.localScale = scale4;

                if (_curSelect < 0)
                {
                    _curSelect = 2;
                }

                _coolTime = 0.5f;
            }


            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Enter"))
            {
                eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
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

        

        _allButtons[_curSelect].Select();


    }

    private IEnumerator TransScene(string scene, float interval)
    {
        //だんだん暗く
        this._isFading = true;
        float time = 0;
        while (time <= interval)
        {
            this._fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            _fadeImage.SetOpacity(_fadeAlpha);
            time += Time.deltaTime;
            yield return 0;
        }

        //シーン切替
        SceneManager.LoadScene(scene);

        //だんだん明るく
        time = 0;
        while (time <= interval)
        {
            this._fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            _fadeImage.SetOpacity(_fadeAlpha);
            time += Time.deltaTime;
            yield return 0;
        }

        this._isFading = false;
    }

    private IEnumerator TransExit(float interval)
    {
        //だんだん暗く
        this._isFading = true;
        float time = 0;
        while (time <= interval)
        {
            this._fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            _fadeImage.SetOpacity(_fadeAlpha);
            time += Time.deltaTime;
            yield return 0;
        }

        Quit();
    }

    public void LoadLevel(string scene, float interval)
    {
        GameObject a = GameObject.Find("FadeManager").gameObject;
        MyFade._myFade.LoadLevel(scene, interval);
    }

    
    public float GetAlpha()
    {
        return _fadeAlpha;
    }

    public void PushStart()
    {
        //シーン遷移
        LoadLevel("SceneSelect", 2f);
        
    }

    public void PushOp()
    {

    }

    public void PushExit()
    {
        StartCoroutine(TransExit(1.5f));
    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
}
