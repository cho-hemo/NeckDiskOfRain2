using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class BossMonsterBase : MonsterBase
{
	[SerializeField] private MonsterData _data;
	[field: SerializeField] public int MaxSqrDetectRange { get; private set; }
	[field: SerializeField] public int MinSqrDetectRange { get; private set; }
	[field: SerializeField] public string SecondName { get; private set; }
	[SerializeField] protected ReadOnlyCollection<SkillData> _skills;
	[field: SerializeField] public int SkillNum { get; protected set; } = -1;
	[SerializeField] protected List<float> _coolDownTimes = new List<float>();
	[SerializeField] protected int[] _availableSkills;
	protected MonsterFSM _fsm;

	public MonsterSpawn SpawnState { get; private set; }
	public MonsterIdle IdleState { get; private set; }
	public MonsterMove MoveState { get; private set; }
	public MonsterOnSkill OnSkillState { get; private set; }
	public MonsterDeath DeathState { get; private set; }

	/// <summary>
	/// 몬스터의 데이터를 설정하는 메서드
	/// </summary>
	public override void Initialize()
	{
		base.Initialize();

		Name = _data.Name;
		Type = _data.Type;
		Hp = MaxHp = _data.Health;
		Power = _data.Power;
		Speed = _data.Speed;
		MaxSqrDetectRange = _data.MaxSqrDetectRange;
		MinSqrDetectRange = _data.MinSqrDetectRange;

		SecondName = _data.SecondName;
		_skills = _data.Skills;
		_coolDownTimes.Clear();
		_availableSkills = new int[_data.Skills.Count];
		_fsm.Initialize(_skills, SpawnState);

		for (int i = 0; i < _skills.Count; i++)
		{
			_coolDownTimes.Add(_skills[i].CoolDownTime);
		}
		UIManager.Instance.ActiveBoss(Name, SecondName);
		UIManager.Instance.BossHpControl(Hp, MaxHp);
	}

	/// <summary>
	/// 데미지를 받는 메서드
	/// </summary>
	/// <param name="damage"></param>
	public override void OnDamaged(int damage)
	{
		base.OnDamaged(damage);
		UIManager.Instance.BossHpControl(Hp, MaxHp);
	}

	/// <summary>
	/// 사용 가능한 스킬을 선택하는 메서드
	/// </summary>
	/// <returns>스킬 사용 가능 여부</returns>
	public virtual bool TrySelectSkill()
	{
		return false;
	}

	/// <summary>
	/// 스킬의 쿨타임을 초기화하는 메서드
	/// </summary>
	public virtual void ResetCoolDown()
	{
		/* Do nothing */
	}

	protected override void OnDie()
	{
		base.OnDie();
		_fsm.ChangeState(DeathState);
	}

	protected override void Awake()
	{
		base.Awake();
		_fsm = GetComponent<MonsterFSM>();
		SpawnState = new MonsterSpawn(this, _fsm);
		IdleState = new MonsterIdle(this, _fsm);
		MoveState = new MonsterMove(this, _fsm);
		OnSkillState = new MonsterOnSkill(this, _fsm);
		DeathState = new MonsterDeath(this, _fsm);
	}

	private void Start()
	{
		Initialize();
	}

	private void Update()
	{
		for (int i = 0; i < _coolDownTimes.Count; i++)
		{
			if (_coolDownTimes[i] > 0)
			{
				_coolDownTimes[i] -= Time.deltaTime;
			}
		}
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
