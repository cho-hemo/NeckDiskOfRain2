using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
using RiskOfRain2.Manager;
using RiskOfRain2.Item;

namespace RiskOfRain2.Player
{
	public abstract class PlayerBase : MonoBehaviour, IPlayerSkill, ISubject
	{
		#region Inspector
		[Header("Player Controller")]
		[SerializeField]
		[Tooltip("Main Camera")]
		protected Camera mainCamera;

		[SerializeField]
		[Tooltip("Ray 사거리")]
		protected float rayRange = float.MaxValue;

		[SerializeField]
		[Tooltip("카메라 윗 방향 최대 각도")]
		protected float topClamp;

		[SerializeField]
		[Tooltip("카메라 아랫 방향 최대 각도")]
		protected float bottomClamp;

		[SerializeField]
		[Tooltip("Player Move 입력")]
		protected Vector2 inputMove;

		[SerializeField]
		[Tooltip("Player Look Input")]
		protected Vector2 inputLook;

		[SerializeField]
		[Tooltip("가속 감속")]
		protected float speedChangeRate = 10.0f;

		[SerializeField]
		[Tooltip("타겟 회전")]
		protected float targetRotation = 0.0f;

		[SerializeField]
		[Tooltip("캐릭터 회전 속도")]
		[Range(0.0f, 0.3f)]
		protected float rotationSmoothTime = 0.12f;

		[SerializeField]
		[Tooltip("Player 회전 가속도")]
		protected float rotationVelocity;

		[SerializeField]
		[Tooltip("Player 추락 가속도")]
		protected float verticalVelocity = 0;

		[SerializeField]
		[Tooltip("Ground와 거리")]
		protected float groundedOffset = -0.14f;

		[SerializeField]
		[Tooltip("Ground 체크를 위한 반지름 CharacterController와 일치해야함")]
		protected float groundedRadius = 0.3f;

		[SerializeField]
		[Tooltip("캐릭터가 밟을 수 있는 Ground의 Layer")]
		protected LayerMask groundLayers;

		[SerializeField]
		[Tooltip("낙하 상태 진입까지 소요되는 시간")]
		protected float fallTimeout = 0.15f;

		[SerializeField]
		[Tooltip("다시 점프를 하기 위한 딜레이 시간")]
		protected float jumpTimeout = 0.1f;

		[SerializeField]
		[Tooltip("추락 TimeOut DeltaTime")]
		protected float fallTimeoutDelta;

		[SerializeField]
		[Tooltip("점프 TimeOut DeltaTime")]
		protected float jumpTimeoutDelta;

		[Space(5)]
		[Header("Player Stat")]

		[SerializeField]
		[Tooltip("Player 이름")]
		protected string playerName;
		[SerializeField]
		[Tooltip("Player 정보")]
		protected string playerInfo;

		[SerializeField]
		[Tooltip("레벨")]
		protected int level = 1;

		[SerializeField]
		[Tooltip("현재 경험치")]
		protected int currentExp;

		[SerializeField]
		[Tooltip("최대 경험치")]
		protected int maxExp = 10;

		[SerializeField]
		[Tooltip("최대 체력")]
		protected float maxHp;

		[SerializeField]
		[Tooltip("현재 체력")]
		protected float currentHp;

		[SerializeField]
		[Tooltip("방어막")]
		protected float defense;

		[SerializeField]
		[Tooltip("공격력")]
		protected float attackDamage;

		[SerializeField]
		[Tooltip("공격 속도")]
		protected float attackSpeed;

		[SerializeField]
		[Tooltip("스킬")]
		protected List<Skill> skills = default;

		[SerializeField]
		[Tooltip("체력 회복")]
		protected float healthRegen;

		[SerializeField]
		[Tooltip("현재 이동속도")]
		protected float currentSpeed = 0f;

		[SerializeField]
		[Tooltip("기본 걷기 속도")]
		protected float defaultWalkSpeed = 7.0f;

		[SerializeField]
		[Tooltip("현재 걷기 속도")]
		protected float currentWalkSpeed = 7.0f;

		[SerializeField]
		[Tooltip("기본 질주 속도")]
		protected float defaultSprintSpeed = 8.75f;

		[SerializeField]
		[Tooltip("현재 질주 속도")]
		protected float currentSprintSpeed = 8.75f;

		[SerializeField]
		[Tooltip("최대 점프 카운트")]
		protected int maxJumpCount = 1;

		[SerializeField]
		[Tooltip("현재 점프 횟수")]
		protected int currentJumpCount = default;

		[SerializeField]
		[Tooltip("점프력")]
		protected float jumpHeight = 5f;

		[SerializeField]
		[Tooltip("중력")]
		protected float gravity = -9.81f;

		[SerializeField]
		[Tooltip("걷기")]
		protected bool isMove;

		[SerializeField]
		[Tooltip("달리기")]
		protected bool isSprint;

		[SerializeField]
		[Tooltip("사격 체크")]
		protected bool isShot;

		[SerializeField]
		[Tooltip("바닥 체크")]
		protected bool isGrounded = true;

		[SerializeField]
		[Tooltip("사망 체크")]
		protected bool isDead = false;

		[SerializeField]
		[Tooltip("스킬 사용 가능 체크")]
		protected bool isSkillAvailable;

		[SerializeField]
		[Tooltip("OSP")]
		protected bool osp = false;

		[Space(5)]
		[Header("플레이어 오브젝트")]

		[SerializeField]
		[Tooltip("총알이 발사될 위치")]
		protected List<Transform> focusPoint;

		[SerializeField]
		[Tooltip("플레이어 타입(종류)")]
		protected PlayerType playerType = PlayerType.NONE;

		[SerializeField]
		[Tooltip("상태머신")]
		protected StateMachine stateMachine = default;

		[SerializeField]
		[Tooltip("애니메이터 컨트롤러")]
		protected Animator playerAnimator;

		[SerializeField]
		[Tooltip("Charcter Controller")]
		protected CharacterController characterController;

		[Space(5)]
		[Header("CineMachine")]
		[SerializeField]
		[Tooltip("CameraTarget")]
		protected Transform cinemachineCameraTarget;
		[SerializeField]
		protected float cinemachineTargetYaw;
		[SerializeField]
		protected float cinemachineTargetPitch;
		#endregion

		public LayerMask layerMask;

		#region Property
		#region PlayerController
		// { PlayerController Property
		public Camera MainCamera { get { return mainCamera; } protected set { mainCamera = value; } }
		public float RayRange { get { return rayRange; } protected set { rayRange = value; } }
		public float TopClamp { get { return topClamp; } protected set { topClamp = value; } }
		public float BottomClamp { get { return bottomClamp; } protected set { bottomClamp = value; } }
		public Vector2 InputMove { get { return inputMove; } protected set { inputMove = value; } }
		public Vector2 InputLook { get { return inputLook; } protected set { inputLook = value; } }
		public float SpeedChangeRate { get { return speedChangeRate; } protected set { speedChangeRate = value; } }
		public float TargetRotation { get { return targetRotation; } protected set { targetRotation = value; } }
		public float RotationSmoothTime { get { return rotationSmoothTime; } protected set { rotationSmoothTime = value; } }
		public float VerticalVelocity { get { return verticalVelocity; } protected set { verticalVelocity = value; } }
		public float RotationVelocity { get { return rotationVelocity; } protected set { rotationVelocity = value; } }
		public float GroundedOffset { get { return groundedOffset; } protected set { groundedOffset = value; } }
		public float GroundedRadius { get { return groundedOffset; } protected set { groundedOffset = value; } }
		public LayerMask GroundLayers { get { return groundLayers; } protected set { groundLayers = value; } }
		public float FallTimeOut { get { return fallTimeout; } protected set { fallTimeout = value; } }
		public float JumpTimeOut { get { return jumpTimeout; } protected set { jumpTimeout = value; } }
		public float FallTimeoutDelta { get { return fallTimeoutDelta; } protected set { fallTimeoutDelta = value; } }
		public float JumpTimeoutDelta { get { return jumpTimeoutDelta; } protected set { jumpTimeoutDelta = value; } }
		// } PlayerController Property
		#endregion

		#region Player Stat
		public string PlayerName { get { return playerName; } protected set { playerName = value; } }
		public string PlayerInfo { get { return playerInfo; } protected set { playerInfo = value; } }
		public int Level { get { return level; } protected set { level = value; } }
		public int CurrentExp { get { return currentExp; } protected set { currentExp = value; } }
		public int MaxExp { get { return maxExp; } protected set { maxExp = value; } }
		public float MaxHp { get { return maxHp; } protected set { maxHp = value; } }
		public float CurrentHp { get { return currentHp; } protected set { currentHp = value; } }
		public float Defense { get { return defense; } protected set { defense = value; } }
		public float AttackDamage { get { return attackDamage; } protected set { attackDamage = value; } }
		public float AttackSpeed { get { return attackSpeed; } protected set { attackSpeed = value; } }
		public List<Skill> Skills { get { return skills; } protected set { skills = value; } }
		public float HealthRegen { get { return healthRegen; } protected set { healthRegen = value; } }
		public float CurrentSpeed { get { return currentSpeed; } protected set { currentSpeed = value; } }
		public float DefaultWalkSpeed { get { return defaultWalkSpeed; } protected set { defaultWalkSpeed = value; } }
		public float CurrentWalkSpeed { get { return currentWalkSpeed; } protected set { currentWalkSpeed = value; } }
		public float DefaultSprintSpeed { get { return defaultSprintSpeed; } protected set { defaultSprintSpeed = value; } }
		public float CurrentSprintSpeed { get { return currentSprintSpeed; } protected set { currentSprintSpeed = value; } }
		public int MaxJumpCount { get { return maxJumpCount; } protected set { maxJumpCount = value; } }
		public int CurrentJumpCount { get { return currentJumpCount; } protected set { currentJumpCount = value; } }
		public float JumpHeight { get { return jumpHeight; } protected set { jumpHeight = value; } }
		public float Gravity { get { return gravity; } protected set { gravity = value; } }
		public bool IsMove { get { return isMove; } protected set { isMove = value; } }
		public bool IsSprint { get { return isSprint; } protected set { isSprint = value; } }
		public bool IsShot { get { return isShot; } protected set { isShot = value; } }
		public bool IsGrounded { get { return isGrounded; } protected set { isGrounded = value; } }
		public bool IsDead { get { return isDead; } protected set { isDead = value; } }
		public bool IsSkillAvailable { get { return isSkillAvailable; } protected set { isSkillAvailable = value; } }
		public bool Osp { get { return osp; } protected set { osp = value; } }
		#endregion

		#region Player Object
		public List<Transform> FocusPoint { get { return focusPoint; } protected set { focusPoint = value; } }
		public PlayerType PlayerType { get { return playerType; } protected set { playerType = value; } }
		public StateMachine StateMachine { get { return stateMachine; } protected set { stateMachine = value; } }
		public Animator PlayerAnimator { get { return playerAnimator; } protected set { playerAnimator = value; } }
		public CharacterController CharacterController { get { return characterController; } protected set { characterController = value; } }
		public Transform CinemachineCameraTarget { get { return cinemachineCameraTarget; } protected set { cinemachineCameraTarget = value; } }
		public float CinemachineTargetYaw { get { return cinemachineTargetYaw; } protected set { cinemachineTargetYaw = value; } }
		public float CinemachineTargetPitch { get { return cinemachineTargetPitch; } protected set { cinemachineTargetPitch = value; } }
		#endregion

		#endregion

		public void SetSkillAvailable(bool value)
		{
			IsSkillAvailable = value;
		}

		public void SetIsSprint(bool value)
		{
			IsSprint = value;
		}

		// #if ENABLE_INPUT_SYSTEM
		//     protected PlayerInput _playerInput;
		//     protected PlayerKeyInput _input;

		//     public PlayerInput PlayerInput { get { return _playerInput; } protected set { _playerInput = value; } }
		//     public PlayerKeyInput Input { get { return _input; } protected set { _input = value; } }
		// #endif

		protected void Start()
		{
			Global.FindRootObject("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().Follow = CinemachineCameraTarget.transform;
			TryGetComponent(out playerAnimator);
			PlayerAnimator.SetBool("IsGrounded", IsGrounded);
			TryGetComponent(out characterController);
			CinemachineTargetYaw = CinemachineCameraTarget.rotation.eulerAngles.y;

			StateMachine = new StateMachine();
			SetState(new Player_IdleState(this));

			MainCamera = Camera.main;

			StartCoroutine(HealthRegeneration());
		}

		protected void Update()
		{
			GroundedCheck();
			JumpAndGravity();
			UpdateState();
		}

		protected void LateUpdate()
		{
			if (IsShot)
			{
				PlayerShootRotation();
			}
			CameraRotation();
		}

		public void TakeDamage(float damage_)
		{
			if (Osp) return;
			float maxDamage_ = (MaxHp + Defense) * 0.9f;
			if (maxDamage_ < damage_)
			{
				if (CurrentHp - maxDamage_ <= 0)
				{
					CurrentHp = 0;
					IsDead = true;
					SetState(new Player_DeadState(this));
				}
				else
				{
					CurrentHp -= maxDamage_;
					StartCoroutine(OneShotProtection());
				}
			}
			else
			{
				if (CurrentHp - damage_ <= 0)
				{
					CurrentHp = 0;
					IsDead = true;
					SetState(new Player_DeadState(this));
				}
				else
				{
					CurrentHp -= damage_;
				}
			}
			NotifyObservers();
		}

		/// <summary>
		/// 스탯 증가
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		public void IncreaseStat(PlayerStat target, float value)
		{
			switch (target)
			{
				case PlayerStat.MAX_HP:
					MaxHp += value;
					break;
				case PlayerStat.CURRENT_HP:
					CurrentHp += value;
					break;
				case PlayerStat.DEFENSE:
					Defense += value;
					break;
				case PlayerStat.ATTACK_DAMAGE:
					AttackDamage += value;
					break;
				case PlayerStat.ATTACK_SPEED:
					AttackSpeed += value;
					break;
				case PlayerStat.WALK_SPEED:
					CurrentWalkSpeed += value;
					break;
				case PlayerStat.SPRINT_SPEED:
					CurrentSprintSpeed += value;
					break;
				case PlayerStat.JUMP_COUNT:
					MaxJumpCount += (int)value;
					break;
				case PlayerStat.JUMP_HEIGHT:
					JumpHeight += value;
					break;

			}
		}

		/// <summary>
		/// Exp 증가 함수
		/// </summary>
		private void IncreaseExp()
		{
			if (MaxExp <= CurrentExp + 1)
			{
				Level += 1;
				CurrentExp = 0;
				MaxExp = Level * 10;
				LevelUp();
			}
			else
			{
				CurrentExp += 1;
			}
			NotifyObservers();
		}

		/// <summary>
		/// 레벨 업 함수
		/// </summary>
		protected abstract void LevelUp();

		protected IEnumerator HealthRegeneration()
		{
			while (true)
			{
				yield return new WaitForSeconds(1f);
				if (MaxHp <= CurrentHp + HealthRegen)
				{
					CurrentHp = MaxHp;
				}
				else
				{
					CurrentHp += HealthRegen;
				}
				NotifyObservers();
			}
		}

		/// <summary>
		/// 스킬 동작 함수
		/// </summary>
		/// <param name="index"></param>
		/// <param name="isPressed"></param>
		protected void SkillAction(int index, bool isPressed)
		{
			Skill skill_ = Skills[index];
			skill_.Action(isPressed);
			StartCoroutine(skill_.SkillCoolTimeRunning(true));
		}

		/// <summary>
		/// 스킬 사용 가능 체크 함수
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		protected bool SkillAvailableCheck(int index)
		{
			return Skills[index].SkillAvailableCheck() & IsSkillAvailable;
		}

		/// <summary>
		/// 일정 이상의 피해를 무시하는 즉사 보호 시스템
		/// </summary>
		/// <returns></returns>
		protected IEnumerator OneShotProtection()
		{
			Osp = true;
			yield return new WaitForSeconds(0.1f);
			Osp = false;
		}

		#region StateMachine Caching
		public void UpdateState()
		{
			// Debug.Log($"Current State : {StateMachine.GetState().ToString()}");
			StateMachine.UpdateState();
		}
		public void SetState(IState state)
		{
			StateMachine.SetState(state);
		}
		public IState GetState()
		{
			return StateMachine.GetState();
		}
		public void ChangeState()
		{
			StateMachine.ChangeState();
		}
		public void AnimationChange()
		{
			StateMachine.AnimationChange();
		}
		public void Action()
		{
			StateMachine.Action();
		}
		#endregion

		#region Animator Caching
		public AnimatorStateInfo GetCurrentAnimatorStateInfo(int layer)
		{
			return PlayerAnimator.GetCurrentAnimatorStateInfo(layer);
		}
		public void SetTrigger(string param)
		{
			PlayerAnimator.SetTrigger(param);
		}

		public void SetBool(string param, bool value)
		{
			PlayerAnimator.SetBool(param, value);
		}

		public void SetFloat(string param, float value)
		{
			PlayerAnimator.SetFloat(param, value);
		}

		public void SetInteger(string param, int value)
		{
			PlayerAnimator.SetInteger(param, value);
		}
		#endregion

		#region Player Controll
		public void Move(Vector2 value)
		{
			if (value == Vector2.zero)
			{
				IsMove = false;
				IsSprint = false;
			}
			else
			{
				IsMove = true;
			}

			InputMove = value;

			// 현재 RollState에서는 못 움직이게끔 return을 진행하고 있는데 다음과 같은 조건으로 수행 시 캐릭터가 여러개가 될 경우 캐릭터마다 움직이지 못 하는 State를 추가 작성을 해줘야 함
			// 이는 OOP에 어긋나는 부분으로 못 움직이는 상태일때 bool변수를 지정해주고 해당 변수를 변경시켜서 각 캐릭터마다 못 움직이는 상태에서 해당 변수에 값을 변경하는 형식으로 변경 예정
			switch (StateMachine.GetState())
			{
				case Commando.TacticalDiveState:
					return;
			}

			if (IsGrounded)
			{
				if (!IsSprint || InputMove.y <= 0 || IsShot)
				{
					//Debug.Log($"Walk : {InputMove}");
					IsSprint = false;
					SetState(new Player_WalkState(this));
				}
				else
				{
					//Debug.Log($"Sprint : {InputMove}");
					SetState(new Player_SprintState(this));
				}
			}
		}

		public void Move()
		{
			float targetSpeed = IsSprint ? CurrentSprintSpeed : CurrentWalkSpeed;

			if (InputMove == Vector2.zero) targetSpeed = 0.0f;

			float currentHorizontalSpeed = new Vector3(CharacterController.velocity.x, 0.0f, CharacterController.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = KeyInputManager.Instance.analogMovement ? InputMove.magnitude : 1f;

			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				CurrentSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				CurrentSpeed = Mathf.Round(CurrentSpeed * 1000f) / 1000f;
			}
			else
			{
				CurrentSpeed = targetSpeed;
			}

			if (!IsShot)
			{
				PlayerRotation();
			}

			//Vector3 targetDirection = Quaternion.Euler(0.0f, TargetRotation, 0.0f) * Vector3.forward;	
			//CharacterController.Move(targetDirection.normalized * (CurrentSpeed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
			Vector3 Input_ = new Vector3(InputMove.x, 0, InputMove.y);
			Vector3 targetDirection_ = (Quaternion.Euler(0, MainCamera.transform.eulerAngles.y, 0) * Input_).normalized;
			CharacterController.Move(targetDirection_ * (CurrentSpeed * Time.deltaTime) + new Vector3(0.0f, VerticalVelocity, 0.0f) * Time.deltaTime);
		}

		public void PlayerRotation()
		{
			Vector2 inputDirection_ = InputMove.normalized;

			TargetRotation = Mathf.Atan2(inputDirection_.x, inputDirection_.y) * Mathf.Rad2Deg + MainCamera.transform.eulerAngles.y;

			float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetRotation, ref rotationVelocity, RotationSmoothTime);

			transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
		}

		public void PlayerShootRotation()
		{
			TargetRotation = MainCamera.transform.eulerAngles.y;
			transform.rotation = Quaternion.Euler(0.0f, TargetRotation, 0.0f);
		}

		public void Look(Vector2 value)
		{
			if (InputLook != value)
			{
				InputLook = value;
				float aimX_ = default;
				float aimY_ = default;

				float angleX_ = CinemachineCameraTarget.localEulerAngles.x;
				if (180 <= angleX_)
				{
					aimY_ = (angleX_ - 270) / 180f;
				}
				else
				{
					aimY_ = (angleX_ + 90) / 180f;
				}

				float angleY_ = CinemachineCameraTarget.localEulerAngles.y;
				if (180 <= angleY_)
				{
					aimX_ = (angleY_ - 270) / 180f;
				}
				else
				{
					aimX_ = (angleY_ + 90) / 180f;
				}

				PlayerAnimator.CrossFade("Aim_Horizontal", 0f, 1, aimX_);
				PlayerAnimator.CrossFade("Aim_Vertical", 0f, 2, aimY_);
			}
		}

		private void CameraRotation()
		{
			if (InputLook.sqrMagnitude >= 0.01f)
			{
				CinemachineTargetYaw += InputLook.x * 1.0f;
				CinemachineTargetPitch += InputLook.y * 1.0f;
			}

			CinemachineTargetYaw = ClampAngle(CinemachineTargetYaw, float.MinValue, float.MaxValue);
			CinemachineTargetPitch = ClampAngle(CinemachineTargetPitch, BottomClamp, TopClamp);

			CinemachineCameraTarget.rotation = Quaternion.Euler(CinemachineTargetPitch, CinemachineTargetYaw, 0.0f);
		}

		private float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		public void Jump(bool isPressed)
		{
			if (isPressed && IsGrounded && CurrentJumpCount < MaxJumpCount)
			{
				SetState(new Player_JumpState(this));
				VerticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
				CurrentJumpCount++;
			}
		}

		public void Sprint()
		{
			IsSprint = !IsSprint;
		}

		public void GroundedCheck()
		{
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			IsGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
		}

		private void JumpAndGravity()
		{
			if (IsGrounded)
			{
				SetBool("IsGrounded", IsGrounded);

				FallTimeoutDelta = FallTimeOut;
				CurrentJumpCount = 0;
				//_animator.SetBool(_animIDFreeFall, false);


				// stop our velocity dropping infinitely when grounded
				if (VerticalVelocity < 0.0f)
				{
					VerticalVelocity = -2f;
				}

				// // Jump
				// if (_input.jump && _jumpTimeoutDelta <= 0.0f)
				// {
				//     // the square root of H * -2 * G = how much velocity needed to reach desired height
				//     _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

				//     // update animator if using character
				//     if (_hasAnimator)
				//     {
				//         _animator.SetBool(_animIDJump, true);
				//     }
				// }

				// jump timeout
				if (JumpTimeoutDelta >= 0.0f)
				{
					JumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				SetState(new Player_FlightState(this));

				JumpTimeoutDelta = JumpTimeOut;

				if (FallTimeoutDelta >= 0.0f)
				{
					FallTimeoutDelta -= Time.deltaTime;
				}
				else
				{
					SetFloat("JumpPower", VerticalVelocity);
				}
			}

			if (VerticalVelocity < 100.0f)
			{
				VerticalVelocity += Gravity * Time.deltaTime;
			}
		}
		#endregion
		#region IPlayerSkill
		public abstract void PassiveSkill();
		public abstract void MainSkill(bool isPressed_);
		public abstract void SubSkill(bool isPressed_);
		public abstract void UtilitySkill(bool isPressed_);
		public abstract void SpecialSkill(bool isPressed_);
		#endregion

		#region Observer Pattern
		protected List<IObserver> _observers = new List<IObserver>();
		public List<IObserver> Observers { get { return _observers; } protected set { _observers = value; } }
		public void RegisterObserver(IObserver observer)
		{
			Observers.Add(observer);
		}

		public void RemoveObserver(IObserver observer)
		{
			Observers.Remove(observer);
		}

		public void NotifyObservers(object data)
		{
			foreach (var observer in Observers)
			{
				observer.UpdateDate(data);
			}
		}

		public void NotifyObservers()
		{
			foreach (var observer in Observers)
			{
				observer.UpdateDate();
			}
		}
		#endregion

		public void OnTriggerEnter(Collider other)
		{
			Debug.Log($"OnTrigger Debug : {other.tag}");
			if (other.tag == "Exp")
			{
				IncreaseExp();
			}
			else if (other.tag == "Item")
			{
				ItemBase item = other.GetComponent<ItemBase>();
				InventoryManager.Instance.ItemAdd(item);
			}

		}

	}
}