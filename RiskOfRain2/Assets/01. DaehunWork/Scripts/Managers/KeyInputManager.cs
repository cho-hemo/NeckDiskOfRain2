using UnityEngine;
using UnityEngine.InputSystem;

public class KeyInputManager : SingletonBase<KeyInputManager>
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

	[Tooltip("메인 스킬 입력을 받을 bool값")]
	public bool mainSkill;

	[Tooltip("서브 스킬 입력을 받을 bool값")]
	public bool SubSkill;

	[Tooltip("유틸리티 스킬 입력을 받을 bool값")]
	public bool utilitySkill;

	[Tooltip("특수 스킬 입력을 받을 bool값")]
	public bool specialSkill;

	[Tooltip("장비 사용 입력을 받을 bool값")]
	public bool useEquipment;

	[Tooltip("상호작용 입력을 받을 bool값")]
	public bool interaction;

	[Tooltip("정보화면 입력을 받을 bool값")]
	public bool informationScreen;

	[Tooltip("핑 입력을 받을 bool값")]
	public bool sendPing;

	[Tooltip("아날로그 움직임 설정")]
	public bool analogMovement;

	[Header("마우스 커서 세팅")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;

	[Space(5)]
	[Header("플레이어")]
	public Player player = default;

	[Space(5)]
	[Header("플레이어 UI Manager")]
	public PlayerUiManager playerUIManager = default;

#if ENABLE_INPUT_SYSTEM

	#region InputSystem
	///<summary>WASD, 방향키, Left Stick 입력 할 시 호출되는 함수</summary>
	///<param name = "value">Vector2 값을 받음</param>
	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
		player.Move(value.Get<Vector2>());
	}

	///<summary>마우스 이동, Right Stick 입력 할 시 호출되는 함수</summary>
	///<param name = "value">Vector2 값을 받음</param>
	public void OnLook(InputValue value)
	{
		if (cursorLocked)
		{
			LookInput(value.Get<Vector2>());
			player.Look(value.Get<Vector2>());
		}
		else
		{

		}
	}

	///<summary>스페이스바, 게임패드에 South키를 입력 할 시 호출되는 함수</summary>
	///<param name = "value">Bool 값을 받음</param>
	public void OnJump(InputValue value)
	{
		JumpInput(value.isPressed);
		player.Jump(value.isPressed);
	}
	///<summary>Ctrl, 게임패드에 Left Stick Press키를 입력 할 시 호출되는 함수</summary>
	///<param name = "value">Bool 값을 받음</param>
	public void OnSprint(InputValue value)
	{
		SprintInput();
		player.Sprint();
	}

	public void OnMainSkill(InputValue value)
	{
		MainSkillInput(value.isPressed);
		player.MainSkill(value.isPressed);
	}

	public void OnSubSkill(InputValue value)
	{
		SubSkillInput(value.isPressed);
		player.SubSkill(value.isPressed);
		// { 2023-03-20 / HyungJun / PlayerUIWorks
		playerUIManager.PlayerSkillActiveIcon(1, 5f);
	}

	// Shift skill
	public void OnUtilitySkill(InputValue value)
	{
		UtilitySkillInput(value.isPressed);
		player.UtilitySkill(value.isPressed);
		playerUIManager.PlayerSkillActiveIcon(2, 5f);
	}

	// R skill
	public void OnSpecialSkill(InputValue value)
	{
		SpecialSkillInput(value.isPressed);
		player.SpecialSkill(value.isPressed);
		playerUIManager.PlayerSkillActiveIcon(3, 5f);
	}

	// Q KeyInput
	public void OnUseEquipment(InputValue value)
	{
		UseEquipmentInput(value.isPressed);
		playerUIManager.PlayerSkillActiveIcon(4, 5f);
		// } 2023-03-20 / HyungJun / PlayerUIWorks
	}

	// E KeyInput
	public void OnInteraction(InputValue value)
	{
		InteractionInput(value.isPressed);
	}

	// Tap KeyInput
	public void OnInformationScreen(InputValue value)
	{
		InformationScreenInput(value.isPressed);
		//  { 2023-03-22 / Daehun / KeyInput Works
		if (value.isPressed)
		{
			SetCursorState(!value.isPressed);
		}
		else
		{
			SetCursorState(!value.isPressed);
		}
		cursorLocked = !value.isPressed;
		//  } 2023-03-22 / Daehun / KeyInput Works
	}
	public void OnSendPing(InputValue value)
	{
		SendPingInput(value.isPressed);
	}

	//  { 2023-03-22 / Daehun / KeyInput Works
	public void OnEsc(InputValue value)
	{
		cursorLocked = !cursorLocked;
	}
	//  } 2023-03-22 / Daehun / KeyInput Works
	#endregion
#endif

	public void Start()
	{
		GameObject.Find("Player").TryGetComponent(out player);
		SetCursorState(cursorLocked);
		//GameObject.Find("PlayerUIManager").TryGetComponent(out playerUIManager);
		GioleFunc.GetRootObj("PlayerUiManager").TryGetComponent(out playerUIManager);       // 2023-03-21 / HyungJun / 릴리즈 버전에서 주석 해제 필요
	}

	///<summary>움직임에 관련된 입력을 받는 함수</summary>
	///<param name = "newMoveDirection">Vector2 값 -1 ~ 1 까지의 값이 들어옴</param>
	public void MoveInput(Vector2 newMoveDirection_)
	{
		move = newMoveDirection_;
	}

	///<summary>화면을 회전 시켜주는 함수</summary>
	///<param name = "newLookDirection">마우스 좌표, 게임패드 기울기를 입력 받음</param>
	public void LookInput(Vector2 newLookDirection_)
	{
		look = newLookDirection_;
	}

	///<summary>점프키 입력을 받는 함수</summary>
	///<param name = "newJumpState">키를 눌렀을 시 true 땟을 시 false가 들어옴</param>
	public void JumpInput(bool newJumpState_)
	{
		jump = newJumpState_;
	}

	///<summary>달리기 토글 입력을 받는 함수</summary>
	public void SprintInput()
	{
		sprint = !sprint;
	}
	public void MainSkillInput(bool newMainSkillInput_)
	{
		mainSkill = newMainSkillInput_;
	}
	public void SubSkillInput(bool newSubSkillInput_)
	{
		SubSkill = newSubSkillInput_;
	}
	public void UtilitySkillInput(bool newUtilitySkillInput_)
	{
		utilitySkill = newUtilitySkillInput_;

	}
	public void SpecialSkillInput(bool newSpecialSkillInput_)
	{
		specialSkill = newSpecialSkillInput_;
	}
	public void UseEquipmentInput(bool newUseEquipmentInput_)
	{
		useEquipment = newUseEquipmentInput_;
	}
	public void InteractionInput(bool newInteractionInput_)
	{
		interaction = newInteractionInput_;
	}
	public void InformationScreenInput(bool newInformationScreenInput_)
	{
		informationScreen = newInformationScreenInput_;
	}
	public void SendPingInput(bool newSendPingInput_)
	{
		sendPing = newSendPingInput_;
	}

	///<summary>화면 밖으로 마우스가 못 나가게 하는 함수</summary>
	///<param name = "newState">true가 들어오면 화면 밖으로 마우스가 나가지 않고 false가 들어오면 밖으로 나갈 수 있음</param>
	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}
}