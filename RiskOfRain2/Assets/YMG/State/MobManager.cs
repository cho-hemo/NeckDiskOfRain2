using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobManager : MonoBehaviour
{
	Transform Target; // 목표
	NavMeshAgent Pathfinder; // 네비매쉬
	public float LookRange = 20f; // 시야 영역
	public float AttackRange = 4f; // 공격 영역
	public float Speed = default;
	Animator Anime;


	private enum State 
	{
		Idle,
		Move,
		Ready,
		Attack
	}
	private State _currentState;

	private void Awake()
	{
		// set default state
		Pathfinder = GetComponent<NavMeshAgent>();
		Anime = GetComponent<Animator>();
	}

	private void Start()
	{
		Target = GameObject.FindGameObjectWithTag("Player").transform;
		Anime.SetBool("isMove", false);
		Anime.SetBool("isAttack", false);
		Anime.SetBool("isDead", false);
	}

	private void Update()
    {
        // check current state
		if (_currentState == State.Move) 
		{
			Moveto();
		}
		else if (_currentState == State.Ready) 
		{
			Readyto();
		}
		else if (_currentState == State.Attack)
		{
			Attackto();
		}
		
	}

	private void Moveto() 
	{
		
	}

	private void Readyto() 
	{
		FaceTarget();
	}

	private void Attackto() 
	{
		Anime.SetBool("isAttack", true);
	}

	void FaceTarget()
	{
		Debug.Log("바라보기");
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
}
