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
    [Tooltip("Player Move Input")]
    protected Vector2 _inputMove;

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
    [Tooltip("이동속도")]
    protected float _speed = 7.0f;

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
    protected bool _isGrounded = false;

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
    public float TopClamp { get { return _topClamp; } protected set { _topClamp = value; } }
    public float BottomClamp { get { return _bottomClamp; } protected set { _bottomClamp = value; } }
    public Vector2 InputMove { get { return _inputMove; } protected set { _inputMove = value; } }
    public float MaxHp { get { return _maxHp; } protected set { _maxHp = value; } }
    public float CurrentHp { get { return _currentHp; } protected set { _currentHp = value; } }
    public float Defense { get { return _defense; } protected set { _defense = value; } }
    public float AttackDamage { get { return _attackDamage; } protected set { _attackDamage = value; } }
    public float AttackDelay { get { return _attackDelay; } protected set { _attackDelay = value; } }
    public List<float> SkillCoolTime { get { return _skillCoolTime; } protected set { _skillCoolTime = value; } }
    public float HealthRegen { get { return _healthRegen; } protected set { _healthRegen = value; } }
    public float Speed { get { return _speed; } protected set { _speed = value; } }
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
        StateMachine = new StateMachine();
        StateMachine.SetState(new Player_IdleState(this));
        TryGetComponent(out _playerAnimator);
    }

    protected void Update()
    {
        StateMachine.UpdateState();
    }

    protected void LateUpdate()
    {

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
        if (value != Vector2.zero)
        {
            InputMove = value;
            if (IsSprint)
            {
                StateMachine.SetState(new Player_SprintState(this));
            }
            else
            {
                StateMachine.SetState(new Player_WalkState(this));
            }

            StateMachine.UpdateState();
        }
    }

    public void Look(Vector2 value)
    {
        //Global.Log($"Look Debug : {value}");

        if (value.sqrMagnitude >= 0.01f)
        {
            CinemachineTargetYaw += value.x + Time.deltaTime;
            CinemachineTargetPitch += value.y + Time.deltaTime;
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