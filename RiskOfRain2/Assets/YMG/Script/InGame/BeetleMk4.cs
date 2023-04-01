using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BeetleMk4 : NormalMonsterBase
{
	public AnimationClip SpawnAnime;
	public AnimationClip AttackAnime;

	protected float AttackRange = 4f; // 공격 영역

	public bool headAttack = false;
	//public bool isDelay = false;
	//public float delayTime = 2f;
	//float timer = 0f;

	WaitForSeconds Delay300 = new WaitForSeconds(3f);

	//열거형으로 정해진 상태값을 사용
	enum State
	{
		Idle,
		Move,
		Attack,
		BeAttacked
	}
	//상태 처리
	State state;

	protected override void Awake()
	{
		base.Awake();
		_pathFinder = GetComponent<NavMeshAgent>();

		LookRange = 20f; // 시야 영역
						 //Speed = 5f;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		StartCoroutine(AnimeWaiting());
		state = State.Idle;
	}

	protected override void Update()
	{
		base.Update();
		if (_anim.GetBool("SpawnEnd") && _pathFinder.enabled == true)
		{
			//만약 state가 idle이라면
			if (state == State.Idle)
			{
				UpdateIdle();
			}
			else if (state == State.Move)
			{
				UpdateRun();
			}
			else if (state == State.Attack)
			{
				UpdateAttack();
			}
			else if (state == State.BeAttacked)
			{
				UpdateBeAttacked();
			}
		}

		//피격 테스트

		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		//	OnDamaged(5);
		//}

		//if (shot) 
		//{
		//	if (!isDelay)
		//	{
		//		isDelay = true;
		//		//Debug.Log("Look");
		//	}
		//	else
		//	{
		//		//Debug.Log("noLook");
		//	}

		//	if (isDelay)
		//	{
		//		timer += Time.deltaTime;

		//		if (timer >= delayTime)
		//		{
		//			timer = 0f;
		//			isDelay = false;
		//		}
		//	}
		//}

	}

	IEnumerator AnimeWaiting()
	{
		yield return new WaitForSeconds(SpawnAnime.length);
		// 애니메이션 발동 조건 
		_anim.SetBool("SpawnEnd", true);
	}

	IEnumerator Turm() // LEGACY
	{
		Time.timeScale = 1f;
		yield return new WaitForSecondsRealtime(3f);
		// 애니메이션 발동 조건 
		//FaceTarget();
	}

	private void UpdateAttack()
	{

		_pathFinder.speed = 0;
		//Debug.Log("Attack");
		float distance = Vector3.Distance(transform.position, _player.transform.position);

		if (distance > AttackRange && headAttack == false)
		{
			state = State.Move;
			_anim.SetTrigger("isMoveTrg");
		}       // if: Attack range를 벗어 났을 때

		// Attack motion 재생 중일 때는 FaceTarget() 을 무시하고 싶음.
		FaceTarget();
	}

	private void UpdateRun()
	{
		float distance = Vector3.Distance(transform.position, _player.transform.position);
		if (distance <= AttackRange)
		{
			state = State.Attack;
			_anim.SetTrigger("isAttackTrg");
		}

		//타겟 방향으로 이동하다가
		_pathFinder.speed = Speed;
		//요원에게 목적지를 알려준다.
		Debug.Log($"pf{_pathFinder}, pl{_player}");
		_pathFinder.SetDestination(_player.position);
	}

	void UpdateReady() // Legacy
	{
		//isLook = true;
		//StartCoroutine(LookAt());
		_anim.SetTrigger("isIdleTrg");
		//FaceTarget();
		float distance = Vector3.Distance(transform.position, _player.transform.position);

		if (distance <= LookRange)
		{
			state = State.Move;
			//이렇게 state값을 바꿨다고 animation까지 바뀔까? no! 동기화를 해줘야한다.
			//anim.SetTrigger("isMoveTrg");
		}

		if (distance <= AttackRange)
		{
			state = State.Attack;
			//anim.SetTrigger("isAttackTrg");
		}


	}

	void UpdateIdle()
	{
		//Debug.Log("Idle");
		float distance = Vector3.Distance(transform.position, _player.transform.position);

		_pathFinder.speed = 0;
		//생성될때 목적지(Player)를 찿는다.
		//Target = GameObject.Find("Player").transform;
		//target을 찾으면 Run상태로 전이하고 싶다.
		if (distance <= LookRange)
		{
			state = State.Move;
			//이렇게 state값을 바꿨다고 animation까지 바뀔까? no! 동기화를 해줘야한다.
			_anim.SetTrigger("isMoveTrg");
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, LookRange);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, AttackRange);
	}

	//! 이 함수는 isDelay가 참인 동안에 Target을 바라보는 함수
	void FaceTarget()
	{
		if (headAttack == false)
		{
			// direction to the target
			Vector3 direction = (_player.position - transform.position).normalized;
			// rotation where we point to that target
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			// update our own rotation to point in this direction
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * Speed);
		}
	}       // FaceTarget()

	//IEnumerator LookAt()
	//{
	//	yield return new WaitForSecondsRealtime(3f);

	//	Debug.Log("바라보기");
	//	// direction to the target
	//	Vector3 direction = (Target.position - transform.position).normalized;
	//	// rotation where we point to that target
	//	Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
	//	// update our own rotation to point in this direction
	//	transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * MonsterSpeed);

	//	Debug.Log("false");
	//}

	//IEnumerator TurmAttack()
	//{
	//	yield return null;

	//	_pathFinder.isStopped = true;
	//	_pathFinder.SetDestination(_player.position);
	//	yield return Delay300;

	//	_pathFinder.isStopped = false;
	//}

	void UpdateBeAttacked()
	{
		_pathFinder.speed = 0;

		if (BeAttackedEnd == true)
		{
			state = State.Move;
			_anim.SetTrigger("isMoveTrg");
		}
	}

	public override void OnDamaged(int damage)
	{
		base.OnDamaged(damage);
		BeAttackedEnd = false;

		if (state == State.Idle || state == State.Move || state == State.BeAttacked)
		{
			state = State.BeAttacked;
			_anim.SetTrigger("BeAttackedTrg");
		}
	}
}
