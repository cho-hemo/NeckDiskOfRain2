using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using RiskOfRain2.Player;
namespace RiskOfRain2.Manager
{
	public class KeyInputManager : SingletonBase<KeyInputManager>
	{
		[Tooltip("아날로그 움직임 설정")]
		public bool analogMovement;

		[Header("마우스 커서 세팅")]
		public bool cursorLocked = true;

		[Space(5)]
		[Header("플레이어")]
		public PlayerController playerController = default;

		public List<float> skillCooltimes = default;

		[Space(5)]
		[Header("플레이어 UI Manager")]
		public PlayerUiManager playerUIManager = default;

#if ENABLE_INPUT_SYSTEM

		#region InputSystem
		///<summary>WASD, 방향키, Left Stick 입력 할 시 호출되는 함수</summary>
		///<param name = "value">Vector2 값을 받음</param>
		public void OnMove(InputValue value)
		{
			if (!IsValidCheck()) return;
			playerController.MoveInput(value.Get<Vector2>());
		}

		///<summary>마우스 이동, Right Stick 입력 할 시 호출되는 함수</summary>
		///<param name = "value">Vector2 값을 받음</param>
		public void OnLook(InputValue value)
		{
			if (!IsValidCheck()) return;
			if (cursorLocked)
			{
				playerController.LookInput(value.Get<Vector2>());
			}
			else
			{
				playerController.LookInput(Vector2.zero);
			}
		}

		///<summary>스페이스바, 게임패드에 South키를 입력 할 시 호출되는 함수</summary>
		///<param name = "value">Bool 값을 받음</param>
		public void OnJump(InputValue value)
		{
			if (!IsValidCheck()) return;
			playerController.JumpInput(value.isPressed);
		}
		///<summary>Ctrl, 게임패드에 Left Stick Press키를 입력 할 시 호출되는 함수</summary>
		///<param name = "value">Bool 값을 받음</param>
		public void OnSprint(InputValue value)
		{
			if (!IsValidCheck()) return;
			playerController.SprintInput();
		}

		public void OnMainSkill(InputValue value)
		{
			if (!IsValidCheck()) return;
			playerController.MainSkillInput(value.isPressed);
		}

		public void OnSubSkill(InputValue value)
		{
			if (!IsValidCheck()) return;
			playerController.SubSkillInput(value.isPressed);
			// { 2023-03-20 / HyungJun / PlayerUIWorks
			int index_ = PlayerDefine.PLAYER_SUB_SKILL_INDEX;
			Debug.Log($"cooltime : {skillCooltimes[index_]}");
<<<<<<< HEAD
			// playerUIManager.PlayerSkillActiveIcon(index_, skillCooltimes[index_]);		// 2023-03-30 / HyungJun / 릴리즈 버전 주석 해제 필요
=======
			//playerUIManager.PlayerSkillActiveIcon(index_, skillCooltimes[index_]);
>>>>>>> 078ca4d0ab069bf1106702c689bf1009e722942a
		}

		// Shift skill
		public void OnUtilitySkill(InputValue value)
		{
			if (!IsValidCheck()) return;
			playerController.UtilitySkillInput(value.isPressed);
			int index_ = PlayerDefine.PLAYER_UTILITY_SKILL_INDEX;
			// playerUIManager.PlayerSkillActiveIcon(index_, skillCooltimes[index_]);		// 2023-03-30 / HyungJun / 릴리즈 버전 주석 해제 필요
		}

		// R skill
		public void OnSpecialSkill(InputValue value)
		{
			if (!IsValidCheck()) return;
			playerController.SpecialSkillInput(value.isPressed);
			int index_ = PlayerDefine.PLAYER_SPECIAL_SKILL_INDEX;
<<<<<<< HEAD
			// playerUIManager.PlayerSkillActiveIcon(index_, skillCooltimes[index_]);		// 2023-03-30 / HyungJun / 릴리즈 버전 주석 해제 필요
=======
			//playerUIManager.PlayerSkillActiveIcon(index_, skillCooltimes[index_]);
>>>>>>> 078ca4d0ab069bf1106702c689bf1009e722942a
		}

		// Q KeyInput
		public void OnUseEquipment(InputValue value)
		{
			if (!IsValidCheck()) return;
			// playerUIManager.PlayerSkillActiveIcon(4, 5f);
			// } 2023-03-20 / HyungJun / PlayerUIWorks
		}
		// E KeyInput
		public void OnInteraction(InputValue value)
		{
			if (!IsValidCheck()) return;
		}

		// Tap KeyInput
		public void OnInformationScreen(InputValue value)
		{
			if (!IsValidCheck()) return;
			//  { 2023-03-22 / Daehun / KeyInput Works
			if (value.isPressed)
			{
				SetCursorState(!value.isPressed);
			}
			else
			{
				SetCursorState(!value.isPressed);
			}
			//  } 2023-03-22 / Daehun / KeyInput Works
		}

		public void OnSendPing(InputValue value)
		{
			if (!IsValidCheck()) return;
		}

		//  { 2023-03-22 / Daehun / KeyInput Works
		public void OnEsc(InputValue value)
		{
			if (!IsValidCheck()) return;
			cursorLocked = !cursorLocked;
			SetCursorState(cursorLocked);
		}
		//  } 2023-03-22 / Daehun / KeyInput Works
		#endregion
#endif

		public void Start()
		{
			//SetCursorState(cursorLocked);
			//GameObject.Find("PlayerUIManager").TryGetComponent(out playerUIManager);
			//GioleFunc.GetRootObj("PlayerUiManager").TryGetComponent(out playerUIManager);       // 2023-03-21 / HyungJun / 릴리즈 버전에서 주석 해제 필요
			// skillCooltimes = new List<float>();
			// int count_ = GameManager.Instance.Skills.Count;
			// for (int i = 0; i < count_; i++)
			// {
			// 	skillCooltimes.Add(GameManager.Instance.Skills[i].SkillCooltime);
			// }
			Global.AddOnSceneLoaded(OnSceneLoaded);
		}

		public void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
		{
			switch (scene.name)
			{
				case Global.PLAY_SCENE_NAME:
					SetCursorState(cursorLocked);
					break;
				default:
					break;
			}
		}

		public void SetPlayerController(PlayerController playerController)
		{
			this.playerController = playerController;
		}

		public void SkillChanged(int index)
		{
			skillCooltimes[index] = GameManager.Instance.Skills[index].SkillCooltime;
		}

		///<summary>화면 밖으로 마우스가 못 나가게 하는 함수</summary>
		///<param name = "newState">true가 들어오면 화면 밖으로 마우스가 나가지 않고 false가 들어오면 밖으로 나갈 수 있음</param>
		public void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
			cursorLocked = newState;
		}

		public bool IsValidCheck()
		{
			if (playerController == default || playerController == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}