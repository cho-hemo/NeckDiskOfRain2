using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(testPlayerController))]
[RequireComponent(typeof(GunController))]
public class testPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;

    Camera viewCamera;
    testPlayerController controller;
    GunController gunController;

    void Start()
    {
        controller = GetComponent<testPlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
    }

    void Update()
    {
        // �̵� �Է�
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        // GetAxis (������ ��)  GetAxisRaw (������ x)
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        // �ٶ󺸴� ����
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);
        }

        // ���� ����
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }
}
