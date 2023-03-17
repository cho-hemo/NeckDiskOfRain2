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
        // ï¿½Ìµï¿½ ï¿½Ô·ï¿½
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        // GetAxis (ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½)  GetAxisRaw (ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ x)
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        // ï¿½Ù¶óº¸´ï¿½ ï¿½ï¿½ï¿½ï¿½
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);
        }

<<<<<<< HEAD:RiskOfRain2/Assets/YMG/Script/testPlayer.cs
        // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
        if (Input.GetMouseButton(0))
=======
        // ¹«±â Á¶ÀÛ
        if (Input.GetMouseButton(0)) // 0 == ¸¶¿ì½º ¿ÞÂÊ ¹öÆ°
>>>>>>> 677080379c4cdb867b5feeb57c84cb9e234685b9:RiskOfRain2/Assets/YMG/Script/Player.cs
        {
            gunController.Shoot();
        }
    }
}
