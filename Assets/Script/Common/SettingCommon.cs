using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingCommon
{
    //止めたいときに使う
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
