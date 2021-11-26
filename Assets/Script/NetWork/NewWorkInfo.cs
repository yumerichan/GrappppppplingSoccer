using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NewWorkInfo : MonoBehaviour
{
    private int TeamColor;              //0 == 赤 1 == 青
    private int TeamNumber;             //チーム番号

    private bool IsInstantiate;         //生成してよいか？

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
