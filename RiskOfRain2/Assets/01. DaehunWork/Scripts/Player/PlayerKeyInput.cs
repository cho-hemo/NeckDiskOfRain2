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

    [Tooltip("")]
    public bool mainSkill;

    [Tooltip("아날로그 움직임 설정")]
    public bool analogMovement;

    [Header("마우스 커서 세팅")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
    ///<summary>WASD, 방향키, Left Stick 입력 할 시 호출되는 함수</summary>
    ///<param name = "value">Vector2 값을 받음</param>
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    ///<summary>마우스 이동, Right Stick 입력 할 시 호출되는 함수</summary>
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

    ///<summary>Ctrl, 게임패드에 Left Stick Press키를 입력 할 시 호출되는 함수</summary>
    ///<param name = "value">Bool 값을 받음</param>
    public void OnSprint(InputValue value)
    {
        SprintInput();
        Debug.Log($"OnSprint Debug : {value}");
    }

    public void OnMainSkill(InputValue value)
    {

    }

    public void OnSubSkill(InputValue value)
    {

    }

    public void OnUtilitySkill(InputValue value)
    {

    }
    public void OnSpecialSkill(InputValue value)
    {

    }
    public void OnUseEquipment(InputValue value)
    {

    }
    public void OnInteraction(InputValue value)
    {

    }
    public void OnInformationScreen(InputValue value)
    {

    }
    public void OnSendPing(InputValue value)
    {

    }




#endif

    ///<summary>움직임에 관련된 입력을 받는 함수</summary>
    ///<param name = "newMoveDirection">Vector2 값 -1 ~ 1 까지의 값이 들어옴</param>
    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    ///<summary>화면을 회전 시켜주는 함수</summary>
    ///<param name = "newLookDirection">마우스 좌표, 게임패드 기울기를 입력 받음</param>
    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    ///<summary>점프키 입력을 받는 함수</summary>
    ///<param name = "newJumpState">키를 눌렀을 시 true 땟을 시 false가 들어옴</param>
    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    ///<summary>달리기 토글 입력을 받는 함수</summary>
    public void SprintInput()
    {
        sprint = !sprint;
    }
    public void MainSkillInput()
    {

    }
    public void SubSkillInput()
    {

    }
    public void UtilitySkillInput()
    {

    }
    public void SpecialSkillInput()
    {

    }
    public void UseEquipmentInput()
    {

    }
    public void InteractionInput()
    {
    }
    public void InformationScreenInput()
    {

    }
    public void SendPingInput()
    {

    }




    ///<summary>화면 밖으로 마우스가 못 나가게 하는 함수</summary>
    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    ///<summary>화면 밖으로 마우스가 못 나가게 하는 함수</summary>
    ///<param name = "newState">true가 들어오면 화면 밖으로 마우스가 나가지 않고 false가 들어오면 밖으로 나갈 수 있음</param>
    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}