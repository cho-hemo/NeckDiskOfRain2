using UnityEngine;
using UnityEngine.AI;
using RiskOfRain2.Manager;
using RiskOfRain2;

public class NormalMonsterBase : MonsterBase
{
	protected Transform _player;
	protected NavMeshAgent _pathFinder;

	[SerializeField] protected float LookRange; // 시야 영역

	public bool BeAttackedEnd = false;
	private int _monsterId = default;
	private bool _HpBarCondition = false;
	private Transform _hpBarTransform = default;

	/// <summary>
	/// 몬스터의 데이터를 설정하는 메서드
	/// </summary>
	public override void Initialize()
	{
		_monsterId = GetInstanceID();
		UIManager.Instance.CreateMonsterHpBar(_monsterId);
		_HpBarCondition = true;
	}

	/// <summary>
	/// 데미지를 받는 메서드
	/// </summary>
	/// <param name="damage"></param>
	public override void OnDamaged(int damage)
	{
		base.OnDamaged(damage);
		UIManager.Instance.OnDamageMonsterHpBar(_monsterId, Hp, MaxHp);
	}

	protected virtual void Update()
	{
		// 여기다가 트랜스폼의 값이 자식의 hp바 위치를 넣어주면 됩니다.
		if (_HpBarCondition) { UIManager.Instance.UpdateMonsterHpBar(_monsterId, _hpBarTransform); }
	}

	/// <summary>
	/// 몬스터 피격 애니메이션 종료 시 호출되는 이벤트
	/// </summary>
	public void HitAnimeEnd()
	{
		BeAttackedEnd = true;
	}

	/// <summary>
	/// 몬스터 사망 애니메이션 종료 시 호출되는 이벤트
	/// </summary>
	public virtual void OnDeathEnd()
	{
		Debug.Log($"Ondeath Debug : {this.name}");
		//풀에 오브젝트 반환
		ObjectPoolManager.Instance.ObjectPoolPush(gameObject);
		MonsterSpawner.ReduceMonsterCount();

		//gameObject.SetActive(false);

		//
		//
		//풀에서 오브젝트 가져오기
		//GameObject inst = ObjectPoolManager.Instance.ObjectPoolPop("BeetleMK2");
		//inst.transform.position = Vector3.zero;
		//inst.SetActive(true);
	}

	protected override void OnDie()
	{
		base.OnDie();
		_HpBarCondition = false;
		UIManager.Instance.DisableMonsterHpBar(_monsterId);
		_pathFinder.enabled = false;
		_anim.SetTrigger("DeathTrg");
	}

	protected override void Awake()
	{
		base.Awake();
		_hpBarTransform = gameObject.FindChildObj("HpBarPos").transform;
	}

	protected virtual void OnEnable()
	{
		if (_player == null && GameManager.Instance.Player != null)
		{
			_player = GameManager.Instance.Player.transform;
		}
		ReSpawn();
		UIManager.Instance.OnDamageMonsterHpBar(_monsterId, Hp, MaxHp);
	}

	protected virtual void ReSpawn()
	{
		Hp = MaxHp;
		_HpBarCondition = true;
		_pathFinder.enabled = true;
	}
}
