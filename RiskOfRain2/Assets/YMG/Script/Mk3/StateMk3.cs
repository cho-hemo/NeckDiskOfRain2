using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMk3
{
	public enum STATE 
	{
		IDLE, PATROL, MOVE, ATTACK, READY
	};

	public enum EVENT 
	{
		ENTER, UPDATE, EXIT
	};

	public STATE name;
	protected EVENT stage;
	protected GameObject npc;
	protected Animator anim;
	protected Transform player;
	protected StateMk3 nextState;
	protected NavMeshAgent agent;

	float visDist = 10.0f;
	float visAngle = 30.0f;
	float combat = 7.0f;

	public StateMk3(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) 
	{
		npc = _npc;
		agent = _agent;
		anim = _anim;
		stage = EVENT.ENTER;
		player = _player;
	}

	public virtual void Enter() { stage = EVENT.UPDATE; }
	public virtual void Update() { stage = EVENT.UPDATE; }
	public virtual void Exit() { stage = EVENT.EXIT;}

	public StateMk3 Process() 
	{
		if (stage == EVENT.ENTER) Enter();
		if (stage == EVENT.UPDATE) Update();
		if (stage == EVENT.EXIT) 
		{
			Exit();
			return nextState; 
		}
		return this;
	}

	public bool CanSeePlayer() 
	{
		Vector3 direction = player.position - npc.transform.position;
		float angle = Vector3.Angle(direction, npc.transform.forward);

		if (direction.magnitude < visDist && angle < visAngle) 
		{
			return true;
		}
		return false;

	}
	public bool CanAttackPlayer()
	{
		Vector3 direction = player.position - npc.transform.position;
		if (direction.magnitude < combat)
		{
			return true;
		}
		return false;
	}
}

public class IdleMk3 : StateMk3
{
	public IdleMk3(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
			: base(_npc, _agent, _anim, _player)
	{
		name = STATE.IDLE;
	}

	public override void Enter()
	{
		anim.SetTrigger("isIdle");
		base.Enter();
	}

	public override void Update()
	{
		// condition to Exit
		if (CanSeePlayer())
		{
			nextState = new MoveMk3(npc, agent, anim, player);
			stage = EVENT.EXIT;
		}
		else if (Random.Range(0, 100) < 10)
		{
			nextState = new PatrolMk3(npc, agent, anim, player);
			stage = EVENT.EXIT;
		}
	}

	public override void Exit()
	{
		anim.ResetTrigger("isIdle");
		base.Exit();
	}
}

public class PatrolMk3 : StateMk3 
{
	int currentIndex = -1;

	public PatrolMk3(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
			: base(_npc, _agent, _anim, _player) 
	{
		name = STATE.PATROL;
		agent.speed = 2;
		agent.isStopped = false;
	}

	public override void Enter() 
	{
		//currentIndex = 0;
		float lastDist = Mathf.Infinity;
		for (int i = 0; i < GameEnvironment.Singleton.Checkpoints.Count; i++) 
		{
			GameObject thisWP = GameEnvironment.Singleton.Checkpoints[i];
			float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position);
			if (distance < lastDist) 
			{
				currentIndex = i-1;
				lastDist = distance;
			}
		}
		anim.SetTrigger("isMoving");
		base.Enter();
	}
	public override void Update()
	{
		if (agent.remainingDistance < 1) 
		{
			if (currentIndex >= GameEnvironment.Singleton.Checkpoints.Count - 1)
				currentIndex = 0;
			else
				currentIndex++;
			agent.SetDestination(GameEnvironment.Singleton.Checkpoints[currentIndex].transform.position);
		}
		// condition to Exit
		if (CanSeePlayer())
		{
			nextState = new MoveMk3(npc, agent, anim, player);
			stage = EVENT.EXIT;
		}
	}
	public override void Exit()
	{
		anim.ResetTrigger("isMoving");
		base.Exit();
	}
}

public class MoveMk3 : StateMk3 
{
	public MoveMk3(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
			: base(_npc, _agent, _anim, _player) 
	{
		name = STATE.MOVE;
		agent.speed = 5;
		agent.isStopped = false;
	}

	public override void Enter() 
	{
		anim.SetTrigger("isMoving");
		base.Enter();
	}

	public override void Update()
	{
		agent.SetDestination(player.position);
		if (agent.hasPath) 
		{
			if (CanAttackPlayer())
			{
				nextState = new AttackMk3(npc, agent, anim, player);
				stage = EVENT.EXIT;
			}
			else if (!CanSeePlayer()) 
			{
				nextState = new PatrolMk3(npc, agent, anim, player);
				stage = EVENT.EXIT;
			}
		}
	}

	public override void Exit() 
	{
		anim.ResetTrigger("isMoving");
		base.Exit();
	}
}

public class AttackMk3 : StateMk3 
{
	float rotationSpeed = 2.0f;
	public AttackMk3(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
			: base(_npc, _agent, _anim, _player) 
	{
		name = STATE.ATTACK;
	}

	public override void Enter() 
	{
		anim.SetTrigger("isAttacking");
		agent.isStopped = true;
		base.Enter();
	}
	public override void Update() 
	{
		Vector3 direction = player.position - npc.transform.position;
		float angle = Vector3.Angle(direction, npc.transform.forward);
		direction.y = 0;

		npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
			Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
		if (!CanAttackPlayer()) 
		{
			nextState = new IdleMk3(npc, agent, anim, player);
			stage = EVENT.EXIT;
		}
	}
	public override void Exit() 
	{
		anim.ResetTrigger("isAttacking");
		base.Exit();
	}
}
