using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TestPlayerController))]
[RequireComponent(typeof(GunController))]
public class TestPlayer : LivingThings // 이녀석이 이미 모노와 아이뎀 가지고 있음
{
    public float moveSpeed = 5f;

    Camera viewCamera;
    TestPlayerController controller;
    GunController gunController;

    public override void Start()
    {
        base.Start();
        controller = GetComponent<TestPlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
    }

    void Update()
    {
        // Movement input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        // Look input
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);
        }


        // Weapo input
        if (Input.GetMouseButton(0)) // 0 == 마우스 왼쪽 버튼
        {
            gunController.Shoot();
        }
    }
}
