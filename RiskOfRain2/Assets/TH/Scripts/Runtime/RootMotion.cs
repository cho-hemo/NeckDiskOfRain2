using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RiskOfRain2;

public class RootMotion : MonoBehaviour
{
<<<<<<< HEAD
	[SerializeField] private GameObject _player;
	[SerializeField] private Transform[] footTargets;
=======
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform[] footTargets;
>>>>>>> a7a5151ec5aff19e6577ad75ab9049622fe59129

    private MonsterBase _monster;
    private Animator _animator;
    private NavMeshAgent _agent;



	private void Awake()
	{
		_monster = GetComponent<MonsterBase>();
		_animator = GetComponent<Animator>();
		_agent = GetComponent<NavMeshAgent>();
	}
<<<<<<< HEAD
	public void Init()
	{
		_player = Global.FindRootObject("Player");
	}
=======

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
>>>>>>> a7a5151ec5aff19e6577ad75ab9049622fe59129

    public void InitMove()
    {
        _animator.applyRootMotion = true;
        _agent.updatePosition = false;
        _agent.updateRotation = true;
    }

<<<<<<< HEAD
	private void Start()
	{
		//transform.GetChild(0).transform.localPosition = Vector3.zero;
		//_agent.SetDestination(_player.transform.position);
		//SyncRootPosAndAgent();
	}

	public void InitRotate()
	{
		_animator.applyRootMotion = false;
		_agent.updatePosition = false;
		_agent.updateRotation = true;
	}

	public void Move()
	{
		_agent.SetDestination(_player.transform.position);
		//SyncRootPosAndAgent();
	}
=======
    public void Move()
    {
        _agent.SetDestination(_player.transform.position);
    }
>>>>>>> a7a5151ec5aff19e6577ad75ab9049622fe59129

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