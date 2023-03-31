using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonsterBase : MonsterBase
{
	/// <summary>
	/// 몬스터의 데이터를 설정하는 메서드
	/// </summary>
	public override void Initialize()
	{
		base.Initialize();
	}

	/// <summary>
	/// 데미지를 받는 메서드
	/// </summary>
	/// <param name="damage"></param>
	public override void OnDamaged(int damage)
	{
		base.OnDamaged(damage);

	}

	protected override void OnDie()
	{
		base.OnDie();

	}

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();
	}
}
