using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerManager : MonoBehaviour
{
    public static TestPlayerManager instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject player;
}
