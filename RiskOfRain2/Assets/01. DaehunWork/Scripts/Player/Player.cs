using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif
public abstract class Player : MonoBehaviour, IPlayerSkill, ISubject
{
    #region Inspector
    [Header("Player Controller")]
    [SerializeField]
    [Tooltip("카메라 윗 방향 최대 각도")]
    protected float _topClamp;

    [SerializeField]
    [Tooltip("카메라 아랫 방향 최대 각도")]
    protected float _bottomClamp;

    [SerializeField]
    [Tooltip("Player Move 입력")]
    protected Vector2 _inputMove;

    [SerializeField]
    [Tooltip("Player Look Input")]
    protected Vector2 _inputLook;

    [SerializeField]
    [Tooltip("가속 감속")]
    protected float _speedChangeRate = 10.0f;

    [SerializeField]
    [Tooltip("타겟 회전")]
    protected float _targetRotation = 0.0f;

    [SerializeField]
    [Tooltip("캐릭터 회전 속도")]
    [Range(0.0f, 0.3f)]
    protected float _rotationSmoothTime = 0.12f;

    [SerializeField]
    [Tooltip("Player 회전 가속도")]
    protected float _rotationVelocity;

    [SerializeField]
    [Tooltip("Player 추락 가속도")]
    protected float _verticalVelocity = 0;

    [SerializeField]
    [Tooltip("Ground와 거리")]
    protected float _groundedOffset = -0.14f;

    [SerializeField]
    [Tooltip("Ground 체크를 위한 반지름 CharacterController와 일치해야함")]
    protected float _groundedRadius = 0.3f;

    [SerializeField]
    [Tooltip("캐릭터가 밟을 수 있는 Ground의 Layer")]
    protected LayerMask _groundLayers;

    [SerializeField]
    [Tooltip("낙하 상태 진입까지 소요되는 시간")]
    protected float _fallTimeout = 0.15f;

    [SerializeField]
    [Tooltip("다시 점프를 하기 위한 딜레이 시간")]
    protected float _jumpTimeout = 0.1f;

    [SerializeField]
    [Tooltip("추락 TimeOut DeltaTime")]
    protected float _fallTimeoutDelta;

    [SerializeField]
    [Tooltip("점프 TimeOut DeltaTime")]
    protected float _jumpTimeoutDelta;

    [Space(5)]
    [Header("Player Stat")]

    [SerializeField]
    [Tooltip("최대 체력")]
    protected float _maxHp;

    [SerializeField]
    [Tooltip("현재 체력")]
    protected float _currentHp;

    [SerializeField]
    [Tooltip("방어막")]
    protected float _defense;

    [SerializeField]
    [Tooltip("공격력")]
    protected float _attackDamage;

    [SerializeField]
    [Tooltip("공격 속도")]
    protected float _attackSpeed;

    [SerializeField]
    [Tooltip("스킬 쿨타임")]
    protected List<float> _skillCoolTime = default;

    [SerializeField]
    [Tooltip("체력 회복")]
    protected float _healthRegen;

    [SerializeField]
    [Tooltip("기본 이동속도")]
    protected float _defaultSpeed = 7.0f;

    [SerializeField]
    [Tooltip("현재 이동속도")]
    protected float _currentSpeed = 0f;

    [SerializeField]
    [Tooltip("걷기 속도")]
    protected float _walkSpeed = 7.0f;

    [SerializeField]
    [Tooltip("질주 속도")]
    protected float _sprintSpeed = 8.75f;

    [SerializeField]
    [Tooltip("최대 점프 카운트")]
    protected int _maxJumpCount = 1;

    [SerializeField]
    [Tooltip("현재 점프 횟수")]
    protected int _currentJumpCount = default;

    [SerializeField]
    [Tooltip("점프력")]
    protected float _jumpHeight = 5f;

    [SerializeField]
    [Tooltip("중력")]
    protected float _gravity = -9.81f;

    [SerializeField]
    [Tooltip("걷기")]
    protected bool _isMove;

    [SerializeField]
    [Tooltip("달리기")]
    protected bool _isSprint;

    [SerializeField]
    [Tooltip("바닥 체크")]
    protected bool _isGrounded = true;

    [SerializeField]
    [Tooltip("사망 체크")]
    protected bool _isDead = false;

    [SerializeField]
    [Tooltip("OSP")]
    protected bool _osp = false;

    [Space(5)]
    [Header("플레이어 오브젝트")]

    [SerializeField]
    [Tooltip("총알이 발사될 위치")]
    protected List<Transform> _focusPoint;

    [SerializeField]
    [Tooltip("플레이어 타입(종류)")]
    protected PlayerType _playerType = PlayerType.NONE;

    [SerializeField]
    [Tooltip("상태머신")]
    protected StateMachine _stateMachine = default;

    [SerializeField]
    [Tooltip("애니메이터 컨트롤러")]
    protected Animator _playerAnimator;

    [SerializeField]
    [Tooltip("Charcter Controller")]
    protected CharacterController _characterController;

    [Space(5)]
    [Header("CineMachine")]
    [SerializeField]
    [Tooltip("CameraTarget")]
    protected GameObject _cinemachineCameraTarget;
    [SerializeField]
    protected float _cinemachineTargetYaw;
    [SerializeField]
    protected float _cinemachineTargetPitch;
    #endregion

    #region Property
    #region PlayerController
    // { PlayerController Property
    public float TopClamp { get { return _topClamp; } protected set { _topClamp = value; } }
    public float BottomClamp { get { return _bottomClamp; } protected set { _bottomClamp = value; } }
    public Vector2 InputMove { get { return _inputMove; } protected set { _inputMove = value; } }
    public Vector2 InputLook { get { return _inputLook; } protected set { _inputLook = value; } }
    public float SpeedChangeRate { get { return _speedChangeRate; } protected set { _speedChangeRate = value; } }
    public float TargetRotation { get { return _targetRotation; } protected set { _targetRotation = value; } }
    public float RotationSmoothTime { get { return _rotationSmoothTime; } protected set { _rotationSmoothTime = value; } }
    public float VerticalVelocity { get { return _verticalVelocity; } protected set { _verticalVelocity = value; } }
    public float RotationVelocity { get { return _rotationVelocity; } protected set { _rotationVelocity = value; } }
    public float GroundedOffset { get { return _groundedOffset; } protected set { _groundedOffset = value; } }
    public float GroundedRadius { get { return _groundedOffset; } protected set { _groundedOffset = value; } }
    public LayerMask GroundLayers { get { return _groundLayers; } protected set { _groundLayers = value; } }
    public float FallTimeOut { get { return _fallTimeout; } protected set { _fallTimeout = value; } }
    public float JumpTimeOut { get { return _jumpTimeout; } protected set { _jumpTimeout = value; } }
    public float FallTimeoutDelta { get { return _fallTimeoutDelta; } protected set { _fallTimeoutDelta = value; } }
    public float JumpTimeoutDelta { get { return _jumpTimeoutDelta; } protected set { _jumpTimeoutDelta = value; } }
    // } PlayerController Property
    #endregion

    #region Player Stat
    public float MaxHp { get { return _maxHp; } protected set { _maxHp = value; } }
    public float CurrentHp { get { return _currentHp; } protected set { _currentHp = value; } }
    public float Defense { get { return _defense; } protected set { _defense = value; } }
    public float AttackDamage { get { return _attackDamage; } protected set { _attackDamage = value; } }
    public float AttackSpeed { get { return _attackSpeed; } protected set { _attackSpeed = value; } }
    public List<float> SkillCoolTime { get { return _skillCoolTime; } protected set { _skillCoolTime = value; } }
    public float HealthRegen { get { return _healthRegen; } protected set { _healthRegen = value; } }
    public float DefaultSpeed { get { return _defaultSpeed; } protected set { _defaultSpeed = value; } }
    public float CurrentSpeed { get { return _currentSpeed; } protected set { _currentSpeed = value; } }
    public float WalkSpeed { get { return _walkSpeed; } protected set { _walkSpeed = value; } }
    public float SprintSpeed { get { return _sprintSpeed; } protected set { _sprintSpeed = value; } }
    public int MaxJumpCount { get { return _maxJumpCount; } protected set { _maxJumpCount = value; } }
    public int CurrentJumpCount { get { return _currentJumpCount; } protected set { _currentJumpCount = value; } }
    public float JumpHeight { get { return _jumpHeight; } protected set { _jumpHeight = value; } }
    public float Gravity { get { return _gravity; } protected set { _gravity = value; } }
    public bool IsMove { get { return _isMove; } protected set { _isMove = value; } }
    public bool IsSprint { get { return _isSprint; } protected set { _isSprint = value; } }
    public bool IsGrounded { get { return _isGrounded; } protected set { _isGrounded = value; } }
    public bool IsDead { get { return _isDead; } protected set { _isDead = value; } }
    public bool Osp { get { return _osp; } protected set { _osp = value; } }
    #endregion

    #region Player Object
    public List<Transform> FocusPoint { get { return _focusPoint; } protected set { _focusPoint = value; } }
    public PlayerType PlayerType { get { return _playerType; } protected set { _playerType = value; } }
    public StateMachine StateMachine { get { return _stateMachine; } protected set { _stateMachine = value; } }
    public Animator PlayerAnimator { get { return _playerAnimator; } protected set { _playerAnimator = value; } }
    public CharacterController CharacterController { get { return _characterController; } protected set { _characterController = value; } }
    public GameObject CinemachineCameraTarget { get { return _cinemachineCameraTarget; } protected set { _cinemachineCameraTarget = value; } }
    public float CinemachineTargetYaw { get { return _cinemachineTargetYaw; } protected set { _cinemachineTargetYaw = value; } }
    public float CinemachineTargetPitch { get { return _cinemachineTargetPitch; } protected set { _cinemachineTargetPitch = value; } }
    #endregion

    #endregion

    // #if ENABLE_INPUT_SYSTEM
    //     protected PlayerInput _playerInput;
    //     protected PlayerKeyInput _input;

    //     public PlayerInput PlayerInput { get { return _playerInput; } protected set { _playerInput = value; } }
    //     public PlayerKeyInput Input { get { return _input; } protected set { _input = value; } }
    // #endif

    protected void Start()
    {
        TryGetComponent(out _playerAnimator);
        PlayerAnimator.SetBool("IsGrounded", IsGrounded);
        TryGetComponent(out _characterController);
        CinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

        StateMachine = new StateMachine();
        SetState(new Player_IdleState(this));
    }

    protected void Update()
    {
        GroundedCheck();
        JumpAndGravity();
        UpdateState();
    }

    protected void LateUpdate()
    {
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
    }

    protected IEnumerator OneShotProtection()
    {
        Osp = true;
        yield return new WaitForSeconds(0.1f);
        Osp = false;
    }

    #region StateMachine Caching
    public void UpdateState()
    {
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

        if (IsGrounded)
        {
            if (IsSprint)
            {
                SetState(new Player_SprintState(this));
            }
            else
            {
                SetState(new Player_WalkState(this));
            }
        }
    }

    public void Move()
    {
        float targetSpeed = IsSprint ? SprintSpeed : WalkSpeed;

        if (InputMove == Vector2.zero) targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(CharacterController.velocity.x, 0.0f, CharacterController.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = KeyInputManager.Instance.analogMovement ? InputMove.magnitude : 1f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            CurrentSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * SpeedChangeRate);

            CurrentSpeed = Mathf.Round(CurrentSpeed * 1000f) / 1000f;
        }
        else
        {
            CurrentSpeed = targetSpeed;
        }

        PlayerRotation();

        Vector3 targetDirection = Quaternion.Euler(0.0f, TargetRotation, 0.0f) * Vector3.forward;

        CharacterController.Move(targetDirection.normalized * (CurrentSpeed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    public void PlayerRotation()
    {
        Vector2 inputDirection_ = InputMove.normalized;

        TargetRotation = Mathf.Atan2(inputDirection_.x, inputDirection_.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetRotation, ref _rotationVelocity,
            RotationSmoothTime);

        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

    }

    public void Look(Vector2 value)
    {
        if (InputLook != value)
        {
            InputLook = value;
            float aimX_ = default;
            float aimY_ = default;

            float angleX_ = CinemachineCameraTarget.transform.localEulerAngles.x;
            if (180 <= angleX_)
            {
                aimY_ = (angleX_ - 270) / 180f;
            }
            else
            {
                aimY_ = (angleX_ + 90) / 180f;
            }

            float angleY_ = CinemachineCameraTarget.transform.localEulerAngles.y;
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

        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(CinemachineTargetPitch, CinemachineTargetYaw, 0.0f);
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
            SetBool("IsGrounded", IsGrounded);
            // reset the jump timeout timer
            JumpTimeoutDelta = JumpTimeOut;

            // fall timeout
            if (FallTimeoutDelta >= 0.0f)
            {
                FallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                // update animator if using character
                SetFloat("JumpPower", VerticalVelocity);
            }
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (VerticalVelocity < 100.0f)
        {
            VerticalVelocity += Gravity * Time.deltaTime;
        }
    }


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
            observer.UpdateDate(gameObject);
        }
    }
    #endregion
}