using System.Collections.Generic;
using UnityEngine;
public static partial class Global
{
    #region Print log func
    public static void Log(object message)
    {
#if DEBUG_MODE
        // Debug.Log(message);      2023-03-21 / HyungJun / 디버그가 너무 찍혀서 잠시 주석
#endif
    }
    #endregion
}