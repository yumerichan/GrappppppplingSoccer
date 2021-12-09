using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TitleScene : MonoBehaviour
{
    private bool _isFading;
    private float _fadeAlpha;

    public Image _fadeImage;

    // Start is called before the first frame update
    void Start()
    {
        _isFading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //シーン遷移
            LoadLevel("SceneSelect", 2f);
        }
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

    public void LoadLevel(string scene, float interval)
    {
        StartCoroutine(TransScene(scene, interval));
    }

    public float GetAlpha()
    {
        return _fadeAlpha;
    }
}
