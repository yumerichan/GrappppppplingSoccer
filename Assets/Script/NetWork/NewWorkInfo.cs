using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWorkInfo : MonoBehaviour
{
    private int TeamColor;              //0 == �� 1 == ��
    private int TeamNumber;             //�`�[���ԍ�

    private bool IsInstantiate;         //�������Ă悢���H

    // Start is called before the first frame update
    void Start()
    {
        TeamColor = -1;
        TeamNumber = -1;
        IsInstantiate = false;
    }  

    public void SetTeamColor(int color)
    {
        TeamColor = color;
    }

    public void SetTeamNumber(int number)
    {
        TeamNumber = number;
    }

    public void SetInstiate(bool ins)
    {
        IsInstantiate = ins;
    }

    public bool GetInstiate()
    {
        return IsInstantiate;
    }

    public int GetTeamColor()
    {
        return TeamColor;
    }

    public int GetTeamNumber()
    {
        return TeamNumber;
    }
}
