using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RootMotion : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform[] footTargets;

    private Animator _animator;
    private NavMeshAgent _agent;

    //
    private bool _isMove = false;

    private Vector2 _velocity;
    private Vector2 SmoothDeltaPosition;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        //_animator.applyRootMotion = true;
        _agent.updatePosition = false;
        _agent.updateRotation = true;
    }

    private void Start()
    {
        //transform.GetChild(0).transform.localPosition = Vector3.zero;
        //_agent.SetDestination(_player.transform.position);
        //SyncRootPosAndAgent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isMove = !_isMove;

            if (_isMove)
            {
                _animator.SetBool("isMove", true);
            }
            else
            {
                _animator.SetBool("isMove", false);
            }
        }

        if (_isMove)
        {
            _agent.SetDestination(_player.transform.position);
            SyncRootPosAndAgent();
        }
    }

    private void SyncRootPosAndAgent()
    {
        Vector3 worldDeltaPosition = _agent.nextPosition - transform.position;
        worldDeltaPosition.y = 0;

        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
        SmoothDeltaPosition = Vector2.Lerp(SmoothDeltaPosition, deltaPosition, smooth);

        _velocity = SmoothDeltaPosition / Time.deltaTime;
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _velocity = Vector2.Lerp(Vector2.zero, _velocity, _agent.remainingDistance / _agent.stoppingDistance);
        }

        bool shouldMove = _velocity.magnitude > 0.5f && _agent.remainingDistance > _agent.stoppingDistance;
        Debug.Log(shouldMove);

        _animator.SetBool("isMove", shouldMove);
        //_animator.SetFloat("", _velocity.magnitude);

        float deltaMagnitude = worldDeltaPosition.magnitude;
        if (deltaMagnitude > _agent.radius / 2f)
        {
            transform.position = Vector3.Lerp(_animator.rootPosition, _agent.nextPosition, smooth);
        }
    }

    private void OnAnimatorMove()
    {
        Vector3 nextPos = _animator.rootPosition;
        nextPos.y = _agent.nextPosition.y;

        transform.position = nextPos;

        _agent.nextPosition = transform.position;
        //transform.position += _animator.deltaPosition;

        ////footsteps
        //for (int i = 0; i < footTargets.Length; i++)
        //{
        //	var foot = footTargets[i];
        //	var ray = new Ray(foot.transform.position + Vector3.up * 0.5f, Vector3.down);
        //	var hitInfo = new RaycastHit();
        //	if (Physics.SphereCast(ray, 0.05f, out hitInfo, 0.50f))
        //	{
        //		Vector3 prevPos = foot.position;
        //		//Debug.Log($"{i}. {hitInfo}");
        //		foot.position = hitInfo.point + Vector3.up * 0.05f;

        //		if (prevPos != foot.position && i == 0)
        //			Debug.Log($"{i}. [prev]{prevPos} [curr]{foot.position}");
        //	}
        //}
    }
}