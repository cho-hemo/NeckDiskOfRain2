using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif
public abstract class Player : MonoBehaviour, IPlayerSkill, ISubject
{
    #region Inspector
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
    #endregion

    #region Property
    public float MaxHp { get { return _maxHp; } protected set { _maxHp = value; } }
    public float CurrentHp { get { return _currentHp; } protected set { _currentHp = value; } }
    public float Defense { get { return _defense; } protected set { _defense = value; } }
    public float AttackDamage { get { return _attackDamage; } protected set { _attackDamage = value; } }
    public float AttackDelay { get { return _attackDelay; } protected set { _attackDelay = value; } }
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
        // // set target speed based on move speed, sprint speed and if sprint is pressed
        // float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

        // // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // // if there is no input, set the target speed to 0
        // if (_input.move == Vector2.zero) targetSpeed = 0.0f;

        // // a reference to the players current horizontal velocity
        // float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        // float speedOffset = 0.1f;
        // float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

        // // accelerate or decelerate to target speed
        // if (currentHorizontalSpeed < targetSpeed - speedOffset ||
        //     currentHorizontalSpeed > targetSpeed + speedOffset)
        // {
        //     // creates curved result rather than a linear one giving a more organic speed change
        //     // note T in Lerp is clamped, so we don't need to clamp our speed
        //     _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
        //         Time.deltaTime * SpeedChangeRate);

        //     // round speed to 3 decimal places
        //     _speed = Mathf.Round(_speed * 1000f) / 1000f;
        // }
        // else
        // {
        //     _speed = targetSpeed;
        // }   

        // _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        // if (_animationBlend < 0.01f) _animationBlend = 0f;

        // // normalise input direction
        // Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        // // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // // if there is a move input rotate player when the player is moving
        // if (_input.move != Vector2.zero)
        // {
        //     _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
        //                       _mainCamera.transform.eulerAngles.y;
        //     float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
        //         RotationSmoothTime);

        //     // rotate to face input direction relative to camera position
        //     transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        // }


        // Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // // move the player
        // _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
        //                  new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

        if (value != Vector2.zero)
        {
            if (IsSprint)
            {
                StateMachine.SetState(new Player_SprintState(this));
            }
            else
            {
                StateMachine.SetState(new Player_WalkState(this));
            }
        }
    }

    public void Look(Vector2 value)
    {

    }

    public void Jump(bool isPressed)
    {

    }

    public void Sprint()
    {

    }

    public void GroundedCheck()
    {

    }



    public abstract void PassiveSkill();
    public abstract void MainSkill(bool isPressed_);
    public abstract void SubSkill(bool isPressed_);
    public abstract void UtilitySkill(bool isPressed_);
    public abstract void SpecialSkill(bool isPressed_);

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

    public void NotifyObservers()
    {
        foreach (var observer in Observers)
        {
            observer.UpdateDate(gameObject);
        }
    }
    #endregion
}