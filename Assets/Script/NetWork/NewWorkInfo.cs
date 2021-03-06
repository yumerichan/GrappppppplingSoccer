using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWorkInfo : MonoBehaviour
{
    private int TeamColor;              //0 == 赤 1 == 青
    private int Number;
    private bool IsInstantiate;         //生成してよいか？

    // Start is called before the first frame update
    void Start()
    {
        TeamColor = -1;
        IsInstantiate = false;
    }  

    public void SetTeamColor(int color)
    {
        TeamColor = color;
    }

    public void SetInstiate(bool ins)
    {
        IsInstantiate = ins;
    }

    public void SetNumber(int number)
    {
        Number = number;
    }

    public bool GetInstiate()
    {
        return IsInstantiate;
    }

    public int GetTeamColor()
    {
        return TeamColor;
    }

    public int GetNumber()
    {
        return Number;
    }

}
