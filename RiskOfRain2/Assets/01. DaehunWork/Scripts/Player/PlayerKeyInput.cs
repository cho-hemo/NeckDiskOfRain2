using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKeyInput : MonoBehaviour
{
    [Header("캐릭터 입력 값")]

    [Tooltip("WASD,방향키,조이스틱 등 플레이어에 움직임을 입력 받을 Vector2값")]
    public Vector2 move;

    [Tooltip("마우스 커서,조이스틱 등 카메라가 바라볼 위치를 입력 받을 Vector2값")]
    public Vector2 look;

    [Tooltip("Player에 점프 키 입력 받을 Bool값")]
    public bool jump;

    [Tooltip("Player에 달리기 토글 키 입력 받을 Bool값")]
    public bool sprint;

    [Tooltip("Player에 마우스 좌클릭, 게임패드 트리거 키 입력을 받을 Bool값")]
    public bool shoot;

    [Header("마우스 커서 세팅")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
    ///<summary>WASD, 방향키, 조이스틱 입력 할 시 호출되는 함수</summary>
    ///<param name = "value">Vector2 값을 받음</param>
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    ///<summary>마우스 이동, 조이스틱 입력 할 시 호출되는 함수</summary>
    ///<param name = "value">Vector2 값을 받음</param>
    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }

    ///<summary>스페이스바, 게임패드에 South키를 입력 할 시 호출되는 함수</summary>
    ///<param name = "value">Bool 값을 받음</param>
    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnSprint(InputValue value)
    {
        SprintInput();
        Debug.Log($"OnSprint Debug : {value}");
    }

    public void OnShoot(InputValue value)
    {
        ShootInput(value.isPressed);
        Debug.Log($"OnShoot Debug : {value.isPressed}");
    }

#endif


    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void SprintInput()
    {
        sprint = !sprint;
    }

    public void ShootInput(bool newShootState)
    {
        shoot = newShootState;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}