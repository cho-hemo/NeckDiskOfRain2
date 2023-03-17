using System.Collections.Generic;
using UnityEngine;
public static partial class Global
{
    #region Print log func
    public static void Log(object message)
    {
#if DEBUG_MODE
        Debug.Log(message);
#endif
    }
    #endregion
}