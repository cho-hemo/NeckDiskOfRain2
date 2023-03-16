using UnityEngine;
using UnityEngine.AI;

public class RootMotion : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

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

        _animator.applyRootMotion = true;
        _agent.updatePosition = false;
        _agent.updateRotation = true;
    }

    private void Start()
    {
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

        //transform.position += _animator.deltaPosition;
        _agent.nextPosition = transform.position;
    }

    private void OnAnimatorIK()
    {
        
    }
}