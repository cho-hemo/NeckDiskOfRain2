using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    public Transform PlayerTransform { get; private set; }

    private new void Awake()
    {
        base.Awake();
        Global.AddOnSceneLoaded(OnSceneLoaded);
        PlayerTransform = Global.FindRootObject("Player").transform;
    }
    private void Start()
    {

    }

    public void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "":
                break;
            default:
                break;
        }
    }
}
