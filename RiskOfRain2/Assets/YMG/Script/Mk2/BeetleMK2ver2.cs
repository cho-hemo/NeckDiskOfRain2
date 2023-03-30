using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum MobState { Idle = 0, Recognition, Run, Attack }

public class BeetleMK2ver2 : MonoBehaviour
{
	private MobState mobState;
	WaitForSeconds Delay500 = new WaitForSeconds(0.5f);

	public float LookRange = 20f; // 시야 영역
	public float AttackRange = 4f; // 공격 영역
	Transform Target; // 목표
	NavMeshAgent Pathfinder; // 네비매쉬
	public float Speed = default; // 속도
	Animator Anime;
	public AnimationClip SpawnAnime;
	public AnimationClip AttackAnime;

	private bool _isLook = default;



	private void Awake()
	{
		Pathfinder = GetComponent<NavMeshAgent>();
		Anime = GetComponent<Animator>();
		//ChangeState(MobState.Idle);
	}

	void Start()
	{
		Target = GameObject.FindGameObjectWithTag("Player").transform;

		Anime.SetBool("isMove", false);
		Anime.SetBool("isAttack", false);
		Anime.SetBool("isDead", false);
		Anime.SetBool("isReco", false);

		StartCoroutine(AnimeWaiting());

		_isLook = false;

	}

	IEnumerator AnimeWaiting()
	{
		//DateTime sourTime = DateTime.Now;
		////Debug.Log($"애니 길이가 몇초? {SpawnAnime.length}");
		//yield return new WaitForSeconds(SpawnAnime.length);
		//DateTime destTime = DateTime.Now;
		//Debug.Log((destTime - sourTime).Duration());

		yield return new WaitForSeconds(SpawnAnime.length);
		// 애니메이션 발동 조건 
		Anime.SetBool("SpawnEnd", true);
	}

	void Update()
	{
		if (Anime.GetBool("SpawnEnd"))
		{
			float distance = Vector3.Distance(Target.position, transform.position);
			//FaceTarget();
			//if (true) ChangeState(MobState.Idle);
			if (distance <= LookRange && distance > AttackRange) ChangeState(MobState.Run);
			else if (distance <= AttackRange) ChangeState(MobState.Attack);
		}

		//UpdateState();
	}
	void UpdateState() // LEGACY
	{
		switch (mobState)
		{
			case MobState.Idle:
				Debug.Log("대기");
				break;
			case MobState.Run:
				Debug.Log("네비");
				break;
			case MobState.Attack:
				Debug.Log("공격");
				break;

		}

		//if (mobState == MobState.Idle) 
		//{
		//	Debug.Log("대기");
		//}
		//else if (mobState == MobState.Run) 
		//{
		//	Debug.Log("네비");
		//}
		//else if (mobState == MobState.Attack) 
		//{
		//	Debug.Log("공격");
		//}
	}

	void ChangeState(MobState newState)
	{
		if (mobState == newState)
			return;

		StopCoroutine(mobState.ToString());
		mobState = newState;
		StartCoroutine(mobState.ToString());
	}

	private IEnumerator Idle() // 코루틴 내부에서 루프
	{
		// 상태 진입 1회
		Pathfinder.SetDestination(transform.position);
		_isLook = false;
		Debug.Log("대기");
		Anime.SetBool("isMove", false);
		Anime.SetBool("isAttack", false);
		while (true) // 현재 상태에서 매 프레임
		{

			yield return null;
		}
		// 상태가 종료될 때 1회

	}
	//yield return null;	//// LEGACY
	//if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) 
	//{
	//	animator.SetBool("Idle", true);
	//}

	private IEnumerator Run() // 코루틴 내부에서 루프
	{
		// 상태 진입 1회
		Anime.SetBool("isAttack", false);

		StartCoroutine(Recognition());
		Anime.SetBool("isMove", true);

		while (true)
		{
			Debug.Log("네비");
			Pathfinder.SetDestination(Target.position); // 적을 향해 간다

			yield return null;
		}

		// 상태가 종료될 때 1회
		//Pathfinder.SetDestination(transform.position);

	}


	private IEnumerator Attack() // 코루틴 내부에서 루프
	{
		// 상태 진입 1회
		//FaceTarget(); 
		Anime.SetBool("isMove", false); // 움직임을 멈춘다
										//_isLook = true; // 바라본다
		Debug.Log("움직임을 멈춤.");
		//StartCoroutine(Recognition());
		FaceTarget();
		//ChangeState(MobState.Recognition);


		//StartCoroutine(FaceT()); // 3초 기다리고
		//_isLook = false; // 안바라본다
		//StopCoroutine(FaceT());
		Anime.SetBool("isAttack", true); // 공격한다
		Anime.SetBool("isReco", false); // 공격한다
		Debug.Log("공격");
		yield return null;
		yield return new WaitForSeconds(1.0f);

		// 상태가 종료될 때 1회
		Anime.SetBool("isAttack", false);
		Anime.SetBool("isReco", true);

	}

	private IEnumerator Recognition()
	{
		//float distance = Vector3.Distance(Target.position, transform.position);

		//if (distance <= LookRange && distance > AttackRange) ChangeState(MobState.Run);
		//yield return new WaitForSeconds(3.0f);
		//if (distance <= AttackRange) ChangeState(MobState.Attack);
		yield return new WaitForSeconds(3.0f);
	}

	private IEnumerator Term(float delay)
	{
		yield return new WaitForSeconds(delay);
	}

	//private IEnumerator Term(float delay, System.Action func_) // LEGACY
	//{
	//	yield return new WaitForSeconds(delay);
	//	func_?.Invoke();
	//}

	void FaceTarget()
	{
		//if (_isLook)

		Debug.Log("바라보기");
		// direction to the target
		Vector3 direction = (Target.position - transform.position).normalized;
		// rotation where we point to that target
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		// update our own rotation to point in this direction
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * Speed);

	}

	IEnumerator FaceT()
	{
		Debug.Log("바라보기");
		yield return new WaitForSeconds(3.0f);
		// direction to the target
		Vector3 direction = (Target.position - transform.position).normalized;
		// rotation where we point to that target
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		// update our own rotation to point in this direction
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * Speed);

	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, LookRange);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, AttackRange);
	}

	void DeathCheck()
	{
		Pathfinder.enabled = false;
		Anime.SetBool("isDead", true);
		gameObject.SetActive(false);
	}
}
