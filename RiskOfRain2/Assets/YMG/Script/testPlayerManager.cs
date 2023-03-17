using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerManager : MonoBehaviour
{
    public static testPlayerManager instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject player;
}
