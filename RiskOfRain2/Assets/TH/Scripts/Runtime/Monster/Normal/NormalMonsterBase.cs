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

	/// <summary>
	/// 몬스터의 데이터를 설정하는 메서드
	/// </summary>
	public override void Initialize()
	{
		/* Do nothing */
	}

	/// <summary>
	/// 데미지를 받는 메서드
	/// </summary>
	/// <param name="damage"></param>
	public override void OnDamaged(int damage)
	{
		base.OnDamaged(damage);
<<<<<<< HEAD
=======

>>>>>>> 633ebe8ab84586231d43ac897b6ace7027038c0d
		// Change after merge Scene
		//UIManager.Instance.MonsterHpBarControl(Name, Hp, MaxHp);
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
		//풀에 오브젝트 반환
		// Change after merge Scene
		//ObjectPoolManager.Instance.ObjectPoolPush(gameObject);

		gameObject.SetActive(false);

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
<<<<<<< HEAD
=======

>>>>>>> 633ebe8ab84586231d43ac897b6ace7027038c0d
		_pathFinder.enabled = false;
		_anim.SetTrigger("DeathTrg");
	}

	protected override void Awake()
	{
		base.Awake();
	}

	protected virtual void OnEnable()
	{
		if (_player == null && GameManager.Instance.Player != null)
		{
<<<<<<< HEAD
=======
			Debug.Log($"Player {_player}");
>>>>>>> 633ebe8ab84586231d43ac897b6ace7027038c0d
			_player = GameManager.Instance.Player.transform;
		}
		_pathFinder.enabled = true;
	}
}
