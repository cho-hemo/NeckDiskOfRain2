using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
	public float LookRange = 20f; // 시야 영역
	public float AttackRange = 4f; // 공격 영역
	public float Speed = default; // 이동속도

	NavMeshAgent Pathfinder; // 네비매쉬
	Transform Target; // 목표
	Animator Anime;

	//public BoxCollider CloseCombat;

	private Vector2 _velocity;
	private Vector2 _smoothDeltaPosition;

	private bool _isMove = false;
	private float _scale;

	private float _cooldownTime = 1.0f;

	private void Awake()
	{
		Pathfinder = GetComponent<NavMeshAgent>();
		Anime = Pathfinder.GetComponent<Animator>();

		_scale = transform.GetChild(0).localScale.x;
	}

	private void OnAnimatorMove()
	{
		//Debug.Log($"* {Anime.rootPosition * _scale}");
		//Vector3 rootPosition = Anime.rootPosition * _scale;//
		//Debug.Log($"r {rootPosition}, s {_scale}");

		Vector3 nextPosition = Anime.rootPosition;
		nextPosition.y = Pathfinder.nextPosition.y;

		transform.position = nextPosition;
		Pathfinder.nextPosition = nextPosition;
	}

	void Start()
	{
		Target = GameObject.FindGameObjectWithTag("Player").transform;

		Anime.SetBool("isMove", false);
		Anime.SetBool("isAttack", false);
	}


	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			_isMove = !_isMove;
			Anime.SetBool("isMove", _isMove);
		}

		float distance = Vector3.Distance(Target.position, transform.position);

		if (Anime.GetBool("isMove"))
		{
			Move();
			if (distance <= AttackRange)
			{
				Anime.SetBool("isAttack", true);
			}
		}
		else if (Anime.GetBool("isAttack"))
		{

		}
		else if (Anime.GetBool("isIdle"))
		{
			Stop();
			if (distance <= LookRange)
			{
				
			}
		}
	}

	
	private void Move()
	{
		Anime.applyRootMotion = true;
		Pathfinder.updatePosition = false; // 애니메이터가 움직임
		Pathfinder.updateRotation = true;

		Pathfinder.SetDestination(Target.position); // 적을 향해 간다
		SynchronizeAnimatorAndAgent();
	}

	private void Stop()
	{
		Anime.applyRootMotion = false;
		Pathfinder.updatePosition = false; // 애니메이터가 움직임
		Pathfinder.updateRotation = false;
	}

	private void SynchronizeAnimatorAndAgent()
	{
		Vector3 worldDeltaPosition = Pathfinder.nextPosition - transform.position;
		worldDeltaPosition.y = 0;

		float dx = Vector3.Dot(transform.right, worldDeltaPosition);
		float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
		Vector2 deltaPosition = new Vector2(dx, dy);

		float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
		_smoothDeltaPosition = Vector2.Lerp(_smoothDeltaPosition, deltaPosition, smooth);

		_velocity = _smoothDeltaPosition / Time.deltaTime;
		if (Pathfinder.remainingDistance <= Pathfinder.stoppingDistance)
		{
			_velocity = Vector2.Lerp(
				Vector2.zero,
				_velocity,
				Pathfinder.remainingDistance / Pathfinder.stoppingDistance);
		}

		//bool shouldMove = _velocity.magnitude > 0.5f
		//	&& Pathfinder.remainingDistance > Pathfinder.stoppingDistance;

		//Anime.SetBool("move", shouldMove);
		//Anime.SetFloat("locomotion", _velocity.magnitude);

		//float deltaMagnitude = worldDeltaPosition.magnitude;
		//if (deltaMagnitude > Pathfinder.radius / 2f)
		//{
		//	transform.position = Vector3.Lerp(
		//		Anime.rootPosition,
		//		Pathfinder.nextPosition,
		//		smooth
		//	);
		//}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, LookRange);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, AttackRange);
	}
}
