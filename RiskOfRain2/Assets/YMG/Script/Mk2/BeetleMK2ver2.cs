using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public enum MobState { Idle = 0, Run, Attack}

public class BeetleMK2ver2 : MonoBehaviour
{
	private MobState mobState;
	WaitForSeconds Delay500 = new WaitForSeconds(0.5f);

	public float LookRange = 20f; // 시야 영역
	public float AttackRange = 4f; // 공격 영역
	public float Speed = default; // 이동속도
	NavMeshAgent Pathfinder; // 네비매쉬
	Transform Target; // 목표
	Animator Anime;
	public AnimationClip SpawnAnime;


	private void Awake()
	{
		Pathfinder = GetComponent<NavMeshAgent>();
		Anime = GetComponent<Animator>();
		ChangeState(MobState.Idle);
	}

	void Start()
    {
		Target = GameObject.FindGameObjectWithTag("Player").transform;

		Anime.SetBool("isMove", false);
		Anime.SetBool("isAttack", false);
		Anime.SetBool("isDead", false);

		StartCoroutine(AnimeWaiting());
	}

	IEnumerator AnimeWaiting()
	{
		//Debug.Log($"애니 길이가 몇초? {SpawnAnime.length}");
		yield return new WaitForSeconds(SpawnAnime.length);

		// 애니메이션 발동 조건 
		Anime.SetBool("SpawnEnd", true);
	}
    void Update()
    {
		if (Anime.GetBool("SpawnEnd"))
		{
			float distance = Vector3.Distance(Target.position, transform.position);

			//if (true) ChangeState(MobState.Idle);
			if (distance <= LookRange && distance > AttackRange) ChangeState(MobState.Run);
			if (distance <= AttackRange) ChangeState(MobState.Attack);
		}

		//UpdateState();
	}
	void UpdateState() // LAGACY
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
		StopCoroutine(mobState.ToString());
		mobState = newState;
		StartCoroutine(mobState.ToString());
	}

	private IEnumerator Idle() // 코루틴 내부에서 루프
	{
		// 상태 진입 1회
			Debug.Log("대기");
		Anime.SetBool("isMove", false);
		Anime.SetBool("isAttack", false);
		while (true) // 현재 상태에서 매 프레임
		{
			yield return null;
		}
		// 상태가 종료될 때 1회

	}
		//yield return null;	//// LAGACY
		//if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) 
		//{
		//	animator.SetBool("Idle", true);
		//}

	private IEnumerator Run() // 코루틴 내부에서 루프
	{
		// 상태 진입 1회
		FaceTarget(); // 적을 바라보고
		Anime.SetBool("isMove", true);
		Anime.SetBool("isAttack", false);
		
			Debug.Log("네비");
			Pathfinder.SetDestination(Target.position); // 적을 향해 간다
		while (true) // 현재 상태에서 매 프레임
		{
			yield return null;
		}
		// 상태가 종료될 때 1회

	}
	private IEnumerator Attack() // 코루틴 내부에서 루프
	{
		// 상태 진입 1회
		FaceTarget(); // 적을 바라보고

		Pathfinder.SetDestination(transform.position);
		Anime.SetBool("isMove", false);
		Anime.SetBool("isAttack", true);
			Debug.Log("공격");
		
		while (true) // 현재 상태에서 매 프레임
		{
			yield return null;
		}
		// 상태가 종료될 때 1회

	}

	void FaceTarget()
	{
		
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
