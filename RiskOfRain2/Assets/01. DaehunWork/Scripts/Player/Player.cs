using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif
public abstract class Player : MonoBehaviour, IPlayerSkill
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
    public bool Osp { get { return _osp; } protected set { _osp = value; } }
    public Transform FocusPoint { get { return _focusPoint; } protected set { _focusPoint = value; } }
    public PlayerType PlayerType { get { return _playerType; } protected set { _playerType = value; } }
    #endregion

#if ENABLE_INPUT_SYSTEM
    //protected PlayerInput _playerInput;
    //protected PlayerKeyInput _input;

    //public PlayerInput PlayerInput { get { return _playerInput; } protected set { _playerInput = value; } }
    //public PlayerKeyInput Input { get { return _input; } protected set { _input = value; } }
#endif    

    protected StateMachine _stateMachine = default;
    public StateMachine StateMachine { get { return _stateMachine; } set { _stateMachine = value; } }

    public void Start()
    {
        StateMachine = new StateMachine();
        StateMachine.SetState(new Player_IdleState(this));
    }

    public void Update()
    {

    }

    public void TakeDamage(float damage_)
    {
        if (Osp) return;
        float maxDamage_ = (MaxHp + Defense) * 0.9f;
        if (maxDamage_ < damage_)
        {
            CurrentHp -= maxDamage_;
            StartCoroutine(OneShotProtection());
        }
        else
        {
            if (CurrentHp - damage_ <= 0)
            {
                CurrentHp = 0;
            }
            else
            {
                CurrentHp -= damage_;
            }
        }
    }

    IEnumerator OneShotProtection()
    {
        Osp = true;
        yield return new WaitForSeconds(0.1f);
        Osp = false;
    }

    public abstract void PassiveSkill();
    public abstract void MainSkill();
    public abstract void SubSkill();
    public abstract void UtilitySkill();
    public abstract void SpecialSkill();

}