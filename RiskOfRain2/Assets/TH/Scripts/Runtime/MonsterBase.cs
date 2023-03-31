using UnityEngine;
using System.Collections.ObjectModel;
using System.Collections.Generic;

public enum MonsterType
{
	NORMAL = 0,
	BOSS
}

public class MonsterBase : MonoBehaviour
{
	[field: SerializeField] public MonsterType Type { get; private set; }
	[field: SerializeField] public int MaxHp { get; private set; }
	[field: SerializeField] public int Hp { get; private set; }
	[field: SerializeField] public int Power { get; private set; }
	[field: SerializeField] public int Speed { get; private set; }
	[field: SerializeField] public int MaxSqrDetectRange { get; private set; }
	[field: SerializeField] public int MinSqrDetectRange { get; private set; }
	[field: SerializeField] public int SkillNum { get; protected set; } = -1;

	public MonsterSpawn SpawnState { get; private set; }
	public MonsterIdle IdleState { get; private set; }
	public MonsterMove MoveState { get; private set; }
	public MonsterOnSkill OnSkillState { get; private set; }
	public MonsterOnDamaged OnDamagedState { get; private set; }
	public MonsterDeath DeathState { get; private set; }

    [SerializeField] private MonsterData _data;
    [SerializeField] protected ReadOnlyCollection<SkillData> _skills;
    [SerializeField] protected List<float> _coolDownTimes = new List<float>();
    [SerializeField] protected int[] _availableSkills;
    protected MonsterFSM _fsm;
    protected Animator _anim;


	/// <summary>
	/// 몬스터의 데이터를 설정하는 메서드
	/// </summary>
	/// <param name="data">몬스터 SO 데이터</param>
	public virtual void Initialize()
	{
		Type = _data.Type;
		Hp = MaxHp = _data.Health;
		Power = _data.Power;
		Speed = _data.Speed;
		MaxSqrDetectRange = _data.MaxSqrDetectRange;
		MinSqrDetectRange = _data.MinSqrDetectRange;

        _skills = _data.Skills;
        _coolDownTimes.Clear();
        _availableSkills = new int[_data.Skills.Count];

		_fsm.Initialize(_skills, SpawnState);
	}

	public void OnDamaged(int damage)
	{
		Hp -= damage;

		if (Hp <= 0)
		{
			OnDie();
			return;
		}

		if (Type == MonsterType.NORMAL)
		{
			_fsm.ChangeState(OnDamagedState);
		}
	}

    /// <summary>
    /// 사용 가능한 스킬을 선택하는 메서드
    /// </summary>
    /// <returns>스킬 사용 가능 여부</returns>
    public virtual bool TrySelectSkill()
    {
        return false;
    }

	public virtual void ResetCoolDown()
	{
		/* Do nothing */
	}

	private void OnDie()
	{
		_fsm.ChangeState(new MonsterDeath(this, _fsm));
	}

	private void Awake()
	{
		_fsm = GetComponent<MonsterFSM>();
		_anim = GetComponent<Animator>();

		SpawnState = new MonsterSpawn(this, _fsm);
		IdleState = new MonsterIdle(this, _fsm);
		MoveState = new MonsterMove(this, _fsm);
		OnSkillState = new MonsterOnSkill(this, _fsm);
		OnDamagedState = new MonsterOnDamaged(this, _fsm);
		DeathState = new MonsterDeath(this, _fsm);
	}

	private void Start()
	{
		//
		Initialize();
	}


	private void OnDrawGizmosSelected()
	{
		if (_skills == null)
			return;

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, Mathf.Sqrt(MaxSqrDetectRange));
		Gizmos.DrawWireSphere(transform.position, Mathf.Sqrt(MinSqrDetectRange));
		Gizmos.color = Color.red;
		foreach (var skill in _skills)
			Gizmos.DrawWireSphere(transform.position, Mathf.Sqrt(skill.SqrRange));
	}
}