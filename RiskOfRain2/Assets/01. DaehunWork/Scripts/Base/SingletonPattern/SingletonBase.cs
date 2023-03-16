using UnityEngine;
using System;

public class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
{
    private static readonly Lazy<T> _instance = new Lazy<T>(() =>
    {
        T instance_ = FindObjectOfType(typeof(T)) as T;

        if (instance_ == null)
        {
            GameObject obj = new GameObject();
            instance_ = obj.AddComponent(typeof(T)) as T;
            DontDestroyOnLoad(obj);
        }
        else
        {
            DontDestroyOnLoad(instance_.gameObject);
        }

        return instance_;
    });

    public static T Instance
    {
        get
        {
            return _instance.Value;
        }
    }

    public void Awake()
    {
        Instance.gameObject.SetActive(true);
    }
}
