using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RootMotion : MonoBehaviour
{
    public GameObject _player;

    [SerializeField] private Transform[] footTargets;

    private MonsterBase _monster;
    private MonsterFSM _fsm;
    private Animator _animator;
    private NavMeshAgent _agent;

    private Vector2 _velocity;
    private Vector2 SmoothDeltaPosition;

    private void Awake()
    {
        _monster= GetComponent<MonsterBase>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //transform.GetChild(0).transform.localPosition = Vector3.zero;
        //_agent.SetDestination(_player.transform.position);
        //SyncRootPosAndAgent();
    }

    public void InitMove()
    {
        _animator.applyRootMotion = true;
        _agent.updatePosition = false;
        _agent.updateRotation = true;
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

    public void Stop()
    {
        _animator.applyRootMotion = false;
        _agent.updatePosition = false;
        _agent.updateRotation = true;
    }

    private void SyncRootPosAndAgent()
    {
		//_agent.velocity = _animator.deltaPosition / Time.deltaTime;
		//_animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
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