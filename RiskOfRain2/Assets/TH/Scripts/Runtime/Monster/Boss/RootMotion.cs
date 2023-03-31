using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RiskOfRain2;

public class RootMotion : MonoBehaviour
{
<<<<<<< HEAD:RiskOfRain2/Assets/TH/Scripts/Runtime/Monster/Boss/RootMotion.cs
    [SerializeField] private GameObject _player;
=======
	[SerializeField] private GameObject _player;
	[SerializeField] private Transform[] footTargets;
>>>>>>> d4f92780f4b707568c9e528223b39541114f75d3:RiskOfRain2/Assets/TH/Scripts/Runtime/RootMotion.cs

	private MonsterBase _monster;
	private Animator _animator;
	private NavMeshAgent _agent;

	private void Awake()
	{
		_monster = GetComponent<MonsterBase>();
		_animator = GetComponent<Animator>();
		_agent = GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
		//transform.GetChild(0).transform.localPosition = Vector3.zero;
		//_agent.SetDestination(_player.transform.position);
		//SyncRootPosAndAgent();
	}

	public void Init()
	{
		_player = Global.FindRootObject("Player");
	}

	public void InitMove()
	{
		_animator.applyRootMotion = true;
		_agent.updatePosition = false;
		_agent.updateRotation = true;
	}

	public void Move()
	{
		_agent.SetDestination(_player.transform.position);
	}

	public void Stop()
	{
		_animator.applyRootMotion = false;
		_agent.updatePosition = false;
		_agent.updateRotation = true;
	}

	private void OnAnimatorMove()
	{
		if (Functions.GetSqrDistance(_agent.destination, transform.position) < _monster.MinSqrDetectRange)
		{
			_agent.ResetPath();
			return;
		}

		Vector3 nextPos = _animator.rootPosition;
		nextPos.y = _agent.nextPosition.y;

		transform.position = nextPos;

		_agent.nextPosition = transform.position;
	}
}