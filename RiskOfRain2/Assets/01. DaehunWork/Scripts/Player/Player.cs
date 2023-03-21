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
    [Tooltip("Player 세로 가속도")]
    protected float _verticalVelocity = 0;

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
    protected float _attackDelay;

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
    [Tooltip("질주속도")]
    protected float _sprintSpeed = 8.75f;

    [SerializeField]
    [Tooltip("점프력")]
    protected float _jumpHeight = 1.5f;

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
    protected Transform _focusPoint;
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

    [SerializeField]
    [Tooltip("CameraTarget")]
    protected GameObject _cinemachineCameraTarget;
    #endregion

    // cinemachine
    protected float _cinemachineTargetYaw;
    protected float _cinemachineTargetPitch;

    #region Property
    #region PlayerController
    public float TopClamp { get { return _topClamp; } protected set { _topClamp = value; } }
    public float BottomClamp { get { return _bottomClamp; } protected set { _bottomClamp = value; } }
    public Vector2 InputMove { get { return _inputMove; } protected set { _inputMove = value; } }
    public Vector2 InputLook { get { return _inputLook; } protected set { _inputLook = value; } }
    public float SpeedChangeRate { get { return _speedChangeRate; } protected set { _speedChangeRate = value; } }
    public float TargetRotation { get { return _targetRotation; } protected set { _targetRotation = value; } }
    public float RotationSmoothTime { get { return _rotationSmoothTime; } protected set { _rotationSmoothTime = value; } }
    public float VerticalVelocity { get { return _verticalVelocity; } protected set { _verticalVelocity = value; } }
    public float RotationVelocity { get { return _rotationVelocity; } protected set { _rotationVelocity = value; } }
    #endregion
    public float MaxHp { get { return _maxHp; } protected set { _maxHp = value; } }
    public float CurrentHp { get { return _currentHp; } protected set { _currentHp = value; } }
    public float Defense { get { return _defense; } protected set { _defense = value; } }
    public float AttackDamage { get { return _attackDamage; } protected set { _attackDamage = value; } }
    public float AttackDelay { get { return _attackDelay; } protected set { _attackDelay = value; } }
    public List<float> SkillCoolTime { get { return _skillCoolTime; } protected set { _skillCoolTime = value; } }
    public float HealthRegen { get { return _healthRegen; } protected set { _healthRegen = value; } }
    public float DefaultSpeed { get { return _defaultSpeed; } protected set { _defaultSpeed = value; } }
    public float CurrentSpeed { get { return _currentSpeed; } protected set { _currentSpeed = value; } }
    public float SprintSpeed { get { return _sprintSpeed; } protected set { _sprintSpeed = value; } }
    public float JumpHeight { get { return _jumpHeight; } protected set { _jumpHeight = value; } }
    public float Gravity { get { return _gravity; } protected set { _gravity = value; } }
    public bool IsMove { get { return _isMove; } protected set { _isMove = value; } }
    public bool IsSprint { get { return _isSprint; } protected set { _isSprint = value; } }
    public bool IsGrounded { get { return _isGrounded; } protected set { _isGrounded = value; } }
    public bool IsDead { get { return _isDead; } protected set { _isDead = value; } }
    public bool Osp { get { return _osp; } protected set { _osp = value; } }
    public Transform FocusPoint { get { return _focusPoint; } protected set { _focusPoint = value; } }
    public PlayerType PlayerType { get { return _playerType; } protected set { _playerType = value; } }
    public StateMachine StateMachine { get { return _stateMachine; } protected set { _stateMachine = value; } }
    public Animator PlayerAnimator { get { return _playerAnimator; } protected set { _playerAnimator = value; } }
    public CharacterController CharacterController { get { return _characterController; } protected set { _characterController = value; } }
    public GameObject CinemachineCameraTarget { get { return _cinemachineCameraTarget; } protected set { _cinemachineCameraTarget = value; } }
    public float CinemachineTargetYaw { get { return _cinemachineTargetYaw; } protected set { _cinemachineTargetYaw = value; } }
    public float CinemachineTargetPitch { get { return _cinemachineTargetPitch; } protected set { _cinemachineTargetPitch = value; } }
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
        StateMachine.SetState(new Player_IdleState(this));
    }

    protected void Update()
    {
        StateMachine.UpdateState();
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
                StateMachine.SetState(new Player_DeadState(this));
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
                StateMachine.SetState(new Player_DeadState(this));
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

    #region SetAnimatorParameter Func warpping
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

        if (IsSprint)
        {
            StateMachine.SetState(new Player_SprintState(this));
        }
        else
        {
            StateMachine.SetState(new Player_WalkState(this));
        }
    }


    public void Move()
    {
        float targetSpeed = IsSprint ? SprintSpeed : DefaultSpeed;

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

        Vector3 inputDirection = new Vector3(InputMove.x, 0.0f, InputMove.y).normalized;

        if (InputMove != Vector2.zero)
        {
            TargetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              Camera.main.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, TargetRotation, 0.0f) * Vector3.forward;

        CharacterController.Move(targetDirection.normalized * (CurrentSpeed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    public void Look(Vector2 value)
    {
        InputLook = value;
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

        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(CinemachineTargetPitch + 0f, CinemachineTargetYaw, 0.0f);
    }

    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    public void Jump(bool isPressed)
    {
        if (isPressed)
        {
            StateMachine.SetState(new Player_JumpState(this));
        }
    }

    public void Sprint()
    {
        IsSprint = !IsSprint;
    }

    public void GroundedCheck()
    {

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