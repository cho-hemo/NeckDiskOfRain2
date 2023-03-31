using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class BossMonsterBase : MonsterBase
{
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

	public override void Initialize()
	{
		base.Initialize();
		SecondName = _data.Name;
		_skills = _data.Skills;
		_coolDownTimes.Clear();
		_availableSkills = new int[_data.Skills.Count];
		_fsm.Initialize(_skills, SpawnState);

		for (int i = 0; i < _skills.Count; i++)
		{
			_coolDownTimes.Add(_skills[i].CoolDownTime);
		}
		UIManager.Instance.ActiveBoss(Name, SecondName);
	}

	/// <summary>
	/// 데미지를 받는 메서드
	/// </summary>
	/// <param name="damage"></param>
	public override void OnDamaged(int damage)
	{
		base.OnDamaged(damage);
		UIManager.Instance.BossHpControl(damage);
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
