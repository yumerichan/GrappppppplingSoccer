using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FreeLookCameraInfo : MonoBehaviour
{
    CinemachineFreeLook _freeLookCamera;

    void Start()
    {
        _freeLookCamera = GetComponent<CinemachineFreeLook>();
    }

    public void ChangeHorSpeed(float slidr)
    {
        //200
        _freeLookCamera.m_XAxis.m_MaxSpeed = 600.0f * slidr;
    }

    public void ChangeVerSpeed(float slidr)
    {
        //1.5

        _freeLookCamera.m_YAxis.m_MaxSpeed = 3.0f * slidr;
    }
}
