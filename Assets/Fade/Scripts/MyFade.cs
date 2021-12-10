using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyFade : MonoBehaviour
{
    static public MyFade _myFade = null;

    public bool _isFading;
    public Fade _fade;

    public float _fadeInTime;
    public float _fadeOutTime;

    private float _fadeAlpha;

    public Image _fadeImage;

    IFade fade;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        _myFade = this;

        Init();
        fade.Range = cutoutRange;
    }

    private void Start()
    {
        _isFading = false;
    }

    float cutoutRange = 1;


    void Init()
    {
        fade = gameObject.transform.GetChild(0). GetComponent<IFade>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _fade.FadeIn(_fadeInTime, () => print("フェードイン完了"));
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            _fade.FadeOut(_fadeOutTime, () => print("フェードアウト完了"));
        }
    }

    public void StartFadeIn()
    {
        _fade.FadeIn(_fadeInTime, () => print("フェードイン完了"));
    }
    public void StartFadeOut()
    {
        _fade.FadeOut(_fadeOutTime, () => print("フェードアウト完了"));
    }

    public float GetFadeInTime()
    {
        return _fadeInTime;
    }
    public float GetFadeOutTime()
    {
        return _fadeOutTime;
    }

    public void aaa()
    {

    }

    private IEnumerator TransScenes(string scene, float interval)
    {
        this._isFading = true;
        float time = 0;
        while (time <= interval)
        {
            this._fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            _fadeImage.SetOpacity(_fadeAlpha);
            time += Time.deltaTime;
            yield return 0;
        }

        //if (action != null)
        //{
        //    action();
        //}

        //シーン切替
        SceneManager.LoadScene(scene);

        //だんだん明るく
        time = 0;
        while (time <= interval)
        {
            this._fadeAlpha = Mathf.Lerp(1f, 0f, time / interval * 2f);
            _fadeImage.SetOpacity(_fadeAlpha);
            time += Time.deltaTime;
            yield return 0;
        }

        this._isFading = false;

        //if (action != null)
        //{
        //    action();
        //}
    }

    public void LoadLevel(string scene, float inta)
    {
        StartCoroutine(TransScenes(scene, _fadeInTime));
    }

    IEnumerator GoFade(float time)
    {
        float endTime1 = Time.timeSinceLevelLoad + time * (cutoutRange);

        var endFrame1 = new WaitForEndOfFrame();

        while (Time.timeSinceLevelLoad <= endTime1)
        {
            cutoutRange = (endTime1 - Time.timeSinceLevelLoad) / time;
            fade.Range = cutoutRange;
            yield return endFrame1;
        }
        cutoutRange = 0;
        fade.Range = cutoutRange;
    }

}
