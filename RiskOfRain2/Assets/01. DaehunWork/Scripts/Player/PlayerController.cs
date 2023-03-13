using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRigidBody = default;
    private float _inputX = default;
    private float _inputY = default;
    public float Speed = default;

    private Vector3 _pos = default;

    private void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _inputX = Input.GetAxis("Horizontal") * Speed;
        _inputY = Input.GetAxis("Vertical") * Speed;
        _pos = new Vector3(_inputX, 0f, _inputY);


    }

    private void FixedUpdate()
    {
        _playerRigidBody.velocity = _pos;
    }
}
