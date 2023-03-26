using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RootMotion : MonoBehaviour
{
    public GameObject _player;

    [SerializeField] private Transform[] footTargets;

    private Animator _animator;
    private NavMeshAgent _agent;

    private Vector2 _velocity;
    private Vector2 SmoothDeltaPosition;

    private void Awake()
    {
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
    }

    public void Stop()
    {
        _animator.applyRootMotion = false;
        _agent.updatePosition = false;
        _agent.updateRotation = false;
    }

    private void SyncRootPosAndAgent()
    {
        Vector3 worldDeltaPosition = _agent.nextPosition - transform.position;
        worldDeltaPosition.y = 0;


		if (worldDeltaPosition == Vector3.zero)
		{
			_animator.applyRootMotion = false;
			_agent.updatePosition = true;
			_agent.updateRotation = true;
		}
		else
		{
			InitMove();
		}

		Debug.Log(_animator.applyRootMotion);

        //float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        //float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        //Vector2 deltaPosition = new Vector2(dx, dy);

        //float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
        //SmoothDeltaPosition = Vector2.Lerp(SmoothDeltaPosition, deltaPosition, smooth);

        //_velocity = SmoothDeltaPosition / Time.deltaTime;
        //if (_agent.remainingDistance <= _agent.stoppingDistance)
        //{
        //    _velocity = Vector2.Lerp(Vector2.zero, _velocity, _agent.remainingDistance / _agent.stoppingDistance);
        //}

        //bool shouldMove = _velocity.magnitude > 0.5f && _agent.remainingDistance > _agent.stoppingDistance;
        ////Debug.Log(shouldMove);

        ////_animator.SetBool("isMove", shouldMove);

        ////_animator.SetFloat("", _velocity.magnitude);

        //float deltaMagnitude = worldDeltaPosition.magnitude;
        //if (deltaMagnitude > _agent.radius / 2f)
        //{
        //    transform.position = Vector3.Lerp(_animator.rootPosition, _agent.nextPosition, smooth);
        //}
    }

    private void OnAnimatorMove()
    {
		if (Functions.GetSqrDistance(_agent.destination, transform.position) <= 1)
			return;

		Vector3 nextPos = _animator.rootPosition;
        nextPos.y = _agent.nextPosition.y;

        transform.position = nextPos;

        _agent.nextPosition = transform.position;
    }
}