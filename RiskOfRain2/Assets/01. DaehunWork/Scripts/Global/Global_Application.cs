using UnityEngine.SceneManagement;
using UnityEngine.Events;

public static partial class Global
{
    public static void AddOnSceneLoaded(UnityAction<Scene, LoadSceneMode> sceneLoaded)
    {
        SceneManager.sceneLoaded += sceneLoaded;
    }
}
