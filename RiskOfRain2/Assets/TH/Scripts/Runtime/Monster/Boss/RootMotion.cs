using UnityEngine;
using UnityEngine.AI;
using RiskOfRain2;

public class RootMotion : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private Animator _animator;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
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
        //if (Functions.GetSqrDistance(_agent.destination, transform.position) < _monster.MinSqrDetectRange)
        //{
        //	_agent.ResetPath();
        //	return;
        //}

        Vector3 nextPos = _animator.rootPosition;
        nextPos.y = _agent.nextPosition.y;

        transform.position = nextPos;

        _agent.nextPosition = transform.position;
    }
}