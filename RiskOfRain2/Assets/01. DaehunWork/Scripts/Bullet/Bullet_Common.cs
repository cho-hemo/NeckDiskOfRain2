using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Common : MonoBehaviour
{
    private Rigidbody _bulletRigidbody = default;
    private float _speed = 100f;
    void Start()
    {
        TryGetComponent(out _bulletRigidbody);
    }

    private void OnEnable()
    {

    }

    private void FixedUpdate()
    {
        _bulletRigidbody.velocity = transform.forward * _speed;
    }
}