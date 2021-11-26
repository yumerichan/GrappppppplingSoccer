using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFade : MonoBehaviour
{
    public Fade _fade;

    public float _fadeInTime;
    public float _fadeOutTime;

    private void Start()
    {
       
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
}
