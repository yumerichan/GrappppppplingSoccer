using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingCommon
{
    //�~�߂����Ƃ��Ɏg��
    private static bool IsStop = false;

    public static bool GetIsStop()
    {
        return IsStop;
    }

    public static void SetIsStop(bool is_stop)
    {
        IsStop = is_stop;
    }
}
