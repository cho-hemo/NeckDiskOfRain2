using System.Collections.Generic;
using UnityEngine;
public static partial class Global
{
    ///<summary>요소가 정상적인지를 체크하는 함수</summary>
    public static bool IsValid<T>(this T target_)
    {

        return false;
    }
    #region Print log func
    public static void Log(object message)
    {
#if DEBUG_MODE
        Debug.Log(message);
#endif
    }
    #endregion
}