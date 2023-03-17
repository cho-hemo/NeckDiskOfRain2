using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float SensitivityX = default;        // 마우스 X축 감도 설정을 위한 값
    public float SensitivityY = default;        // 마우스 Y축 감도 설정을 위한 값

    public float MinimumY = default;            // 카메라에 Y축 회전이 정해진 범위 내에서만 이루어지도록 할 최소 값
    public float MaximumY = default;            // 카메라에 Y축 회전이 정해진 범위 내에서만 이루어지도록 할 최대 값

    private float _rotationX = default;
    private float _rotationY = default;

    private void Update()
    {
        InputMouseMove();
    }
    private void InputMouseMove()
    {
        _rotationX += Input.GetAxis("Mouse X") * SensitivityX;
        _rotationY += Input.GetAxis("Mouse Y") * SensitivityY;
        _rotationY = Mathf.Clamp(_rotationY, MinimumY, MaximumY);
        this.transform.localEulerAngles = new Vector3(-_rotationY, _rotationX, 0f);
    }
}
