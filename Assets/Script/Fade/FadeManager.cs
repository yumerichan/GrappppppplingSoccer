using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    const float FADESPEED = 0.005f;    //�t�F�[�h�̃X�s�[�h

    public enum FADE_TYPE
    {
        FADE_IN,
        FADE_OUT,
    }

    public static FadeManager Instance;

    private float fRed, fGreen, fBlue, fAlpha; //�F,�����x
    public bool IsFadeOut = false;     //�t�F�[�h�A�E�g�t���O
    public bool IsFadeIn = false;      //�t�F�[�h�C���t���O
    private bool IsFadeEnd = false;

    //canvas���o������R�����g�A�E�g��������Component���Ă�������
    Image fade_image;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void DeleteInstance()
    {
        if (Instance != null)
            Destroy(Instance);
    }

    // Start is called before the first frame update
    void Start()
    {
        fade_image = GetComponent<Image>();
        fade_image.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        fRed = fade_image.color.r;
        fGreen = fade_image.color.g;
        fBlue = fade_image.color.b;
        fAlpha = fade_image.color.a;

        IsFadeEnd = false;
        IsFadeIn = true;
        IsFadeOut = false;
        fAlpha = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFadeOut)
            StepFadeOut();

        if (IsFadeIn)
            StepFadeIn();
    }

    //�t�F�[�h�A�E�g����
    void StepFadeOut()
    {
        fade_image.enabled = true;

        if (fAlpha >= 1.0f)
        {
            IsFadeOut = false;
            IsFadeEnd = true;
        }
        else
            fAlpha += FADESPEED;

        SetAlpha();
    }

    //�t�F�[�h�C������
    void StepFadeIn()
    {
        fade_image.enabled = true;

        if (fAlpha <= 0.0f)
        {
            IsFadeIn = false;
            fade_image.enabled = false;
        }
        else
            fAlpha -= FADESPEED;

        SetAlpha();
    }

    //�F���ݒ�
    void SetAlpha()
    {
        fade_image.color = new Color(fRed, fGreen, fBlue, fAlpha);
    }

    //�t�F�[�h�^�C�v�ύX
    public void SetFadeType(FADE_TYPE type)
    {
        IsFadeEnd = false;

        switch (type)
        {
            case (FADE_TYPE.FADE_IN):
                IsFadeIn = true;
                IsFadeOut = false;
                fAlpha = 1.0f;
                break;

            case (FADE_TYPE.FADE_OUT):
                fAlpha = 0.0f;
                IsFadeIn = false;
                IsFadeOut = true;
                break;

        }
    }

    public bool GetFadeEndFlg() { return IsFadeEnd; }
}
